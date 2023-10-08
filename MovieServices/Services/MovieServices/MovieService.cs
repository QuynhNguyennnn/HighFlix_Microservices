
using AutoMapper;
using MovieServices.DAOs;
using MovieServices.DTOs.MovieDTOs.ResponseDTO;
using MovieServices.Models;

namespace MovieServices.Services.MovieServices
{
    public class MovieService : IMovieService
    {
        public MovieResponse GetMovieById(int id, IMapper _mapper) => MovieDAO.GetMovieById(id, _mapper);

        public List<MovieResponse> GetMovieList(IMapper _mapper) => MovieDAO.GetMovieList(_mapper);

        public List<MovieResponse> GetMovieListNew(IMapper _mapper) => MovieDAO.GetMovieListNew(_mapper);

        public Movie CreateMovie(Movie movie, List<int> cates) => MovieDAO.CreateMovie(movie, cates);

        public Movie UpdateMovie(Movie movie, List<int> cates) => MovieDAO.UpdateMovie(movie, cates);

        public Movie DeleteMovie(int id) => MovieDAO.DeleteMovie(id);
        public List<Movie> SearchMovies(string searchMovieName)
        {
            using (var context = new HighFlixV2Context())
            {
                return context.Movies
                    .Where(movie =>
                        movie.IsActive &&
                        movie.MovieName.Contains(searchMovieName))
                    .ToList();
            }
        }

    }
}
