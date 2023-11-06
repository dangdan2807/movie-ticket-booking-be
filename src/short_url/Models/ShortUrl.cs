using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketBookingBe.Models
{
    [Table("short_urls")]
    public class ShortUrl
    {
        [Key]
        [Required]
        [Column("hash_id")]
        [StringLength(256)]
        public string HashId { get; set; } = null!;

        [Required]
        [Column("title")]
        [StringLength(256)]
        public string Title { get; set; } = null!;

        [Required]
        [Column("long_url")]
        [StringLength(256)]
        public string LongUrl { get; set; } = null!;

        [Required]
        [Column("short_url")]
        [StringLength(256)]
        public string ShortUrlString { get; set; } = null!;

        [Required]
        [Column("click_count")]
        public ulong ClickCount { get; set; } = 0;

        [Required]
        [Column("status")]
        public bool Status { get; set; } = true;

        [Required]
        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [Required]
        [Column("user_id")]
        [ForeignKey("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("create_at")]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        [Column("update_at")]
        public DateTime? UpdateAt { get; set; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        [NotMapped]
        public User? User { get; set; }
    }
}
