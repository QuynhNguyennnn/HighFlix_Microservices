using APIS.DTOs.AuthenticationDTOs.ResponseDto;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieServices.DTOs.MovieDTOs.ResponseDTO;
using MovieServices.Services.MovieServices;
using MovieServices.Services.StatisticServices;

namespace MovieServices.Controllers.Statistic
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private IStatisticService statisticService = new StatisticService();
        private IMovieService movieService = new MovieService();
        private readonly IMapper _mapper;

        public StatisticController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("movieId, dateStatistic")]
        public ActionResult<ServiceResponse<StatisticMovieResponse>> GetStatisticmovie(int movieId, DateTime statisticDate)
        {
            var response = new ServiceResponse<StatisticMovieResponse>();
            var movieStatistic = statisticService.StatisticMovieByDate(movieId, statisticDate);
            var movie = movieService.GetMovieById(movieId);

            //foreach (var movie in movieList)
            //{
            //    movieResponseList.Add(_mapper.Map<MovieResponse>(movie));
            //}
            //response.Data = movieResponseList;
            //response.Message = "Get Movie List";
            //response.Status = 200;
            //response.TotalDataList = movieResponseList.Count;
            return response;
        }

    }
}
