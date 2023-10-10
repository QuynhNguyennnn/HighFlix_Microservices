using APIS.DTOs.AuthenticationDTOs.ResponseDto;
using APIS.DTOs.MovieDTOs.RequestDto;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieServices.DTOs.MovieDTOs.ResponseDTO;
using MovieServices.Services.MovieServices;
using MovieServices.Models;

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
            var movieList = service.GetMovieList(_mapper);
            response.Data = movieList;
            response.Message = "Get Movie List";
            response.Status = 200;
            response.TotalDataList = movieList.Count;
            return response;
        }

        [HttpGet("new")]
        public ActionResult<ServiceResponse<List<MovieResponse>>> GetMovieListNew()
        {
            var response = new ServiceResponse<List<MovieResponse>>();
            var movieList = service.GetMovieListNew(_mapper);
            response.Data = movieList;
            response.Message = "Get Movie List New";
            response.Status = 200;
            response.TotalDataList = movieList.Count;
            return response;
        }

        [HttpGet("id")]
        public ActionResult<ServiceResponse<MovieResponse>> GetMovieById(int id)
        {
            //var movie = service.GetMovieById(id, _mapper);
            var response = new ServiceResponse<MovieResponse>();
            //response.Data = movie;
            response.Message = "Get Movie";
            response.Status = 200;
            return response;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public ActionResult<ServiceResponse<Models.Movie>> CreateMovie(AddMovieDto addMovieDto)
        {
            Models.Movie movie = _mapper.Map<Models.Movie>(addMovieDto);
            movie = service.CreateMovie(movie, addMovieDto.Categories);
            var response = new ServiceResponse<Models.Movie>();
            response.Data = movie;
            response.Message = "Create Movie";
            response.Status = 200;
            return response;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Update")]
        public ActionResult<ServiceResponse<Models.Movie>> UpdateMovie(UpdateMovieDto updateMovieDto)
        {
            Models.Movie movie = _mapper.Map<Models.Movie>(updateMovieDto);

            movie = service.UpdateMovie(movie, updateMovieDto.Categories);
            var response = new ServiceResponse<Models.Movie>();
            response.Data = movie;
            response.Message = "Update Movie";
            response.Status = 200;
            return response;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Delete")]
        public ActionResult<ServiceResponse<Models.Movie>> DeleteMovie(int id)
        {
            Models.Movie movie = service.DeleteMovie(id);
            var response = new ServiceResponse<Models.Movie>();
            response.Data = movie;
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

