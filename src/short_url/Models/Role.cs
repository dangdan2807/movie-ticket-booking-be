using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.Models
{
    [Table("roles")]
    public class Role
    {
        [Key]
        [Required(ErrorMessage = "role id is required")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("role_id")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "role code is required")]
        [Column("role_code")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Role name must be 1 - 50 characters")]
        public string RoleCode { get; set; } = "";

        [Required(ErrorMessage = "role name is required")]
        [Column("role_name")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Role name must be 3 - 100 characters")]
        public string RoleName { get; set; } = "";

        [Required(ErrorMessage = "priority is required")]
        [Column("priority")]
        public int Priority { get; set; } = 5;

        [Column("description")]
        [StringLength(255, ErrorMessage = "Description maximum 255 characters")]
        public string Description { get; set; } = "";

        [Required(ErrorMessage = "status is required")]
        [Column("status")]
        public bool Status { get; set; } = true;

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [Column("create_at")]
        public DateTime? CreateAt { get; set; } = DateTime.Now;

        [Column("update_at")]
        public DateTime? UpdateAt { get; set; }

        [Column("create_by")]
        public int? CreateBy { get; set; }

        [Column("update_by")]
        public int? UpdateBy { get; set; }

        [Column("delete_at")]
        public DateTime? DeleteAt { get; set; }

        [NotMapped]
        public ICollection<UserRole>? UserRoles { get; set; }
    }
}
