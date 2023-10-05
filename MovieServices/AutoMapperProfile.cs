using APIS.DTOs.MovieDTOs.RequestDto;
using AutoMapper;
using MovieServices.DTOs.EpisodeDTOs.RequestDTO;
using MovieServices.DTOs.EpisodeDTOs.ResponseDTO;
using MovieServices.DTOs.MovieDTOs.ResponseDTO;
using MovieServices.Models;

namespace ProjectAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            //for movie
            CreateMap<MovieResponse, Movie>();
            CreateMap<Movie, MovieResponse>();
            CreateMap<AddMovieDto, Movie>();
            CreateMap<UpdateMovieDto, Movie>();

            //for episode
            CreateMap<EpisodeResponse, Episode>();
            CreateMap<Episode, EpisodeResponse>();
            CreateMap<AddEpisodeDto, Episode>();
            CreateMap<UpdateEpisodeDto, Episode>();
            CreateMap<DeleteEpisodeDto, Episode>();
        }
    }
}
