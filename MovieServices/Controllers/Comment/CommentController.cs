
using MovieServices.Models;
using MovieServices.Services.CommentServices;
using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using APIS.DTOs.AuthenticationDTOs.ResponseDto;
using APIS.DTOs.CommentDTOs.ResponseDto;
using MovieServices.DTOs.CommentDTOs.RequestDto;

namespace MovieServices.Controllers.Comment
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private ICommentService service = new CommentService();
        private readonly IMapper _mapper;

        public CommentController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<ServiceResponse<List<CommentReponse>>> GetComments()
        {
            var response = new ServiceResponse<List<CommentReponse>>();
            var commentResponseList = new List<CommentReponse>();
            var commentList = service.GetComments();
            foreach (var comment in commentList)
            {
                commentResponseList.Add(_mapper.Map<CommentReponse>(comment));
            }
            response.Data = commentResponseList;
            response.Message = "Get Comment List";
            response.Status = 200;
            response.TotalDataList = commentResponseList.Count;
            return response;
        }
        [HttpGet("movieId")]
        public ActionResult<ServiceResponse<List<CommentReponse>>> GetCommentById(int movieId)
        {
            var response = new ServiceResponse<List<CommentReponse>>();
            var commentResponseList = new List<CommentReponse>();
            var commentList = service.GetCommentById(movieId);
            foreach (var comment in commentList)
            {
                commentResponseList.Add(_mapper.Map<CommentReponse>(comment));
            }
            response.Data = commentResponseList;
            response.Message = "Get Comment List";
            response.Status = 200;
            response.TotalDataList = commentResponseList.Count;
            return response;
        }


        [HttpPost("Create")]
        [Authorize(Roles = "User")]
        public ActionResult<ServiceResponse<CommentReponse>> CreateComment(CreateCommentDto createCommentDto)
        {
            Models.Comment comment = _mapper.Map<Models.Comment>(createCommentDto);
            var commentResponse = _mapper.Map<CommentReponse>(service.CreateComment(comment));
            var response = new ServiceResponse<CommentReponse>();
            response.Data = commentResponse;
            response.Status = 200;
            response.Message = "Create Comment";
            return response;
        }
        [HttpPut("Update")]
        [Authorize(Roles = "User")]
        public ActionResult<ServiceResponse<CommentReponse>> UpdateComment(UpdateCommentDto updateCommentDto)
        {
            var commentResponse = _mapper.Map<CommentReponse>(service.UpdateComment(_mapper.Map<Models.Comment>(updateCommentDto)));
            var response = new ServiceResponse<CommentReponse>();
            response.Data = commentResponse;
            response.Status = 200;
            response.Message = "Update Comment";
            return response;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Delete")]
        public ActionResult<ServiceResponse<CommentReponse>> DeleteComment(int id)
        {
            var commentResponse = _mapper.Map<CommentReponse>(service.DeleteComment(id));
            var response = new ServiceResponse<CommentReponse>();
            response.Data = commentResponse;
            response.Status = 200;
            response.Message = "Delete Comment";
            return response;
        }
    }
}
