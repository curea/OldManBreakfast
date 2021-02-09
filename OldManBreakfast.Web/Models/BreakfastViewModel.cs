using System;
using System.Collections.Generic;

namespace OldManBreakfast.Web.Models
{
    public class BreakfastViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime? FallbackDate { get; set; }
        public List<AttachedImageViewModel> Images { get; set; }
    }
}