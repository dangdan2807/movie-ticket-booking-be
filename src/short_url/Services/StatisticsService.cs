using MovieTicketBookingBe.Repositories;
using MovieTicketBookingBe.Services;
using ShortUrlBachEnd.Models.DTO;

namespace ShortUrlBachEnd.Services
{

    public class StatisticsService : IStatisticsService
    {
        private readonly Serilog.ILogger _logger;
        private readonly IShortUrlRepository _shortUrlRepository;
        private readonly IUserRepository _userRepository;

        public StatisticsService(Serilog.ILogger logger, IShortUrlRepository shortUrlRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _shortUrlRepository = shortUrlRepository;
            _userRepository = userRepository;
        }

        public async Task<StatisticsBaseDTO> StatisticsBase()
        {
            int totalUsers = await _userRepository.TotalUsers();
            int totalShortUrls = await _shortUrlRepository.TotalShortUrls();

            return new StatisticsBaseDTO
            {
                totalUsers = totalUsers,
                totalShortUrls = totalShortUrls,
            };
        }
    }
}
