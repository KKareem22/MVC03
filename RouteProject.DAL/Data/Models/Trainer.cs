using RouteProject.DAL.Data.Models.Enums;

namespace RouteProject.DAL.Data.Models
{
    public class Trainer : GymUser
    {
        public Specialties Specialties { get; set; }

        //HireDate in Database=CreateAt in BaseEntity
        public ICollection<Session> Sessions { get; set; } = new HashSet<Session>();
    }
}
