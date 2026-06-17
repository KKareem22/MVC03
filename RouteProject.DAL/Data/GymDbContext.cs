using Microsoft.EntityFrameworkCore;
using RouteProject.DAL.Data.Configurations;
using RouteProject.DAL.Data.Models;

namespace RouteProject.DAL.Data
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {

        }
        public DbSet<Member> Members { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MemberConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new SessionConfigurtion());
            modelBuilder.ApplyConfiguration(new TrainerConfiguration());
            modelBuilder.ApplyConfiguration(new PlanConfiguration());
            modelBuilder.ApplyConfiguration(new MemberShipConfiguration());

            modelBuilder.Entity<Plan>().HasData(
                new Plan { Id = 1, Name = "Basic Plan", DurationDays = 30, Description = "Access to gym equiment during staffed hours.", Price = 300m },
                new Plan { Id = 2, Name = "Standard Plan", DurationDays = 60, Description = "Includes Basic Plan benefits plus access to group classes.", Price = 500m, IsActive = false },
                new Plan { Id = 3, Name = "Premium Plan", DurationDays = 90, Description = "Unlimited access to equiment classes and sauna.", Price = 900m, IsActive = false },
                new Plan { Id = 4, Name = "Annual Plan", DurationDays = 365, Description = "Full Year access with personal trainer sessions.", Price = 3000m }
            );


        }

    }
}
