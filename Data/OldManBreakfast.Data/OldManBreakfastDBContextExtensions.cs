using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

using OldManBreakfast.Data.Models;

namespace OldManBreakfast.Data
{
    public static class OldManBreakfastDBContextExtensions
    {
        public static void EnsureSeedData(this OldManBreakfastDBContext context, bool IsDevelopment = false)
        {
            if (context.AllMigrationsApplied())
            {
                if (!context.Breakfasts.Any() && context.Users.Any() && IsDevelopment)
                {
                    var breakfast = new Breakfast { Id = 1, Name = "Rolling Thunder", Description = "Davies Chuck Wagon Diner<br/>9495 W Colfax Ave, Lakewood, CO 80215<br/><br/>We'll meet at Davies for breakfast, then head out to <b>Oh My God Road</b>.<br/>If you have a 4-Wheel or other high-clearance vehicle, this should be an easy trail.<br/>Here is a map of the area: <a href='/images/Breakfast/1/Clear_Creek_GEO_PDF.pdf' target='_blank'><br/><img src='/images/Breakfast/1/Clear_Creek_GEO.jpg'/></a>", EventDate = new DateTime(2018, 03, 17, 06, 30, 00), Organizer = context.Users.FirstOrDefault() };
                    breakfast.Images.Add(new AttachedImage() { Source = "/images/Breakfast/1/Oh-My-God-Road-small.jpg", Url="/images/Breakfast/1/Oh-My-God-Road.jpg" });
                    context.Breakfasts.Add(breakfast);
                }
                context.SaveChanges();
            }
        }

        public static bool AllMigrationsApplied(this OldManBreakfastDBContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }
    }
}
