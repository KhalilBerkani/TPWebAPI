using Authentification.JWT.DAL.Models;
using Authentification.JWT.DAL.Repositories;
using Authentification.JWT.Service.DTOs;
using AutoMapper;
using System.Security.Cryptography;
using System.Text;

namespace Authentification.JWT.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<User?> GetFullUserEntityAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }


        public async Task<UserDto?> GetUserByUsernameAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        public async Task<UserDto> RegisterUserAsync(string username, string email, string password)
        {
            var passwordErrors = ValidatePassword(password);
            if (passwordErrors.Any())
                throw new ArgumentException(string.Join(" ", passwordErrors));

            string hashedPassword = HashPassword(password);

            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = hashedPassword
            };

            var createdUser = await _userRepository.RegisterUserAsync(user);
            return _mapper.Map<UserDto>(createdUser);
        }



        public bool VerifyPassword(string hashedPassword, string passwordToCheck)
        {
            return hashedPassword == HashPassword(passwordToCheck);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        public List<string> ValidatePassword(string password)
        {
            var errors = new List<string>();

            if (password.Length < 8)
                errors.Add("Le mot de passe doit contenir au moins 8 caractères.");

            if (!password.Any(char.IsUpper))
                errors.Add("Le mot de passe doit contenir au moins une lettre majuscule.");

            if (!password.Any(char.IsLower))
                errors.Add("Le mot de passe doit contenir au moins une lettre minuscule.");

            if (!password.Any(char.IsDigit))
                errors.Add("Le mot de passe doit contenir au moins un chiffre.");

            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                errors.Add("Le mot de passe doit contenir au moins un caractère spécial (ex: @, #, !).");

            return errors;
        }

    }
}
