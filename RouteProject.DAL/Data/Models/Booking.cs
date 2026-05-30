namespace RouteProject.DAL.Data.Models
{
    public class Booking : BaseEntity
    {
        public int SessionId { get; set; }
        public Session Session { get; set; } = default!;
        public int MemberId { get; set; }
        public Member Member { get; set; } = default!;
        public bool IsAttended { get; set; } = false;
    }
}
