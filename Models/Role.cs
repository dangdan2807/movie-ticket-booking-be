using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.Models
{
    [Table("roles")]
    public class Role
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("role_id")]
        public int RoleId { get; set; }

        [Required]
        [Column("role_name")]
        [StringLength(100)]
        public string RoleName { get; set; }

        [Required]
        [Column("status")]
        public bool status { get; set; } = true;

        //[ForeignKey("UserRole")]
        //[Column("user_id")]
        //public int? userId { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
