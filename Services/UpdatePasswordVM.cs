using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.Services
{
    public class UpdatePasswordVM
    {
        [Required]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Current password must be between 6 - 255 characters")]
        public string oldPassword { get; set; } = "";

        [Required]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "New password must be between 6 - 255 characters")]
        public string newPassword { get; set; } = "";

        [Required]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Confirm new password must be between 6 - 255 characters")]
        public string confirmNewPassword { get; set; } = "";
    }
}
