namespace Authentification.JWT.Service.DTOs
{
    /// <summary>
    /// Data Transfer Object (DTO) représentant un utilisateur.
    /// Utilisé pour exposer des données utilisateur sans révéler les informations sensibles.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Identifiant unique de l'utilisateur.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nom d'utilisateur.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Adresse email de l'utilisateur.
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}
