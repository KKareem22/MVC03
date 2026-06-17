namespace RouteProject.DAL.Data.Models
{
    public class Member : GymUser
    {
        public string? Photo { get; set; }
        //joinDate in Database=CreateAt in BaseEntity
        public HealthRecord HealthRecord { get; set; } = default!;

        public ICollection<MemberShip> MemberShips { get; set; } = default!;
        public ICollection<Booking> GetBookings { get; set; } = new HashSet<Booking>();
    }
}
