using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.ViewModels
{
    public class UpdateRoleVM
    {
        [StringLength(100)]
        public string roleName { get; set; }
        [StringLength(100)]
        public string roleCode { get; set; }
        [StringLength(255)]
        public string description { get; set; }
        public bool? status { get; set; } = true;
    }
}
