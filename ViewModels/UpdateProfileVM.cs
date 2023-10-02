using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.ViewModels
{
    public class UpdateProfileVM
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Fullname must be between 3 - 100 characters")]
        public string fullName { get; set; } = "";

        [StringLength(12, MinimumLength = 10, ErrorMessage = "Phone must be 10 - 12 characters")]
        [RegularExpression(@"^0[3|5|7|8|9]\d{8,10}$", ErrorMessage = "Phone must be numeric")]
        public string phone { get; set; } = "";

        [StringLength(255, ErrorMessage = "Address maximum 255 characters")]
        public string address { get; set; } = "";

        [Required(ErrorMessage = "Confirm password is required")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Confirm password must be between 6 - 255 characters")]
        public string confirmPassword { get; set; } = "";
    }
}
