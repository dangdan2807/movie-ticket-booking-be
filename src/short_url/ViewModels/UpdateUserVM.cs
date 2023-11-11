using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.ViewModels
{
    public class UpdateUserVM
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Full name must be between 3 - 100 characters")]
        public string fullName { get; set; } = "";

        [Required(ErrorMessage = "Email is required")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Email must be between 6 and 255 characters")]
        [RegularExpression(@"^[a-zA-Z0-9]+(?:\.[a-zA-Z0-9]+)*@[a-zA-Z0-9]+(?:\.[a-zA-Z0-9]+)*$", ErrorMessage = "invalid email")]
        public string email { get; set; } = "";

        [StringLength(255, ErrorMessage = "Address maximum 255 characters")]
        public string address { get; set; } = "";

        public bool status { get; set; } = true;

        [Required(ErrorMessage = "RoleIds is required")]
        [MinLength(1, ErrorMessage = "RoleIds must have at least 1 role")]
        public List<int> roleIds { get; set; } = new List<int>();
    }
}
