using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.src.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Phone is required")]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "Phone must be 10 - 12 characters")]
        [RegularExpression(@"^0[3|5|7|8|9]\d{8}$", ErrorMessage = "Phone must be numeric")]
        public string phone { get; set; } = "";

        [Required(ErrorMessage = "password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "password must be between 6 and 100 characters")]
        public string password { get; set; } = "";
    }
}
