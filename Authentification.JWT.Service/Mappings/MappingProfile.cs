using AutoMapper;
using Authentification.JWT.DAL.Models;
using Authentification.JWT.Service.DTOs;

namespace Authentification.JWT.Service.Mappings
{
    /// <summary>
    /// Profil de mappage AutoMapper pour convertir entre les entités et les DTOs.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Constructeur définissant les règles de conversion entre les objets User et UserDto.
        /// </summary>
        public MappingProfile()
        {
            // Permet le mappage bidirectionnel entre User (Entity) et UserDto
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
