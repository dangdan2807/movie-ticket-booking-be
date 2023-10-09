using MovieTicketBookingBe.src.Models;
using MovieTicketBookingBe.src.Models.DTO;
using MovieTicketBookingBe.src.Repositories;
using MovieTicketBookingBe.src.ViewModels;
using shortid;
using System.Security.Cryptography;
using System.Text;

namespace MovieTicketBookingBe.src.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IShortUrlRepository _shortUrlRepository;
        private readonly IUserRepository _userRepository;
        private readonly Serilog.ILogger _logger;

        public ShortUrlService(IShortUrlRepository shortUrlRepository, IUserRepository userRepository, Serilog.ILogger logger)
        {
            _shortUrlRepository = shortUrlRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        private string createHashId()
        {
            string input = DateTime.UtcNow.ToString();
            string hash = "";
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    builder.Append(data[i].ToString("x2"));
                }

                hash = builder.ToString();
            }
            return hash;
        }

        public async Task<ShortUrlDTO> CreateShortUrl(int userId, CreateShortUrlVM shortUrlVM)
        {
            if (shortUrlVM == null)
            {
                throw new ArgumentNullException("shortUrl is null");
            }
            if (userId <= 0)
            {
                throw new Exception("userId is invalid");
            }

            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var shortUrlstring = ShortId.Generate();
            var hashId = createHashId();

            var shortUrl = new ShortUrl
            {
                LongUrl = shortUrlVM.longUrl,
                ShortUrlString = string.IsNullOrEmpty(shortUrlVM.shortUrl) ? shortUrlstring : shortUrlVM.shortUrl,
                HashId = hashId,
                UserId = userId,
            };

            var saveShortUrl = await _shortUrlRepository.CreateShortUrl(shortUrl);
            if (saveShortUrl == null)
            {
                throw new Exception("Create shortUrl failed");
            }

            return new ShortUrlDTO
            {
                hashId = saveShortUrl.HashId,
                longUrl = saveShortUrl.LongUrl,
                shortUrl = saveShortUrl.ShortUrlString,
                status = saveShortUrl.Status,
                userId = saveShortUrl.UserId,
                clickCount = saveShortUrl.ClickCount,
                createdAt = saveShortUrl.CreatedAt,
                updateAt = saveShortUrl.UpdateAt,
            };
        }

        public async Task<GetShortUrlsDTO> GetShortUrlsForAdmin(PaginationVM paginationVM, string? keyword = "", bool? status = true)
        {
            if (paginationVM.currentPage <= 0 || paginationVM.pageSize <= 0)
            {
                throw new Exception("Page is invalid");
            }
            if (paginationVM.sort.Equals("ASC") && paginationVM.sort.Equals("DESC"))
            {
                throw new Exception("Sort is invalid");
            }
            return await _shortUrlRepository.GetShortUrlsForAdmin(paginationVM, keyword, status);
        }
    }
}
