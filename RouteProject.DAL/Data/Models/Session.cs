namespace RouteProject.DAL.Data.Models
{
    public class Session : BaseEntity
    {
        public string Description { get; set; } = default!;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; } = default!;
    }
}
