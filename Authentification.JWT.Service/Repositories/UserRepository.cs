using Authentification.JWT.DAL.Context;
using Authentification.JWT.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentification.JWT.Service.Repositories
{
    /// <summary>
    /// Implémentation concrète de l'interface IUserRepository.
    /// Fournit les opérations d'accès aux données liées à l'entité User.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructeur du UserRepository injectant le contexte de base de données.
        /// </summary>
        /// <param name="context">Contexte de base de données Entity Framework.</param>
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Recherche un utilisateur en base de données à partir de son nom d'utilisateur.
        /// </summary>
        /// <param name="username">Nom d'utilisateur à rechercher.</param>
        /// <returns>L'utilisateur trouvé ou null s'il n'existe pas.</returns>
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        /// <summary>
        /// Enregistre un nouvel utilisateur dans la base de données.
        /// </summary>
        /// <param name="user">Objet utilisateur à enregistrer.</param>
        /// <returns>L'utilisateur enregistré avec son identifiant généré.</returns>
        public async Task<User> RegisterUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
