using System.ComponentModel.DataAnnotations;

namespace MovieTicketBookingBe.src.ViewModels
{
    public class CreateShortUrlVM
    {
        [Required(ErrorMessage = "long url is required")]
        [Url(ErrorMessage = "long url is invalid")]
        public string longUrl { get; set; }

        //[Url(ErrorMessage = "short url is invalid")]
        public string? shortUrl { get; set; } = "";
    }
}
