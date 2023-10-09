using MovieTicketBookingBe.src.Models;
using MovieTicketBookingBe.src.Models.DTO;
using MovieTicketBookingBe.src.ViewModels;

namespace MovieTicketBookingBe.src.Repositories
{
    public interface IShortUrlRepository
    {
        Task<GetShortUrlsDTO> GetShortUrlsForAdmin(PaginationVM paginationDVM, string? keyword = "", bool? status = true);
        Task<GetShortUrlsDTO> GetShortUrlsByUserId(int userId, PaginationVM paginationDVM, string? keyword = "", bool? status = true);
        Task<ShortUrl> GetShortUrlByShortUrl(int shortUrl);
        Task<ShortUrl> GetShortUrlByLongUrl(string longUrl);
        Task<ShortUrl> CreateShortUrl(ShortUrl shortUrl);
        Task<ShortUrl> UpdateShortUrl(ShortUrl shortUrl);
        Task<ShortUrl> DeleteShortUrlById(int hashId);
    }
}
