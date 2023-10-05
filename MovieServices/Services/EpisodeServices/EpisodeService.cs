﻿using MovieServices.DAOs;
using MovieServices.Models;

namespace MovieServices.Services.EpisodeServices
{
    public class EpisodeService : IEpisodeService
    {
        public List<Episode> GetEpisodes() => EpisodeDAO.GetEpisodes();
        public Episode GetEpisodeById(int episodeId) => EpisodeDAO.GetEpisodeById(episodeId);

        public Episode CreateEpisode(Episode episode) => EpisodeDAO.CreateEpisode(episode);

        public Episode UpdateEpisode(Episode episode) => EpisodeDAO.UpdateEpisode(episode);

        public Episode DeleteEpisode(int episodeId) => EpisodeDAO.DeleteEpisode(episodeId);
    }
}
