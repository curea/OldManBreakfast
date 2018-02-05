using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using OldManBreakfast.Data.Models;

namespace OldManBreakfast.Data
{
    public class OldManBreakfastDBContext: IdentityDbContext<ApplicationUser, ApplicationUserRole, long>
    {
        public DbSet<Breakfast> Breakfasts { get; set; }      

        public OldManBreakfastDBContext() : base(new DbContextOptions<OldManBreakfastDBContext>()) { }

        public OldManBreakfastDBContext(DbContextOptions<OldManBreakfastDBContext> options) : base(options) { }

        public override int SaveChanges()
        {
            updateBaseEntityFields();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            updateBaseEntityFields();
            return await this.SaveChangesAsync(CancellationToken.None);
        }

        private void updateBaseEntityFields()
        {
            var currentTime = DateTime.Now.ToUniversalTime();

            var entities = this.ChangeTracker
                .Entries()
                .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added && x.Entity != null && typeof(BaseEntity).IsAssignableFrom(x.Entity.GetType()))
                .ToList();

            // Set the create/modified date as appropriate
            foreach (var entity in entities)
            {
                var entityBase = entity.Entity as BaseEntity;
                if (entity.State == EntityState.Added)
                {
                    entityBase.Created = currentTime;
                    //entityBase.CreateUserId = userId;
                }

                entityBase.Updated = currentTime;
                entityBase.Version++;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            if (builder.Options.Extensions.FirstOrDefault(e => e is Microsoft.EntityFrameworkCore.Infrastructure.Internal.SqliteOptionsExtension) == null)
                SqliteDbContextOptionsBuilderExtensions.UseSqlite(builder, "Data Source=OldManBreakfast.db", null);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
