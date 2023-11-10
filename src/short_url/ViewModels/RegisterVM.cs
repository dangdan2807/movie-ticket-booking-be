using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "full name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Fullname must be between 3 - 50 characters")]
        public string fullname { get; set; } = "";

        [Required(ErrorMessage = "Email is required")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Email must be between 6 and 255 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+(?:\\.[a-zA-Z0-9]+)*@[a-zA-Z0-9]+(?:\\.[a-zA-Z0-9]+)*$", ErrorMessage = "invalid email")]
        public string email { get; set; } = "";

        [StringLength(255, ErrorMessage = "Address maximum 255 characters")]
        public string address { get; set; } = "";

        [Required(ErrorMessage = "password is required")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Password must be between 6 - 255 characters")]
        public string password { get; set; } = "";
    }
}
