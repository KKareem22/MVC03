namespace RouteProject.DAL.Data.Models
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; } = default!;
        public ICollection<Session> Sessions { get; set; } = new HashSet<Session>();
    }
}
