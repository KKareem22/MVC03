namespace RouteProject.BLL.Services.ViewModels.MemberViewModels
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string? Photo { get; set; }
        public string Gender { get; set; } = default!;

        //Details Member
        public string DateOfBirth { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string? PlanName { get; set; }
        public string? MembershipStartDate { get; set; }
        public string? MembershipEndDate { get; set; }
    }
}
