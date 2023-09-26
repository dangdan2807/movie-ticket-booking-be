using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.ViewModels
{
    public class RegisterVM
    {
        //[Required]
        //[StringLength(50, MinimumLength = 3, ErrorMessage = "Fullname must be between 3 - 50 characters")]
        public string? fullname { get; set; }

        //[Required]
        //[StringLength(15, MinimumLength = 10, ErrorMessage = "Phone must be 10 - 15 characters")]
        //[RegularExpression(@"^\+?(\d{10,15})$", ErrorMessage = "Invalid phone number")]
        public string? phone { get; set; }

        public string? address { get; set; }

        //[Required]
        //[StringLength(255, MinimumLength = 3, ErrorMessage = "Username must be between 3 - 255 characters")]
        public string? password { get; set; }
    }
}
