using APIS.DTOs.RequestDto;
using AuthenticationServices.Models;
using AutoMapper;

namespace ProjectAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<RegisterDto, User>();
        }
    }
}
