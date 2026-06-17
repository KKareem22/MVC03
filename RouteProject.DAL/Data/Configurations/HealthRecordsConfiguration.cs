using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RouteProject.DAL.Data.Models;

namespace RouteProject.DAL.Data.Configurations
{
    public class HealthRecordsConfiguration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.Property(x => x.BloodType).HasMaxLength(5);
            builder.Property(x => x.Note).HasMaxLength(500);
            builder.Property(x => x.Height).HasPrecision(5, 2);
            builder.Property(x => x.Weight).HasPrecision(5, 2);
            builder.Property(x => x.CreateAt).HasDefaultValueSql("GETDATE()");
        }
    }
}
