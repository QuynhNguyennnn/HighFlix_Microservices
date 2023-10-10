
using AutoMapper;
using MovieServices.DTOs.MovieDTOs.ResponseDTO;
using MovieServices.Models;

namespace MovieServices.Services.MovieServices
{
    public interface IMovieService
    {
        List<MovieResponse> GetMovieList(IMapper _mapper);
        List<MovieResponse> GetMovieListNew(IMapper _mapper);
        //MovieResponse GetMovieById(int id, IMapper _mapper);
        List<Movie> SearchMovies(string searchMovieName);
        Movie CreateMovie(Movie movie , List<int> cates);
        Movie UpdateMovie(Movie movie, List<int> cates);
        Movie DeleteMovie(int id);
    }
}
