﻿using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;
using MovieTicketBookingBe.ViewModels;

namespace MovieTicketBookingBe.Repositories
{
    public interface IShortUrlRepository
    {
        Task<GetShortUrlsDTO> GetShortUrlsForAdmin(PaginationVM paginationVM, string? keyword = "", bool? status = true);
        Task<GetShortUrlsDTO> GetShortUrlsByUserId(int userId, PaginationVM paginationVM, string? keyword = "", bool? status = true);
        Task<ShortUrl> GetShortUrlByHashIdAndUserId(string hardId, int userId);
        Task<ShortUrl> GetShortUrlByShortLink(string url);
        Task<ShortUrl> GetShortUrlByLongLinkAndUserId(string longUrl, int userId);
        Task<ShortUrl> CreateShortUrl(ShortUrl shortUrl);
        Task<ShortUrl> UpdateShortUrlByShortLink(ShortUrl shortUrlObj);
        Task<ShortUrl> DeleteShortUrlByShortLink(string shortLink);
    }
}