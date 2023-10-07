using APIS.DTOs.AuthenticationDTOs.ResponseDto;
using AutoMapper;
using CategoryServices.DTOs.RequestDTO;
using CategoryServices.DTOs.ResponseDTO;
using CategoryServices.Models;
using CategoryServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CategoryServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService service = new CategoryService();
        private readonly IMapper _mapper;

        public CategoriesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ServiceResponse<List<CategoryResponse>>> GetCategoryList()
        {
            var response = new ServiceResponse<List<CategoryResponse>>();
            var categoryResponseList = new List<CategoryResponse>();
            var categoryList = service.GetCategoryList();
            foreach (var category in categoryList)
            {
                categoryResponseList.Add(_mapper.Map<CategoryResponse>(category));
            }
            response.Data = categoryResponseList;
            response.Message = "Get Category List";
            response.Status = 200;
            response.TotalDataList = categoryResponseList.Count;
            return response;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public ActionResult<Category> CreateCategory(AddCategoryDTO addCategoryDTO)
        {
            Category category = _mapper.Map<Category>(addCategoryDTO);

            return service.CreateCategory(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Update")]
        public ActionResult<Category> UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
        {
            Category category = _mapper.Map<Category>(updateCategoryDTO);

            return service.UpdateCategory(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Delete")]
        public ActionResult<Category> DeleteCategory(int id)
        {
            return service.DeleteCategory(id);
        }
    }
}
