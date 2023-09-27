﻿using System.ComponentModel.DataAnnotations.Schema;
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
        [Column("role_code")]
        [StringLength(50)]
        public string RoleCode { get; set; }

        [Required]
        [Column("role_name")]
        [StringLength(100)]
        public string RoleName { get; set; }

        [Column("description")]
        [StringLength(255)]
        public string Description { get; set; } = "";

        [Required]
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
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
