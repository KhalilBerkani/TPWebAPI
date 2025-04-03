using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentification.JWT.Service;

namespace Authentification.JWT.Service.Services
{
    /// <summary>
    /// Service responsable de la génération de tokens JWT.
    /// </summary>
    public class JwtService : IJwtService
    {
        private readonly string _key;

        /// <summary>
        /// Constructeur du JwtService. Récupère la clé secrète depuis la configuration.
        /// </summary>
        /// <param name="configuration">Configuration contenant la clé JWT.</param>
        public JwtService(IConfiguration configuration)
        {
            _key = configuration["Jwt:Key"]!;
        }

        /// <summary>
        /// Génère un token JWT signé contenant l'identifiant de l'utilisateur.
        /// </summary>
        /// <param name="userId">Identifiant unique de l'utilisateur.</param>
        /// <returns>Un token JWT sous forme de chaîne de caractères.</returns>
        public string GenerateToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyBytes = Encoding.UTF8.GetBytes(_key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
