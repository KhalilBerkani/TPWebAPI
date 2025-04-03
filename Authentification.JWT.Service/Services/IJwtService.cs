namespace Authentification.JWT.Service.Services
{
    /// <summary>
    /// Interface du service de génération de tokens JWT.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Génère un token JWT à partir de l'identifiant utilisateur.
        /// </summary>
        /// <param name="userId">Identifiant unique de l'utilisateur.</param>
        /// <returns>Token JWT sous forme de chaîne de caractères.</returns>
        string GenerateToken(int userId);
    }
}
