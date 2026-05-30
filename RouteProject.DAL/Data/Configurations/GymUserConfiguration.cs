using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RouteProject.DAL.Data.Models;

namespace RouteProject.DAL.Data.Configurations
{
    public class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(u => u.Name).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(u => u.Email).HasMaxLength(100);
            builder.Property(u => u.Phone).HasMaxLength(11);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Phone).IsUnique();
            builder.ToTable(ta =>
            {
                ta.HasCheckConstraint("CK-Email", "Email Like '%___@%___%.__%'");
                ta.HasCheckConstraint("CK-Phone", "Phone Like '010%' or Phone Like'011%' or Phone like '012%' or Phone Like'015%'");
            });
            builder.OwnsOne(u => u.Address, a =>
            {
                a.Property(s => s.Street).HasColumnName("Street").HasColumnType("varchar").HasMaxLength(30);
                a.Property(s => s.City).HasColumnName("City").HasColumnType("varchar").HasMaxLength(30);
            });
        }
    }
}
