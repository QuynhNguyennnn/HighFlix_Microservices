using APIS.DTOs.AuthenticationDTOs.ResponseDto;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieServices.DTOs.EpisodeDTOs.RequestDTO;
using MovieServices.DTOs.EpisodeDTOs.ResponseDTO;
using MovieServices.Services.EpisodeServices;

namespace MovieServices.Controllers.Episode
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesController : ControllerBase
    {
        private IEpisodeService service = new EpisodeService();
        private readonly IMapper _mapper;

        public EpisodesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ServiceResponse<List<EpisodeResponse>>> GetEpisodes()
        {
            var response = new ServiceResponse<List<EpisodeResponse>>();
            var episodeResponseList = new List<EpisodeResponse>();
            var episodeList = service.GetEpisodes();
            foreach (var episode in episodeList)
            {
                episodeResponseList.Add(_mapper.Map<EpisodeResponse>(episode));
            }
            response.Data = episodeResponseList;
            response.Message = "Get Episode List";
            response.Status = 200;
            response.TotalDataList = episodeResponseList.Count;
            return response;
        }

        [HttpGet("id")]
        public ActionResult<ServiceResponse<EpisodeResponse>> GetEpisodeById(int id)
        {
            var response = new ServiceResponse<EpisodeResponse>();
            var episode = service.GetEpisodeById(id);
            var episodeResponse = _mapper.Map<EpisodeResponse>(episode);
            response.Data = episodeResponse;
            response.Message = $"Get Episode by {id}";
            response.Status = 200;
            response.TotalDataList = 1;
            return response;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public ActionResult<Models.Episode> CreateMovie(AddEpisodeDto addEpisodeDto)
        {
            Models.Episode episode = _mapper.Map<Models.Episode>(addEpisodeDto);
            return service.CreateEpisode(episode);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Update")]
        public ActionResult<Models.Episode> UpdateEpisode(UpdateEpisodeDto updateEpisodeDto)
        {
            Models.Episode episode = _mapper.Map<Models.Episode>(updateEpisodeDto);
            return service.UpdateEpisode(episode);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Delete")]
        public ActionResult<Models.Episode> DeleteEpisode(int id)
        {
            return service.DeleteEpisode(id);
        }
    }
}
