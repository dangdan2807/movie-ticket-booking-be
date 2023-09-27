using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketBookingBe.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Required]
        [Column("user_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("full_name")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [Column("password")]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [Column("phone")]
        [StringLength(20)]
        [RegularExpression(@"^\+?(\d{10,15})$", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [StringLength(255)]
        [Column("address")]
        public string Address { get; set; }

        [Required]
        [Column("status")]
        public bool Status { get; set; } = true;

        [Column("create_at")]
        public DateTime? CreateAt { get; set; } = DateTime.Now;

        [Column("update_at")]
        public DateTime? UpdateAt { get; set; }

        [Column("update_by")]
        public int UpdateBy { get; set; }

        [NotMapped]
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
