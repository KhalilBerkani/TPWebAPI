// Service/Services/IUserService.cs

using Authentification.JWT.DAL.Models;
using Authentification.JWT.Service.DTOs;

namespace Authentification.JWT.Service.Services
{
    public interface IUserService
    {
        Task<UserDto?> GetUserByUsernameAsync(string username);
        Task<UserDto> RegisterUserAsync(string username, string email, string password);
        bool VerifyPassword(string storedHash, string storedSalt, string passwordToCheck);

        Task<User?> GetFullUserEntityAsync(string username);
    }
}
