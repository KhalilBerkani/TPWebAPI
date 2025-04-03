using AutoMapper;
using Authentification.JWT.DAL.Models;
using Authentification.JWT.Service.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Authentification.JWT.Service.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
