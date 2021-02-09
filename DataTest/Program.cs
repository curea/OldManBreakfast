using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

using Microsoft.EntityFrameworkCore;

using OldManBreakfast.Data;
using OldManBreakfast.Data.Models;

namespace DataTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new OldManBreakfastDBContext();

            if (!context.AllMigrationsApplied())
                context.Database.Migrate();
            context.EnsureSeedData();
        }
    }
}
