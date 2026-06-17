using System.ComponentModel.DataAnnotations;

namespace RouteProject.BLL.Services.ViewModels.PlanViewModels
{
    public class PlanToUpdateViewModel
    {
        public string? Name { get; set; }
        [Required(ErrorMessage = "Duration Days is required.")]
        [Range(1, 365, ErrorMessage = "Duration Days must be between 1 and 365.")]
        public int DurationDay { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }
    }
}
