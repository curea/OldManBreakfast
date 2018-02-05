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
                if (context.Breakfasts.Any() && IsDevelopment)
                {
                    context.Breakfasts.Add(new Breakfast { Id = 1, Name = "Rolling Thunder", Description = "Rock & Roll", EventDate = new DateTime(2018, 02, 17, 06, 30, 00), FallbackDate = new DateTime(2018, 02, 24, 06, 30, 00) });
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
