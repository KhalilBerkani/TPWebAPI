using Authentification.JWT.DAL.Models;
using Authentification.JWT.Service.DTOs;
using Authentification.JWT.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Authentification.JWT.Controllers
{
    /// <summary>
    /// Contrôleur responsable de l'authentification des utilisateurs (inscription et connexion).
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        /// <summary>
        /// Constructeur de AuthController avec injection des services utilisateur et JWT.
        /// </summary>
        /// <param name="userService">Service de gestion des utilisateurs.</param>
        /// <param name="jwtService">Service de génération de tokens JWT.</param>
        public AuthController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Enregistre un nouvel utilisateur.
        /// </summary>
        /// <param name="model">Données d'inscription (nom d'utilisateur, email, mot de passe).</param>
        /// <returns>Un utilisateur enregistré ou une erreur de validation.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                var user = await _userService.RegisterUserAsync(model.Username, model.Email, model.Password);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Authentifie un utilisateur et retourne un token JWT si les identifiants sont valides.
        /// </summary>
        /// <param name="model">Données de connexion (nom d'utilisateur, mot de passe).</param>
        /// <returns>Un token JWT ou un message d'erreur si la connexion échoue.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var userEntity = await _userService.GetFullUserEntityAsync(model.Username);
            if (userEntity == null)
                return Unauthorized("Utilisateur non trouvé.");

            if (!_userService.VerifyPassword(userEntity.PasswordHash, userEntity.PasswordSalt, model.Password))
                return Unauthorized("Mot de passe incorrect.");

            var token = _jwtService.GenerateToken(userEntity.Id);
            return Ok(new { token });
        }
    }

    /// <summary>
    /// Modèle utilisé pour enregistrer un nouvel utilisateur.
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Nom d'utilisateur choisi.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Adresse email de l'utilisateur.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Mot de passe à définir pour l'utilisateur.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }

    /// <summary>
    /// Modèle utilisé pour l'authentification d'un utilisateur existant.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Nom d'utilisateur saisi lors de la connexion.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Mot de passe saisi lors de la connexion.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
