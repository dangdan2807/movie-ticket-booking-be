using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketBookingBe.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; } = true;
        public DateTime? CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
        public int UpdateBy { get; set; }
    }
}
