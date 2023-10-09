﻿using APIS.DTOs.AuthenticationDTOs.ResponseDto;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieServices.DTOs.MovieDTOs.RequestDto;
using MovieServices.DTOs.MovieDTOs.ResponseDTO;
using MovieServices.Models;
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

        [HttpGet("{startDate}/{endDate}")]
        public ActionResult<ServiceResponse<List<StatisticMovieResponse>>> GetStatisticByDate(DateTime startDate, DateTime endDate)
        {
            ServiceResponse<List<StatisticMovieResponse>> response = new ServiceResponse<List<StatisticMovieResponse>>();
            List<StatisticMovieResponse> statisticMovieResponses = new List<StatisticMovieResponse>();
            List<StatisticMovieResponse> statisticMovieResponsesFinal = new List<StatisticMovieResponse>();
            List<Models.Movie> movieList = movieService.GetMovieList();
            var statisticResponses = statisticService.GetStatisticByDateToDate(startDate, endDate);
            if (statisticResponses == null)
            {
                response.Data = null;
                response.Message = "Id not found";
                response.Status = 400;
            }
            else
            {
                foreach (var item in statisticResponses)
                {
                    StatisticMovieResponse statisticMovieResponse = new StatisticMovieResponse();
                    var movie = movieService.GetMovieById(item.MovieId);
                    if (movie == null)
                    {
                        continue;
                    }
                    statisticMovieResponse.View += item.View;
                    statisticMovieResponse.MovieName = movie.MovieName;
                    statisticMovieResponse.MovieThumnailImage = movie.MovieThumnailImage;
                    statisticMovieResponse.ReleasedYear = movie.ReleasedYear;
                    statisticMovieResponses.Add(statisticMovieResponse);
                }
                for (var i = 0; i <= movieList.Count; i++)
                {
                    while (i < movieList.Count)
                    {
                        int viewCount = 0;
                        StatisticMovieResponse statisticMovieResponse = new StatisticMovieResponse();
                        int j = 0;
                        for (j = 0; j < statisticResponses.Count; j++)
                        {
                            if (movieList[i].MovieId == statisticResponses[j].MovieId)
                            {
                                viewCount += statisticResponses[j].View;
                            }
                        }
                        if (viewCount > 0)
                        {
                            statisticMovieResponse.MovieName = movieList[i].MovieName;
                            statisticMovieResponse.MovieThumnailImage = movieList[i].MovieThumnailImage;
                            statisticMovieResponse.ReleasedYear = movieList[i].ReleasedYear;
                            statisticMovieResponse.View = viewCount;
                            statisticMovieResponsesFinal.Add(statisticMovieResponse);
                            i++;
                        }
                        else
                        {
                            i++;
                        }
                    }
                }
                response.Data = statisticMovieResponsesFinal;
                response.Message = "Statistic success";
                response.TotalDataList = statisticMovieResponsesFinal.Count;
                response.Status = 200;
            }
            return response;
        }
    }
}
