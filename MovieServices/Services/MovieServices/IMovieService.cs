
using MovieServices.Models;

namespace MovieServices.Services.MovieServices
{
    public interface IMovieService
    {
        List<Movie> GetMovieList();
        Movie GetMovieById(int id);
        List<Movie> SearchMovies(string searchMovieName);
        Movie CreateMovie(Movie movie , List<int> cates);
        Movie UpdateMovie(Movie movie, List<int> cates);
        Movie DeleteMovie(int id);
    }
}
