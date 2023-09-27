using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.Models.DTO
{
    public class RoleDTO
    {
        public int roleId { get; set; }
        public string roleName { get; set; }
        public string roleCode { get; set; }
        public string description { get; set; }
        public bool status { get; set; } = true;
        public DateTime? createAt { get; set; } = DateTime.Now;
        //public DateTime? updateAt { get; set; }
        //public int? createBy { get; set; }
        //public int? updateBy { get; set; }
    }
}
