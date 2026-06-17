using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RouteProject.DAL.Data.Models;

namespace RouteProject.DAL.Data.Configurations
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(p => p.Description).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(p => p.Price).HasPrecision(10, 2);
            builder.Property(p => p.CreateAt).HasDefaultValueSql("GETDATE()");

            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Plan_Duration", "DurationDays between 1 and 365");
            });
        }
    }
}
