﻿using Microsoft.EntityFrameworkCore;
using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;
using MovieTicketBookingBe.ViewModels;
using MySqlConnector;
using System.Data;

namespace MovieTicketBookingBe.Repositories
{
    public class ShortUrlRepository : IShortUrlRepository
    {
        private readonly AppDbContext _context;
        private readonly Serilog.ILogger _logger;
        private readonly IConfiguration _configuration;

        private readonly string _connectionString;

        public ShortUrlRepository(AppDbContext context, Serilog.ILogger logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<ShortUrl> CreateShortUrl(ShortUrl shortUrl)
        {
            if (shortUrl == null)
            {
                throw new ArgumentNullException("short url obj is required");
            }
            if (string.IsNullOrEmpty(shortUrl.LongUrl.Trim()))
            {
                throw new ArgumentNullException("long url is required");
            }
            if (string.IsNullOrEmpty(shortUrl.ShortUrlString))
            {
                throw new ArgumentNullException("short url is required");
            }
            if (string.IsNullOrEmpty(shortUrl.HashId.Trim()))
            {
                throw new ArgumentNullException("hash id is required");
            }
            if (shortUrl.UserId == 0)
            {
                throw new ArgumentNullException("user id is required");
            }
            shortUrl.CreatedAt = DateTime.UtcNow;

            await _context.ShortUrls.AddAsync(shortUrl);
            await _context.SaveChangesAsync();
            return shortUrl;
        }

        public async Task<ShortUrl> DeleteShortUrlByShortLink(string shortLink)
        {
            if (string.IsNullOrEmpty(shortLink.Trim()))
            {
                throw new ArgumentNullException("short link is required");
            }

            var shortUrl = await GetShortUrlByShortLink(shortLink);
            if (shortUrl == null)
            {
                throw new ArgumentNullException("short url not found");
            }
            shortUrl.IsDeleted = true;
            shortUrl.UpdateAt = DateTime.UtcNow;
            shortUrl.DeletedAt = DateTime.UtcNow;

            _context.Entry(shortUrl).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return shortUrl;
        }

        public async Task<ShortUrl> GetShortUrlByHashIdAndUserId(string hashId, int userId)
        {
            if (string.IsNullOrEmpty(hashId))
            {
                throw new ArgumentNullException("hash id is required");
            }

            ShortUrl shortUrl = new ShortUrl();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string queryString = "select * from short_urls where is_deleted = 0 and hash_id = @hashId " +
                    (userId == 0 ? "" : "and user_id = @userId ") + "limit 1";
                MySqlCommand cmd = new MySqlCommand(queryString, connection);
                cmd.Parameters.AddWithValue("@hashId", hashId);
                if (userId > 0)
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                }
                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        shortUrl = new ShortUrl
                        {
                            HashId = reader.GetString("hash_id"),
                            LongUrl = reader.GetString("long_url"),
                            ShortUrlString = reader.GetString("short_url"),
                            Status = reader.GetBoolean("status"),
                            ClickCount = reader.GetUInt64("click_count"),
                            UserId = reader.GetInt16("user_id"),
                            CreatedAt = reader.GetDateTime("create_at"),
                            UpdateAt = reader["update_at"] != DBNull.Value ? reader.GetDateTime("update_at") : null,
                        };
                    }
                }
                connection.Close();
            }

            return shortUrl;
        }

        public async Task<ShortUrl> GetShortUrlByLongLinkAndUserId(string longUrl, int userId)
        {
            if (string.IsNullOrEmpty(longUrl))
            {
                throw new ArgumentNullException("long url is required");
            }
            if (userId == 0)
            {
                throw new ArgumentNullException("user id is required");
            }

            ShortUrl shortUrlObj = null;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string queryString = "select * from short_urls where is_deleted = 0 and long_url = @longUrl and user_id = @userId limit 1";
                MySqlCommand cmd = new MySqlCommand(queryString, connection);
                cmd.Parameters.AddWithValue("@longUrl", longUrl);
                cmd.Parameters.AddWithValue("@userId", userId);
                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        shortUrlObj = new ShortUrl
                        {
                            HashId = reader.GetString("hash_id"),
                            LongUrl = reader.GetString("long_url"),
                            ShortUrlString = reader.GetString("short_url"),
                            Status = reader.GetBoolean("status"),
                            ClickCount = reader.GetUInt64("click_count"),
                            UserId = reader.GetInt16("user_id"),
                            CreatedAt = reader.GetDateTime("create_at"),
                            UpdateAt = reader["update_at"] != DBNull.Value ? reader.GetDateTime("update_at") : null,
                        };
                    }
                }
                connection.Close();
            }
            return shortUrlObj;
        }

        public async Task<ShortUrl> GetShortUrlByShortLink(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("short url is required");
            }

            ShortUrl shortUrlObj = null;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                string queryString = "select * from short_urls where is_deleted = 0 and short_url = @shortUrl limit 1";
                MySqlCommand cmd = new MySqlCommand(queryString, connection);
                cmd.Parameters.AddWithValue("@shortUrl", url);
                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        shortUrlObj = new ShortUrl
                        {
                            HashId = reader.GetString("hash_id"),
                            LongUrl = reader.GetString("long_url"),
                            ShortUrlString = reader.GetString("short_url"),
                            Status = reader.GetBoolean("status"),
                            ClickCount = reader.GetUInt64("click_count"),
                            UserId = reader.GetInt16("user_id"),
                            CreatedAt = reader.GetDateTime("create_at"),
                            UpdateAt = reader["update_at"] != DBNull.Value ? reader.GetDateTime("update_at") : null,
                        };
                    }
                }
                connection.Close();
            }

            return shortUrlObj;
        }

        public async Task<GetShortUrlsDTO> GetShortUrlsByUserId(int userId, PaginationVM paginationVM, string? keyword = "", bool? status = true)
        {
            List<ShortUrlDTO> shortUrls = new List<ShortUrlDTO>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                int skip = (paginationVM.currentPage - 1) * paginationVM.pageSize;
                int limit = paginationVM.pageSize;

                string procName = "Proc_GetShortUrls";
                MySqlCommand cmd = new MySqlCommand(procName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("in_Keyword", keyword);
                cmd.Parameters.AddWithValue("in_Status", status);
                cmd.Parameters.AddWithValue("in_Offset", skip);
                cmd.Parameters.AddWithValue("in_endRecord", limit);
                cmd.Parameters.AddWithValue("in_UserId", userId);
                cmd.Parameters.Add("out_TotalRecord", MySqlDbType.Int32);
                cmd.Parameters["out_TotalRecord"].Direction = ParameterDirection.Output;
                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ShortUrlDTO shortUrl = new ShortUrlDTO
                        {
                            hashId = reader.GetString("hash_id"),
                            longUrl = reader.GetString("long_url"),
                            shortUrl = reader.GetString("short_url"),
                            status = reader.GetBoolean("status"),
                            clickCount = reader.GetUInt64("click_count"),
                            userId = reader.GetInt16("user_id"),
                            createdAt = reader.GetDateTime("create_at"),
                            updateAt = reader["update_at"] != DBNull.Value ? reader.GetDateTime("update_at") : null,
                        };
                        shortUrls.Add(shortUrl);
                    }
                }
                connection.Close();
                int totalRecord = Convert.ToInt32(cmd.Parameters["out_TotalRecord"].Value);
                GetShortUrlsDTO getShortUrlsDTO = new GetShortUrlsDTO
                {
                    shortUrls = shortUrls,
                    pagination = new PaginationDTO
                    {
                        currentPage = paginationVM.currentPage,
                        pageSize = paginationVM.pageSize,
                        totalCount = totalRecord,
                    }
                };
                return getShortUrlsDTO;
            }
        }

        public async Task<GetShortUrlsDTO> GetShortUrlsForAdmin(PaginationVM paginationVM, string? keyword = "", bool? status = true)
        {
            List<ShortUrlDTO> shortUrls = new List<ShortUrlDTO>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                int skip = (paginationVM.currentPage - 1) * paginationVM.pageSize;
                int limit = paginationVM.pageSize;

                string procName = "Proc_GetShortUrls";
                MySqlCommand cmd = new MySqlCommand(procName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("in_Keyword", keyword);
                cmd.Parameters.AddWithValue("in_Status", status);
                cmd.Parameters.AddWithValue("in_Offset", skip);
                cmd.Parameters.AddWithValue("in_endRecord", limit);
                cmd.Parameters.AddWithValue("in_UserId", null);
                cmd.Parameters.Add("out_TotalRecord", MySqlDbType.Int32);
                cmd.Parameters["out_TotalRecord"].Direction = ParameterDirection.Output;
                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ShortUrlDTO shortUrl = new ShortUrlDTO
                        {
                            hashId = reader.GetString("hash_id"),
                            longUrl = reader.GetString("long_url"),
                            shortUrl = reader.GetString("short_url"),
                            status = reader.GetBoolean("status"),
                            clickCount = reader.GetUInt64("click_count"),
                            userId = reader.GetInt16("user_id"),
                            createdAt = reader.GetDateTime("create_at"),
                            updateAt = reader["update_at"] != DBNull.Value ? reader.GetDateTime("update_at") : null,
                        };
                        shortUrls.Add(shortUrl);
                    }
                }
                connection.Close();
                int totalRecord = Convert.ToInt32(cmd.Parameters["out_TotalRecord"].Value);
                GetShortUrlsDTO getShortUrlsDTO = new GetShortUrlsDTO
                {
                    shortUrls = shortUrls,
                    pagination = new PaginationDTO
                    {
                        currentPage = paginationVM.currentPage,
                        pageSize = paginationVM.pageSize,
                        totalCount = totalRecord,
                    }
                };
                return getShortUrlsDTO;
            }
        }

        public async Task<ShortUrl> UpdateShortUrlByShortLink(ShortUrl shortUrl)
        {
            if (shortUrl == null)
            {
                throw new ArgumentNullException("short url obj is required");
            }
            if (string.IsNullOrEmpty(shortUrl.LongUrl.Trim()))
            {
                throw new ArgumentNullException("long url is required");
            }
            if (string.IsNullOrEmpty(shortUrl.ShortUrlString.Trim()))
            {
                throw new ArgumentNullException("short url is required");
            }
            if (string.IsNullOrEmpty(shortUrl.HashId.Trim()))
            {
                throw new ArgumentNullException("hash id is required");
            }
            if (shortUrl.UserId == 0)
            {
                throw new ArgumentNullException("user id is required");
            }
            shortUrl.UpdateAt = DateTime.UtcNow;

            _context.Entry(shortUrl).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return shortUrl;
        }
    }
}
