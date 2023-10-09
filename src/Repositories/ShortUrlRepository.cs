using MovieTicketBookingBe.src.Models;
using MovieTicketBookingBe.src.Models.DTO;
using MovieTicketBookingBe.src.ViewModels;
using MySqlConnector;
using System.Data;

namespace MovieTicketBookingBe.src.Repositories
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
            _logger.Information("Create short url repo");
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

        public Task<ShortUrl> DeleteShortUrlById(int hashId)
        {
            throw new NotImplementedException();
        }

        public Task<ShortUrl> GetShortUrlByLongUrl(string longUrl)
        {
            throw new NotImplementedException();
        }

        public Task<ShortUrl> GetShortUrlByShortUrl(int shortUrl)
        {
            throw new NotImplementedException();
        }

        public Task<GetShortUrlsDTO> GetShortUrlsByUserId(int userId, PaginationVM paginationDVM, string? keyword = "", bool? status = true)
        {
            throw new NotImplementedException();
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

        public Task<ShortUrl> UpdateShortUrl(ShortUrl shortUrl)
        {
            throw new NotImplementedException();
        }
    }
}
