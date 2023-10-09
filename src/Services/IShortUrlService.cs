using MovieTicketBookingBe.src.Models.DTO;
using MovieTicketBookingBe.src.ViewModels;

namespace MovieTicketBookingBe.src.Services
{
    public interface IShortUrlService
    {
        Task<ShortUrlDTO> CreateShortUrl(int userId, CreateShortUrlVM shortUrlVM);
        Task<GetShortUrlsDTO> GetShortUrlsForAdmin(PaginationVM paginationVM, string? keyword = "", bool? status = true);

    }
}
