using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.src.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Fullname must be between 3 - 50 characters")]
        public string fullname { get; set; } = "";

        [Required]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "Phone must be 10 - 12 characters")]
        [RegularExpression(@"^\+?(\d{10,12})$", ErrorMessage = "Invalid phone number")]
        public string phone { get; set; } = "";

        [StringLength(255, ErrorMessage = "Address maximum 255 characters")]
        public string address { get; set; } = "";

        [Required]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Password must be between 6 - 255 characters")]
        public string password { get; set; } = "";
    }
}
