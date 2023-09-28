using MovieTicketBookingBe.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.ViewModels
{
    public class UpdateUserVM
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Fullname must be between 3 - 100 characters")]
        public string fullName { get; set; } = "";

        [StringLength(12, MinimumLength = 10, ErrorMessage = "Phone must be 10 - 12 characters")]
        [RegularExpression(@"^0[3|5|7|8|9]\d{8,10}$", ErrorMessage = "Phone must be numeric")]
        public string phone { get; set; } = "";

        [StringLength(255, ErrorMessage = "Address maximum 255 characters")]
        public string address { get; set; } = "";

        //[RegularExpression("true|false", ErrorMessage = "Status must be boolean")]
        public bool status { get; set; } = true;

        [Required(ErrorMessage = "RoleIds is required")]
        [MinLength(1, ErrorMessage = "RoleIds must have at least 1 role")]
        public List<int> roleIds { get; set; } = new List<int>();
    }
}
