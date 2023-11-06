using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.ViewModels
{
    public class UpdateShortUrlVM
    {
        [Required(ErrorMessage = "title is required")]
        public string title { get; set; } = "";

        [Required(ErrorMessage = "long url is required")]
        [Url(ErrorMessage = "long url is invalid")]
        [DefaultValue("https://chat.openai.com/c/94d522b9-eb3d-452a-bd03-692d26ffb641")]
        public string longUrl { get; set; } = "";

        [Required(ErrorMessage = "short url is required")]
        [DefaultValue("bGeKmcxr_")]
        public string shortUrl { get; set; } = "";

        public bool? status { get; set; } = true;
    }
}
