using MovieServices.Models;

namespace MovieServices.Services.StatisticServices
{
    public interface IStatisticService
    {
        List<Statistic> StatisticMovieByDate(int movieId, DateTime statisticDate);
    }
}
