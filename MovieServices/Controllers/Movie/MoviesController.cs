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
            var movieResponseList = new List<MovieResponse>();
            var movieList = service.GetMovieList();
            foreach (var movie in movieList)
            {
                movieResponseList.Add(_mapper.Map<MovieResponse>(movie));
            }
            response.Data = movieResponseList;
            response.Message = "Get Movie List";
            response.Status = 200;
            response.TotalDataList = movieResponseList.Count;
            return response;
        }

        [HttpGet("id")]
        public ActionResult<MovieResponse> GetMovieById(int id)
        {
            var movie = service.GetMovieById(id);
            var movieResponse = _mapper.Map<MovieResponse>(movie);
            return movieResponse;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public ActionResult<Models.Movie> CreateMovie(AddMovieDto addMovieDto)
        {
            Models.Movie movie = _mapper.Map<Models.Movie>(addMovieDto);

            return service.CreateMovie(movie, addMovieDto.Categories);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Update")]
        public ActionResult<Models.Movie> UpdateMovie(UpdateMovieDto updateMovieDto)
        {
            Models.Movie movie = _mapper.Map<Models.Movie>(updateMovieDto);

            return service.UpdateMovie(movie, updateMovieDto.Categories);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Delete")]
        public ActionResult<Models.Movie> DeleteMovie(int id)
        {
            return service.DeleteMovie(id);
        }
        [HttpGet("Search")]
        public ActionResult<ServiceResponse<List<MovieResponse>>> SearchMovies([FromQuery] string searchMovieName)
        {
            var movies = service.SearchMovies(searchMovieName);
            return Ok(movies);
        }
    }
}

