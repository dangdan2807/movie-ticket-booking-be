using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.ViewModels
{
    public class CreateShortUrlVM
    {
        public string? title { get; set; } = "";

        [Required(ErrorMessage = "long url is required")]
        [Url(ErrorMessage = "long url is invalid")]
        [DefaultValue("https://chat.openai.com/c/94d522b9-eb3d-452a-bd03-692d26ffb641")]
        public string longUrl { get; set; } = "";

        //[Url(ErrorMessage = "short url is invalid")]
        [DefaultValue("bGeKmcxr_")]
        public string? shortUrl { get; set; } = "";
    }
}
