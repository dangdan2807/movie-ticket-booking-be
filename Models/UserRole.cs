using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketBookingBe.Models
{
    [Table("user_role")]
    public class UserRole
    {
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }

        [ForeignKey("user_id")]
        public virtual User? User { get; set; }

        [ForeignKey("role_id")]
        public virtual Role? Role { get; set; }
    }
}
