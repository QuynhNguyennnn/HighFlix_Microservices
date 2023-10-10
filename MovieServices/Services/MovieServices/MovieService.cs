
using MovieServices.DAOs;
using MovieServices.Models;

namespace MovieServices.Services.MovieServices
{
    public class MovieService : IMovieService
    {
        public Movie GetMovieById(int id) => MovieDAO.GetMovieById(id);

        public List<Movie> GetMovieList() => MovieDAO.GetMovieList();

        public Movie CreateMovie(Movie movie, List<int> cates) => MovieDAO.CreateMovie(movie, cates);

        public Movie UpdateMovie(Movie movie, List<int> cates) => MovieDAO.UpdateMovie(movie, cates);

        public Movie DeleteMovie(int id) => MovieDAO.DeleteMovie(id);
        public List<Movie> SearchMovies(string searchMovieName) => MovieDAO.SearchMovies(searchMovieName);

    }
}
