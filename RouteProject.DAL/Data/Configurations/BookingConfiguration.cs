using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RouteProject.DAL.Data.Models;

namespace RouteProject.DAL.Data.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Ignore(b => b.Id);
            builder.Property(b => b.CreateAt).HasColumnName("BookingDate").HasDefaultValueSql("GETDATE()");
            builder.HasKey(b => new { b.SessionId, b.MemberId });
        }
    }
}
