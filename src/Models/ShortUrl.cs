using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.src.Models
{
    [Table("short_urls")]
    public class ShortUrl
    {
        [Key]
        [Required]
        [Column("hash_id")]
        [StringLength(255)]
        public string HashId { get; set; } = null!;

        [Required]
        [Column("long_url")]
        [StringLength(255)]
        public string LongUrl { get; set; } = null!;

        [Required]
        [Column("short_url")]
        [StringLength(255)]
        public string ShortUrlString { get; set; } = null!;

        [Required]
        [Column("create_at")]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        [Column("update_at")]
        public DateTime? UpdateAt { get; set; }

        [Required]
        [Column("status")]
        public bool Status { get; set; } = true;

        [Required]
        [Column("user_id")]
        [ForeignKey("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("click_count")]
        public ulong ClickCount { get; set; } = 0;

        [NotMapped]
        public User? User { get; set; }
    }
}
