using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.src.ViewModels
{
    public class CreateRoleVM
    {
        [Required(ErrorMessage = "Role name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Role name must be between 3 - 100 characters")]
        public string roleName { get; set; } = "";

        [Required(ErrorMessage = "Role code is required")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Role code must be between 1 - 50 characters")]
        public string roleCode { get; set; } = "";

        [StringLength(255, ErrorMessage = "Description maximum 255 characters")]
        public string description { get; set; } = "";

        //[RegularExpression("true|false", ErrorMessage = "Status must be boolean")]
        public bool? status { get; set; } = true;
    }
}
