using Authentification.JWT.DAL.Models;
using Authentification.JWT.Service.DTOs;
using Authentification.JWT.Service.Services;
using Authentification.JWT.Services;
using Microsoft.AspNetCore.Mvc;

namespace Authentification.JWT.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }
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

    public class RegisterModel
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginModel
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
