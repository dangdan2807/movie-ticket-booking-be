using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.ViewModels
{
    public class CreateRoleVM
    {
        [StringLength(100)]
        public string roleName { get; set; }
        [StringLength(100)]
        public string roleCode { get; set; }
        public bool? status { get; set; } = true;
    }
}
