using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.ViewModels
{
    public class UpdateRoleVM
    {
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Role name must be between 3 - 100 characters")]
        public string roleName { get; set; } = "";

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Role code must be between 3 - 50 characters")]
        public string roleCode { get; set; } = "";

        [StringLength(255, ErrorMessage = "Description maximum 255 characters")]
        public string description { get; set; } = "";

        //[RegularExpression("true|false", ErrorMessage = "Status must be boolean")]
        public bool? status { get; set; } = true;
    }
}
