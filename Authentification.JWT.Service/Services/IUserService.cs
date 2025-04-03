using Authentification.JWT.DAL.Models;
using Authentification.JWT.Service.DTOs;

namespace Authentification.JWT.Service.Services
{
    /// <summary>
    /// Interface représentant les opérations métier liées aux utilisateurs.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Récupère un utilisateur sous forme de DTO à partir de son nom d'utilisateur.
        /// </summary>
        /// <param name="username">Nom d'utilisateur à rechercher.</param>
        /// <returns>Un UserDto ou null si l'utilisateur n'existe pas.</returns>
        Task<UserDto?> GetUserByUsernameAsync(string username);

        /// <summary>
        /// Enregistre un nouvel utilisateur avec son mot de passe haché et retourne un DTO.
        /// </summary>
        /// <param name="username">Nom d'utilisateur.</param>
        /// <param name="email">Adresse email.</param>
        /// <param name="password">Mot de passe en clair.</param>
        /// <returns>Le UserDto correspondant à l'utilisateur enregistré.</returns>
        Task<UserDto> RegisterUserAsync(string username, string email, string password);

        /// <summary>
        /// Vérifie si un mot de passe correspond au hachage et au salt stockés.
        /// </summary>
        /// <param name="storedHash">Mot de passe haché stocké.</param>
        /// <param name="storedSalt">Salt utilisé lors du hachage.</param>
        /// <param name="passwordToCheck">Mot de passe en clair à vérifier.</param>
        /// <returns>True si le mot de passe est correct, sinon false.</returns>
        bool VerifyPassword(string storedHash, string storedSalt, string passwordToCheck);

        /// <summary>
        /// Récupère l'utilisateur complet (entité) à partir du nom d'utilisateur.
        /// </summary>
        /// <param name="username">Nom d'utilisateur à rechercher.</param>
        /// <returns>L'objet User ou null s'il n'existe pas.</returns>
        Task<User?> GetFullUserEntityAsync(string username);
    }
}
