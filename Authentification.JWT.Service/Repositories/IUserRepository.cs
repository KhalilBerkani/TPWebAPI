using Authentification.JWT.DAL.Models;

namespace Authentification.JWT.DAL.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User> RegisterUserAsync(User user);
    }
}
