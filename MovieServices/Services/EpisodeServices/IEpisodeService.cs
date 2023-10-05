using MovieServices.Models;

namespace MovieServices.Services.EpisodeServices
{
    public interface IEpisodeService
    {
        List<Episode> GetEpisodes();
        Episode GetEpisodeById(int episodeId);
        Episode CreateEpisode(Episode episode);
        Episode UpdateEpisode(Episode episode);
        Episode DeleteEpisode(int episodeId);
    }
}
