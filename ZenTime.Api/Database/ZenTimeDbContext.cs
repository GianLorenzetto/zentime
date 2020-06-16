using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZenTime.Api.Database.Entities;

namespace ZenTime.Api.Database
{
    public class ZenTimeDbContext : DbContext
    {
        public DbSet<TimeSheetProjectDefinition> ProjectDefinitions { get; set; }
        public DbSet<TimeSheetTaskDefinition> TaskDefinitions { get; set; }
        public DbSet<TimeSheetEntry> TimeSheetEntries { get; set; }

        public ZenTimeDbContext(DbContextOptions<ZenTimeDbContext> options):
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("zt");
        }
    }
}