using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZenTime.Common;
using ZenTime.Database.Extensions;
using ZenTime.Domain.Common;
using ZenTime.Domain.TimeSheets;

namespace ZenTime.Database
{
    public class ZenTimeDbContext : DbContext
    {
        private readonly IDateTimeOffsetProvider _dateTimeOffsetProvider;
        
        public DbSet<Project>? TimeSheetProjects { get; set; }
        public DbSet<Activity>? TimeSheetActivities { get; set; }
        public DbSet<TimeSheet>? TimeSheetEntries { get; set; }

        public ZenTimeDbContext(DbContextOptions<ZenTimeDbContext> options):
            base(options)
        {
            _dateTimeOffsetProvider = new DateTimeOffsetProvider();
        }
        
        public ZenTimeDbContext(DbContextOptions<ZenTimeDbContext> options, IDateTimeOffsetProvider dateTimeOffsetProvider):
            base(options)
        {
            _dateTimeOffsetProvider = dateTimeOffsetProvider;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            if (!ChangeTracker.AutoDetectChangesEnabled)
            {
                ChangeTracker.DetectChanges();
            }
            
            AuditAddedEntities();
            AuditUpdatedEntities();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AuditAddedEntities()
        {
            var addedEntries = ChangeTracker.Entries<AuditableEntity>()
                .Where(e =>
                    e.State == EntityState.Added)
                .ToArray();

            foreach (var entry in addedEntries)
            {
                entry.CurrentValues[nameof(AuditableEntity.CreatedAt)] = _dateTimeOffsetProvider.UtcNow;
            }
        }
        
        private void AuditUpdatedEntities()
        {
            var updatedEntries = ChangeTracker.Entries<AuditableEntity>()
                .Where(e =>
                    e.State == EntityState.Modified
                    || e.State == EntityState.Deleted)
                .ToArray();

            foreach (var entry in updatedEntries)
            {
                entry.CurrentValues[nameof(AuditableEntity.UpdatedAt)] = _dateTimeOffsetProvider.UtcNow;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("zt");
            modelBuilder.RemovePluralizingTableNameConvention();
        }
    }
}