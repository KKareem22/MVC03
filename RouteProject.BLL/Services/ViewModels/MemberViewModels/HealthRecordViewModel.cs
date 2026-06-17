using System.ComponentModel.DataAnnotations;

namespace RouteProject.BLL.Services.ViewModels.MemberViewModels
{
    public class HealthRecordViewModel
    {
        [Range(.1, 300, ErrorMessage = "Height must grater than 0 .")]
        public decimal Height { get; set; }
        [Range(.1, 500, ErrorMessage = "Weight must grater than 0 .")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Blood type is required.")]
        [StringLength(3, ErrorMessage = "Blood type must be at most 3 characters.")]
        public string BloodType { get; set; } = default!;

        public string? Note { get; set; }
    }
}
