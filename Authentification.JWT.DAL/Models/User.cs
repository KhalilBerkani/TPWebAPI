using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentification.JWT.DAL.Models

{
    /// <summary>
    /// Entité représentant un utilisateur stocké en base de données.
    /// Contient les informations d'identification nécessaires à l'authentification.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identifiant unique de l'utilisateur (clé primaire).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nom d'utilisateur (doit être unique).
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Adresse email de l'utilisateur.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Mot de passe de l'utilisateur, stocké sous forme hachée (SHA256).
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Salt utilisé pour le hashage du mot de passe (sécurité renforcée).
        /// </summary>
        public string PasswordSalt { get; set; } = string.Empty;
    }
}
