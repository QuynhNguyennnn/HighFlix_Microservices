using APIS.DTOs.AuthenticationDTOs.ResponseDto;
using APIS.DTOs.MovieDTOs.RequestDto;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieServices.DTOs.MovieDTOs.ResponseDTO;
using MovieServices.Services.MovieServices;
using MovieServices.Models;
using MovieServices.DAOs;

namespace MovieServices.Controllers.Movie
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IMovieService service = new MovieService();
        private readonly IMapper _mapper;

        public MoviesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ServiceResponse<List<MovieResponse>>> GetMovieList()
        {
            var response = new ServiceResponse<List<MovieResponse>>();
            var movieResponseList = new List<MovieResponse>();
            var movieList = service.GetMovieList();
            foreach (var movie in movieList)
            {
                List<MovieCategory> movieCategories = MovieCategoryDAO.GetCategoryByMovieId(movie.MovieId);
                MovieResponse movieResponse = _mapper.Map<MovieResponse>(movie);
                movieCategories.ForEach(movieCategory =>
                {
                    movieResponse.Categories += movieCategory.CategoryId.ToString();
                });
                movieResponseList.Add(movieResponse);
            }

            response.Data = movieResponseList;
            response.Message = "Get Movie List";
            response.Status = 200;
            response.TotalDataList = movieResponseList.Count;
            return response;
        }

        [HttpGet("id")]
        public ActionResult<ServiceResponse<MovieResponse>> GetMovieById(int id)
        {
            var movie = service.GetMovieById(id);
            var movieResponse = _mapper.Map<MovieResponse>(movie);
            List<MovieCategory> movieCategories = MovieCategoryDAO.GetCategoryByMovieId(movie.MovieId);
            movieCategories.ForEach(movieCategory =>
            {
                movieResponse.Categories += movieCategory.CategoryId.ToString();
            });
            var response = new ServiceResponse<MovieResponse>();
            response.Data = movieResponse;
            response.Message = "Get Movie Detail";
            response.Status = 200;
            response.TotalDataList = 1;
            return response;
        }

        [HttpGet("new")]
        public ActionResult<ServiceResponse<List<MovieResponse>>> GetMovieListNew()
        {
            var response = new ServiceResponse<List<MovieResponse>>();
            var movieResponseList = new List<MovieResponse>();
            var movieList = service.GetMovieListNew();
            foreach (var movie in movieList)
            {
                List<MovieCategory> movieCategories = MovieCategoryDAO.GetCategoryByMovieId(movie.MovieId);
                MovieResponse movieResponse = _mapper.Map<MovieResponse>(movie);
                movieCategories.ForEach(movieCategory =>
                {
                    movieResponse.Categories += movieCategory.CategoryId.ToString();
                });
                movieResponseList.Add(movieResponse);
            }
            response.Data = movieResponseList;
            response.Message = "Get Movie List New";
            response.Status = 200;
            response.TotalDataList = movieList.Count;
            return response;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public ActionResult<ServiceResponse<MovieResponse>> CreateMovie(AddMovieDto addMovieDto)
        {
            Models.Movie movie = _mapper.Map<Models.Movie>(addMovieDto);
            movie = service.CreateMovie(movie, addMovieDto.Categories);
            var movieResponse = _mapper.Map<MovieResponse>(movie);
            var response = new ServiceResponse<MovieResponse>();
            response.Data = movieResponse;
            response.Message = "Create Movie";
            response.Status = 200;
            return response;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Update")]
        public ActionResult<ServiceResponse<MovieResponse>> UpdateMovie(UpdateMovieDto updateMovieDto)
        {
            Models.Movie movie = _mapper.Map<Models.Movie>(updateMovieDto);

            movie = service.UpdateMovie(movie, updateMovieDto.Categories);
            var movieResponse = _mapper.Map<MovieResponse>(movie);
            var response = new ServiceResponse<MovieResponse>();
            response.Data = movieResponse;
            response.Message = "Update Movie";
            response.Status = 200;
            return response;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Delete")]
        public ActionResult<ServiceResponse<MovieResponse>> DeleteMovie(int id)
        {
            Models.Movie movie = service.DeleteMovie(id);
            var movieResponse = _mapper.Map<MovieResponse>(movie);
            var response = new ServiceResponse<MovieResponse>();
            response.Data = movieResponse;
            response.Message = "Delete Movie";
            response.Status = 200;
            return response;
        }

        [HttpGet("Search")]
        public ActionResult<ServiceResponse<List<MovieResponse>>> SearchMovies([FromQuery] string searchMovieName)
        {
            var movies = service.SearchMovies(searchMovieName);
            return Ok(movies);
        }
    }
}

