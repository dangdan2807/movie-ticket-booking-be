using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.ViewModels
{
    public class UpdateProfileVM
    {
        //[StringLength(100, MinimumLength = 3, ErrorMessage = "Fullname must be between 3 - 100 characters")]
        public string? fullName { get; set; } = "";

        //[Required(ErrorMessage = "Email is required")]
        //[StringLength(255, MinimumLength = 6, ErrorMessage = "Email must be between 6 and 255 characters")]
        //[RegularExpression(@"^[a-zA-Z0-9]+(?:\.[a-zA-Z0-9]+)*@[a-zA-Z0-9]+(?:\.[a-zA-Z0-9]+)*$", ErrorMessage = "invalid email")]
        public string? email { get; set; } = "";

        //[StringLength(255, ErrorMessage = "Address maximum 255 characters")]
        public string? address { get; set; } = "";
    }
}
