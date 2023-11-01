using MovieTicketBookingBe.Models.DTO;
using MovieTicketBookingBe.ViewModels;

namespace MovieTicketBookingBe.Services
{
    public interface IShortUrlService
    {
        Task<ShortUrlDTO> CreateShortUrl(int userId, CreateShortUrlVM CreateShortUrlVM);
        Task<ShortUrlDTO> UpdateShortUrlByShortLink(string shortLink, int userIdInt, List<string> roles, UpdateShortUrlVM updateShortUrlVM);
        Task<ShortUrlDTO> UpdateClickCountByShortLink(string shortLink);
        Task<GetShortUrlsDTO> GetShortUrlsForAdmin(PaginationVM paginationVM, string? keyword = "", bool? status = true);
        Task<GetShortUrlsDTO> GetShortUrlsForUserId(int userId, PaginationVM paginationVM, string? keyword = "", bool? status = true);
        Task<ShortUrlDTO> GetShortUrlByHashIdAndUserId(string hashId, int userId, List<string> roles);
        Task<ShortUrlDTO> GetShortUrlByShortLink(string shortLink);
        Task<ShortUrlDTO> DeleteShortUrlByShortLink(string shortLink, int userId, List<string> roles);
    }
}
