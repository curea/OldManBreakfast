using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace OldManBreakfast.Data
{
     public static class DetailedCompareExtension
    {
        public static List<Variance> DetailedCompare<T>(this T Original, T Compare, string KeyValue = null, string NS = null)
        {
            var variances = new List<Variance>();
            if (Compare != null)
            {
                var fi = Original.GetType().GetRuntimeProperties();
                foreach (var f in fi)
                {
                    var att = f.GetCustomAttribute(typeof(NoCompare));
                    if (att == null)
                    {
                        if (typeof(IEnumerable).IsAssignableFrom(f.PropertyType) && !typeof(string).IsAssignableFrom(f.PropertyType))
                        {
                            var coll1 = (IEnumerable)f.GetValue(Original);
                            var coll2 = (IEnumerable)f.GetValue(Compare);
                            var key = getKeyFieldFromList(coll2);
                            if (key == null)
                                key = getKeyFieldFromList(coll1);

                            //Either both lists are empty or null
                            if (key == null)
                                continue;

                            foreach (var item2 in coll2)
                            {
                                var item1 = coll1.GetItemByKey(key, key.GetValue(item2));
                                var keyPath = !string.IsNullOrEmpty(KeyValue) ? string.Join(":", KeyValue, key.GetValue(item2).ToString()) : key.GetValue(item2).ToString();
                                var newNs = !string.IsNullOrEmpty(NS) ? string.Join(".", NS, f.Name) : f.Name;
                                var childVariances = item2.DetailedCompare(item1, keyPath, newNs);
                                variances.AddRange(childVariances);
                            }

                        }
                        else
                        {
                            var v = new Variance();
                            v.Name = !string.IsNullOrEmpty(NS) ? string.Join(".", NS, f.Name) : f.Name;
                            v.Key = KeyValue;
                            v.Original = f.GetValue(Original);
                            v.Compare = f.GetValue(Compare);
                            if ((v.Original != null && !v.Original.Equals(v.Compare)) || (v.Compare != null && !v.Compare.Equals(v.Original)))
                                variances.Add(v);
                        }
                    }
                }
            }
            else
            {
                var v = new Variance();
            }
            return variances;
        }

        public static void ApplyVariance<T>(this T ApplyTo, Variance Variance)
        {
            var applyToItem = getApplyToItem(ApplyTo, Variance);
            var propertyInfo = applyToItem.GetType().GetProperty(Variance.Name.Split('.').Last());
            var safeType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
            var safeValue = (Variance.Compare == null) ? null : Convert.ChangeType(Variance.Compare, safeType);
            propertyInfo.SetValue(applyToItem, safeValue, null);
        }

        private static object getApplyToItem(object obj, Variance variance)
        {
            var path = variance.Name.Split('.');
            if (path.Length == 1)
                return obj;

            for (int i = 0; i < path.Length; i++)
            {
                if (obj == null) { return null; }

                var type = obj.GetType();

                //If this is a list, find the corresponding item
                if (typeof(IEnumerable).IsAssignableFrom(type) && !typeof(string).IsAssignableFrom(type))
                {
                    var coll = (IEnumerable)obj;
                    var key = getKeyFieldFromList(coll);
                    var item = coll.GetItemByKey(key, Convert.ChangeType(variance.Key, key.PropertyType));

                    if (i == path.Length - 1)
                        return item;
                        
                    obj = item;
                    type = obj.GetType();
                }

                var info = type.GetProperty(path[i]);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }

            return null;
        }

        private static PropertyInfo getKeyField(Type itemType)
        {
            var propertyList = itemType.GetRuntimeProperties();
            foreach (var property in propertyList)
            {
                var att = property.GetCustomAttribute(typeof(KeyAttribute));
                if (att != null)
                    return property;
            }
            return null;
        }

        private static PropertyInfo getKeyFieldFromList(IEnumerable items)
        {
            if (items != null)
            {
                var itemType = getFirstItemType(items);
                if (itemType != null)
                    return getKeyField(itemType);
            }

            return null;
        }

        private static Type getFirstItemType(IEnumerable items)
        {
            IEnumerator iter = items.GetEnumerator();
            if (iter.MoveNext())
                return iter.Current.GetType();

            return null;
        }

        private static object GetItemByKey(this IEnumerable items, PropertyInfo key, object value)
        {
            foreach (var item in items)
            {
                if (key.GetValue(item).Equals(value))
                    return item;
            }

            return null;
        }
    }

    /// <summary>
    /// The NoCompare attribute is used to flag properties that should be ignored in a Detailed Compare
    /// </summary>
    public class NoCompare : Attribute { }

    public class Variance
    {
        public string Name { get; set; }
        public object Key { get; set; }
        public object Original { get; set; }
        public object Compare { get; set; }
    }
}