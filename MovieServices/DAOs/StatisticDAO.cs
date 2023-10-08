using MovieServices.Models;

namespace MovieServices.DAOs
{
    public class StatisticDAO
    {
        public static Statistic StatisticMovieByDate(int movieId, DateTime statisticDate)
        {
            Statistic movieStatistic = new Statistic();

            try
            {
                using (var context = new HighFlixV2Context())
                {
                    movieStatistic = context.Statistics.SingleOrDefault(mv => (mv.MovieId == movieId) && (mv.Date == statisticDate));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return movieStatistic;
        }
    }
}
