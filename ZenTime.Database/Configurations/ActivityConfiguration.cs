using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZenTime.Domain.TimeSheets;

namespace ZenTime.Database.Configurations
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.Property(e => e.Id);
            
            builder.Property(e => e.Name)
                .HasMaxLength(100);

            builder.Property(e => e.CreatedAt)
                .HasColumnType("datetimeoffset");
            
            builder.Property(e => e.UpdatedAt)
                .HasColumnType("datetimeoffset");
        }
    }
}