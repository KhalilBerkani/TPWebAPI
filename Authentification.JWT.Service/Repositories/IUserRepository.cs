using Authentification.JWT.DAL.Models;

namespace Authentification.JWT.Service.Repositories;

/// <summary>
/// Interface du repository utilisateur définissant les opérations d'accès aux données.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Recherche un utilisateur en base de données à partir de son nom d'utilisateur.
    /// </summary>
    /// <param name="username">Nom d'utilisateur à rechercher.</param>
    /// <returns>L'utilisateur correspondant ou null s'il n'existe pas.</returns>
    Task<User?> GetUserByUsernameAsync(string username);

    /// <summary>
    /// Enregistre un nouvel utilisateur en base de données.
    /// </summary>
    /// <param name="user">L'utilisateur à enregistrer.</param>
    /// <returns>L'utilisateur créé avec son identifiant généré.</returns>
    Task<User> RegisterUserAsync(User user);
}
