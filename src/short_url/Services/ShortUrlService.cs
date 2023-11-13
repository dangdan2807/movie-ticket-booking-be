using MovieTicketBookingBe.Contraints;
using MovieTicketBookingBe.Models;
using MovieTicketBookingBe.Models.DTO;
using MovieTicketBookingBe.Repositories;
using MovieTicketBookingBe.ViewModels;
using shortid;
using System.Security.Cryptography;
using System.Text;

namespace MovieTicketBookingBe.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IShortUrlRepository _shortUrlRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly Serilog.ILogger _logger;

        public ShortUrlService(IShortUrlRepository shortUrlRepository, IUserRepository userRepository, IRoleRepository roleRepository, Serilog.ILogger logger)
        {
            _shortUrlRepository = shortUrlRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
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

        public async Task<ShortUrlDTO> CreateShortUrl(int userId, CreateShortUrlVM CreateShortUrlVM)
        {
            if (CreateShortUrlVM == null)
            {
                throw new Exception("shortUrl is null");
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

            if (string.IsNullOrEmpty(CreateShortUrlVM.longUrl))
            {
                throw new Exception("long url is required");
            }

            var shortUrlByLongUrl = await _shortUrlRepository.GetShortUrlByLongLinkAndUserId(CreateShortUrlVM.longUrl, userId);
            if (shortUrlByLongUrl != null)
            {
                throw new Exception("Long url is exist");
            }

            var shortUrlstring = ShortId.Generate();
            var shortUrlObj = await _shortUrlRepository.GetShortUrlByShortLink(shortUrlstring);

            while (shortUrlObj != null)
            {
                shortUrlstring = ShortId.Generate();
                shortUrlObj = await _shortUrlRepository.GetShortUrlByShortLink(shortUrlstring);
            }
            if (string.IsNullOrEmpty(CreateShortUrlVM.title))
            {
                CreateShortUrlVM.title = shortUrlstring;
            }

            var hashId = createHashId();

            var shortUrl = new ShortUrl
            {
                LongUrl = CreateShortUrlVM.longUrl,
                Title = CreateShortUrlVM.title,
                ShortUrlString = string.IsNullOrEmpty(CreateShortUrlVM.shortUrl) ? shortUrlstring : CreateShortUrlVM.shortUrl,
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
                title = saveShortUrl.Title,
                longUrl = saveShortUrl.LongUrl,
                shortUrl = saveShortUrl.ShortUrlString,
                status = saveShortUrl.Status,
                userId = saveShortUrl.UserId,
                clickCount = saveShortUrl.ClickCount,
                createdAt = saveShortUrl.CreatedAt,
                updateAt = saveShortUrl.UpdateAt,
            };
        }

        public async Task<GetShortUrlsDTO> GetShortUrlsForAdmin(PaginationVM paginationVM, string? keyword = "", DateTime? startDate = null, DateTime? endDate = null, bool? status = true)
        {
            if (paginationVM.currentPage <= 0 || paginationVM.pageSize <= 0)
            {
                throw new Exception("Page is invalid");
            }
            if (paginationVM.sort.Equals("ASC") && paginationVM.sort.Equals("DESC"))
            {
                throw new Exception("Sort is invalid");
            }
            return await _shortUrlRepository.GetShortUrlsForAdmin(paginationVM, keyword, startDate, endDate, status);
        }

        public async Task<ShortUrlDTO> GetShortUrlByHashIdAndUserId(string hashId, int userId, List<string> roles)
        {
            if (string.IsNullOrEmpty(hashId))
            {
                throw new ArgumentNullException("hash id is required");
            }
            if (roles.Count <= 0)
            {
                throw new ArgumentNullException("role is required");
            }

            var role = "";
            var priority = -1;
            roles.Select(async item => await _roleRepository.GetRoleByCode(item)).Select(item =>
            {
                if (item.Result == null)
                {
                    throw new Exception("Role not found");
                }
                if (priority == -1 || item.Result.Priority < priority)
                {
                    priority = item.Result.Priority;
                    role = item.Result.RoleCode;
                }
                return item;
            }).ToList();

            ShortUrl shortUrl = new ShortUrl();
            if (role.Equals(RolesContraint.ADMIN) || role.Equals(RolesContraint.MOD))
            {
                shortUrl = await _shortUrlRepository.GetShortUrlByHashIdAndUserId(hashId, 0);
            }
            else
            {
                shortUrl = await _shortUrlRepository.GetShortUrlByHashIdAndUserId(hashId, userId);
            }
            if (shortUrl == null)
            {
                throw new Exception("Short url not found");
            }

            return new ShortUrlDTO
            {
                hashId = shortUrl.HashId,
                title = shortUrl.Title,
                longUrl = shortUrl.LongUrl,
                shortUrl = shortUrl.ShortUrlString,
                status = shortUrl.Status,
                userId = shortUrl.UserId,
                clickCount = shortUrl.ClickCount,
                createdAt = shortUrl.CreatedAt,
                updateAt = shortUrl.UpdateAt,
            };
        }

        public async Task<ShortUrlDTO> GetShortUrlByShortLink(string shortLink)
        {
            if (string.IsNullOrEmpty(shortLink))
            {
                throw new ArgumentNullException("short url is required");
            }

            var shortUrlObj = await _shortUrlRepository.GetShortUrlByShortLink(shortLink);

            if (shortUrlObj == null)
            {
                throw new Exception("Short url not found");
            }
            return new ShortUrlDTO
            {
                hashId = shortUrlObj.HashId,
                title = shortUrlObj.Title,
                longUrl = shortUrlObj.LongUrl,
                shortUrl = shortUrlObj.ShortUrlString,
                status = shortUrlObj.Status,
                userId = shortUrlObj.UserId,
                clickCount = shortUrlObj.ClickCount,
                createdAt = shortUrlObj.CreatedAt,
                updateAt = shortUrlObj.UpdateAt,
            };
        }

        public async Task<GetShortUrlsDTO> GetShortUrlsForUserId(int userId, PaginationVM paginationVM, string? keyword = "", DateTime? startDate = null, DateTime? endDate = null, bool? status = true)
        {
            if (paginationVM.currentPage <= 0 || paginationVM.pageSize <= 0)
            {
                throw new Exception("Page is invalid");
            }
            if (paginationVM.sort.Equals("ASC") && paginationVM.sort.Equals("DESC"))
            {
                throw new Exception("Sort is invalid");
            }
            if (userId <= 0)
            {
                throw new Exception("userId is invalid");
            }
            return await _shortUrlRepository.GetShortUrlsByUserId(userId, paginationVM, keyword, startDate, endDate, status);
        }

        public async Task<ShortUrlDTO> UpdateShortUrlByShortLink(string shortLink, int userId, List<string> roles, UpdateShortUrlVM updateShortUrlVM)
        {
            if (string.IsNullOrEmpty(shortLink))
            {
                throw new ArgumentNullException("short url is required");
            }
            if (updateShortUrlVM == null)
            {
                throw new ArgumentNullException("short url is null");
            }

            var shortUrl = await _shortUrlRepository.GetShortUrlByShortLink(shortLink);
            if (shortUrl == null)
            {
                throw new Exception("Short url not found");
            }

            var role = "";
            var priority = -1;
            roles.Select(async item => await _roleRepository.GetRoleByCode(item)).Select(item =>
            {
                if (item.Result == null)
                {
                    throw new Exception("Role not found");
                }
                if (priority == -1 || item.Result.Priority < priority)
                {
                    priority = item.Result.Priority;
                    role = item.Result.RoleCode;
                }
                return item;
            }).ToList();
            if (shortUrl.UserId != userId)
            {
                switch (role)
                {
                    case RolesContraint.ADMIN:
                    case RolesContraint.MOD:
                        break;
                    case RolesContraint.MEMBER:
                    case RolesContraint.VIP:
                    default:
                        throw new UnauthorizedAccessException("You don't have permission to update this short url");
                }
            }

            var shortUrlByLongUrl = await _shortUrlRepository.GetShortUrlByLongLinkAndUserId(updateShortUrlVM.longUrl, userId);
            if (shortUrlByLongUrl != null && !shortUrlByLongUrl.ShortUrlString.Equals(shortLink))
            {
                throw new Exception("Long url is exist");
            }

            shortUrl.LongUrl = updateShortUrlVM.longUrl;
            shortUrl.Title = updateShortUrlVM.title;
            shortUrl.ShortUrlString = updateShortUrlVM.shortUrl;
            shortUrl.Status = updateShortUrlVM.status.Value;

            var updateShortUrl = await _shortUrlRepository.UpdateShortUrlByShortLink(shortUrl);
            if (updateShortUrl == null)
            {
                throw new Exception("Update shortUrl failed");
            }

            return new ShortUrlDTO
            {
                hashId = updateShortUrl.HashId,
                title = updateShortUrl.Title,
                longUrl = updateShortUrl.LongUrl,
                shortUrl = updateShortUrl.ShortUrlString,
                status = updateShortUrl.Status,
                userId = updateShortUrl.UserId,
                clickCount = updateShortUrl.ClickCount,
                createdAt = updateShortUrl.CreatedAt,
                updateAt = updateShortUrl.UpdateAt,
            };
        }

        public async Task<ShortUrlDTO> UpdateClickCountByShortLink(string shortLink)
        {
            if (string.IsNullOrEmpty(shortLink))
            {
                throw new ArgumentNullException("short url is required");
            }

            var shortUrl = await _shortUrlRepository.GetShortUrlByShortLink(shortLink);
            if (shortUrl == null)
            {
                throw new Exception("Short url not found");
            }
            shortUrl.ClickCount = shortUrl.ClickCount + 1;

            var updateShortUrl = await _shortUrlRepository.UpdateShortUrlByShortLink(shortUrl);
            if (updateShortUrl == null)
            {
                throw new Exception("Update shortUrl failed");
            }

            return new ShortUrlDTO
            {
                hashId = updateShortUrl.HashId,
                title = updateShortUrl.Title,
                longUrl = updateShortUrl.LongUrl,
                shortUrl = updateShortUrl.ShortUrlString,
                status = updateShortUrl.Status,
                userId = updateShortUrl.UserId,
                clickCount = updateShortUrl.ClickCount,
                createdAt = updateShortUrl.CreatedAt,
                updateAt = updateShortUrl.UpdateAt,
            };
        }

        public async Task<ShortUrlDTO> DeleteShortUrlByShortLink(string shortLink, int userId, List<string> roles)
        {
            if (string.IsNullOrEmpty(shortLink))
            {
                throw new ArgumentNullException("short url is required");
            }
            if (userId <= 0)
            {
                throw new Exception("userId is invalid");
            }
            if (roles.Count <= 0)
            {
                throw new ArgumentNullException("role is required");
            }

            var role = "";
            var priority = -1;
            roles.Select(async item => await _roleRepository.GetRoleByCode(item)).Select(item =>
            {
                if (item.Result == null)
                {
                    throw new Exception("Role not found");
                }
                if (priority == -1 || item.Result.Priority < priority)
                {
                    priority = item.Result.Priority;
                    role = item.Result.RoleCode;
                }
                return item;
            }).ToList();
            _logger.Information("role: " + role);

            var shortUrl = await _shortUrlRepository.GetShortUrlByShortLink(shortLink);
            if (shortUrl == null)
            {
                throw new Exception("Short url not found");
            }

            if (shortUrl.UserId != userId)
            {
                switch (role)
                {
                    case RolesContraint.VIP:
                    case RolesContraint.MEMBER:
                        throw new UnauthorizedAccessException("You don't have permission to delete this short url");
                }
            }

            var deleteShortUrl = await _shortUrlRepository.DeleteShortUrlByShortLink(shortLink);
            if (deleteShortUrl == null)
            {
                throw new Exception("Delete shortUrl failed");
            }

            return new ShortUrlDTO
            {
                hashId = deleteShortUrl.HashId,
                title = deleteShortUrl.Title,
                longUrl = deleteShortUrl.LongUrl,
                shortUrl = deleteShortUrl.ShortUrlString,
                status = deleteShortUrl.Status,
                userId = deleteShortUrl.UserId,
                clickCount = deleteShortUrl.ClickCount,
                createdAt = deleteShortUrl.CreatedAt,
                updateAt = deleteShortUrl.UpdateAt,
            };
        }

        public async Task<int> CountShortUrlsByUserId(int userId)
        {
            if (userId <= 0)
            {
                throw new Exception("userId is invalid");
            }

            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            return await _shortUrlRepository.CountShortUrlsByUserId(userId);
        }
    }
}
