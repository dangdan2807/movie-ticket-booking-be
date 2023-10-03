using System.Text.Json.Serialization;

namespace MovieTicketBookingBe.src.Models.DTO
{
    public class UserDTO
    {
        public int id { get; set; }
        public string fullName { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public bool status { get; set; } = true;
        public DateTime? createAt { get; set; } = DateTime.Now;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<RoleDTO>? roles { get; set; } = null;
        //public DateTime? UpdateAt { get; set; }
        //public int UpdateBy { get; set; }
    }
}
