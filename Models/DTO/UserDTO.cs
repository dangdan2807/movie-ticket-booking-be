using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketBookingBe.Models.DTO
{
    public class UserDTO
    {
        public int id { get; set; }
        public string fullName { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public bool status { get; set; } = true;
        public DateTime? CreateAt { get; set; } = DateTime.Now;
        //public DateTime? UpdateAt { get; set; }
        //public int UpdateBy { get; set; }
    }
}
