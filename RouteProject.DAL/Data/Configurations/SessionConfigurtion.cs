using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RouteProject.DAL.Data.Models;

namespace RouteProject.DAL.Data.Configurations
{
    public class SessionConfigurtion : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.Property(s => s.CreateAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(s => s.Category)
                .WithMany(c => c.Sessions)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Trainer)
                .WithMany(t => t.Sessions)
                .HasForeignKey(s => s.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Session_Capacity", "Capacity Between 1 And 25");
                t.HasCheckConstraint("CK_Session_Dates", "StartDate < EndDate");
            });
        }
    }
}
