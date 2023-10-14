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
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Fullname must be between 3 - 100 characters")]
        public string FullName { get; set; } = null!;

        [Required]
        [Column("password")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Password must be between 6 - 255 characters")]
        public string Password { get; set; } = null!;

        [Required]
        [Column("phone")]
        [StringLength(20)]
        [RegularExpression(@"^\+?(\d{10,12})$", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; } = null!;

        [StringLength(255, ErrorMessage = "Address maximum 255 characters")]
        [Column("address")]
        public string Address { get; set; } = null!;

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
        public ICollection<UserRole>? UserRoles { get; set; }

        [NotMapped]
        public ICollection<ShortUrl>? ShortUrls { get; set; }
    }
}
