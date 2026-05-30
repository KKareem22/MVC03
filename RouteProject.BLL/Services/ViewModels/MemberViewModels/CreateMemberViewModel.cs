using RouteProject.DAL.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace RouteProject.BLL.Services.ViewModels.MemberViewModels
{
    public class CreateMemberViewModel
    {
        [Required(ErrorMessage = "Name is Required")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Phone is Required")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone number must be a valid Egyptian mobile number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = default!;

        [Required(ErrorMessage = "Date of Birth is Required")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Building Number is Required")]
        [Range(1, 9000, ErrorMessage = "Must between 1 to 9000")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "City is Required")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "City can only contian letters and spaces. ")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "City must be between 2 and 100 characters.")]
        public string City { get; set; } = default!;

        [Required(ErrorMessage = "Street is Required")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Street must be between 2 and 150 characters.")]
        [RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "Street can only contain letters, numbers and spaces.")]
        public string Street { get; set; } = default!;

        [Required(ErrorMessage = "Health Record is Required")]
        public HealthRecordViewModel HealthRecordViewModel { get; set; } = default!;
    }
}
