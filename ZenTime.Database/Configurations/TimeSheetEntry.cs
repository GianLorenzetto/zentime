using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZenTime.Domain.TimeSheets;

namespace ZenTime.Database.Configurations
{
    public class TimeSheetConfiguration : IEntityTypeConfiguration<TimeSheet>
    {
        public void Configure(EntityTypeBuilder<TimeSheet> builder)
        {
            builder.Property(e => e.Id);

            builder.HasOne<Activity>()
                .WithMany()
                .HasForeignKey(e => e.ActivityId);
            
            builder.HasOne<Project>()
                .WithMany()
                .HasForeignKey(e => e.ProjectId);

            builder.Property(e => e.Details)
                .HasMaxLength(500);

            builder.Property(e => e.DurationInMinutes);

            builder.Property(e => e.StartedAt)
                .HasColumnType("datetimeoffset");
            
            builder.Property(e => e.CreatedAt)
                .HasColumnType("datetimeoffset");
            
            builder.Property(e => e.UpdatedAt)
                .HasColumnType("datetimeoffset");
        }
    }
}