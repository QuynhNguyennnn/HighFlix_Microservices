
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
        //private IUserService userService = new UserService();

        public CommentController(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet]
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


        [HttpPost("Create")]
       // [Authorize(Roles = "Admin")]
        public ActionResult<Models.Comment> CreateComment(CreateCommentDto createCommentDto)
        {
            /*User member = new User();
            var username = User?.Identity?.Name;
            var user = userService.GetUserByUsername(username);
      //      var userId = user.UserId;
            var userInfor = new User();
            if (user != null)
            {
                userInfor.UserId = user.UserId;
            }
           
   
                createCommentDto.UserId = userInfor.UserId;
            */
           
            
            Models.Comment comment = _mapper.Map<Models.Comment>(createCommentDto);
            return service.CreateComment(comment);
        }
        [HttpPut("Update")]
        public ActionResult<Models.Comment> UpdateComment(UpdateCommentDto updateCommentDto)
        {
            Models.Comment comment = _mapper.Map<Models.Comment>(updateCommentDto);
            return service.UpdateComment(comment);
        }

        // [Authorize(Roles = "Admin")]
        [HttpPut("Delete")]
        public ActionResult<Models.Comment> DeleteComment(int id)
        {
            return service.DeleteComment(id);
        }
    }
}
