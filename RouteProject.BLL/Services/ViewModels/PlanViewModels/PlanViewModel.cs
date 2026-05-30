namespace RouteProject.BLL.Services.ViewModels.PlanViewModels
{
    public class PlanViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public bool IsActive { get; set; }
    }
}
