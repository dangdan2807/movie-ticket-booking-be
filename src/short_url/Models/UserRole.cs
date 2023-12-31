﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketBookingBe.Models
{
    [Table("user_role")]
    public class UserRole
    {
        [Column("user_id")]
        [ForeignKey("user_id")]
        public int UserId { get; set; }

        [Column("role_id")]
        [ForeignKey("role_id")]
        public int RoleId { get; set; }

        [NotMapped]
        public virtual User? User { get; set; }

        [NotMapped]
        public virtual Role? Role { get; set; }
    }
}
