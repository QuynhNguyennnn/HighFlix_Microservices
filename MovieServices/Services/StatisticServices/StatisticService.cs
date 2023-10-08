using MovieServices.DAOs;
using MovieServices.Models;

namespace MovieServices.Services.StatisticServices
{
    public class StatisticService : IStatisticService
    {
        public List<Statistic> StatisticMovieByDate(int movieId,  DateTime statisticDate) => StatisticDAO.StatisticMovieByDate(movieId, statisticDate);
    }
}
