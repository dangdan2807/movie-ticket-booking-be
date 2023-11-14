using ShortUrlBachEnd.Models.DTO;

namespace ShortUrlBachEnd.Services
{
    public interface IStatisticsService
    {
        Task<StatisticsBaseDTO> StatisticsBase();
    }
}
