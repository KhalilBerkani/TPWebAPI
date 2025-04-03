using Authentification.JWT.DAL.Models;
using Authentification.JWT.Service;
using Authentification.JWT.Service.DTOs;
using AutoMapper;
using System.Security.Cryptography;
using System.Text;
using Authentification.JWT.Service.Repositories;  

namespace Authentification.JWT.Service.Services
{
    /// <summary>
    /// Service métier responsable de la gestion des utilisateurs (enregistrement, authentification, validation...).
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructeur du service utilisateur.
        /// </summary>
        /// <param name="userRepository">Dépendance vers le repository utilisateur.</param>
        /// <param name="mapper">Dépendance AutoMapper pour la conversion entité/DTO.</param>
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Récupère l'entité complète d'un utilisateur à partir de son nom d'utilisateur.
        /// </summary>
        /// <param name="username">Nom d'utilisateur.</param>
        /// <returns>Entité utilisateur ou null.</returns>
        public async Task<User?> GetFullUserEntityAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        /// <summary>
        /// Récupère un utilisateur sous forme de DTO.
        /// </summary>
        /// <param name="username">Nom d'utilisateur.</param>
        /// <returns>UserDto ou null si l'utilisateur n'existe pas.</returns>
        public async Task<UserDto?> GetUserByUsernameAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        /// <summary>
        /// Enregistre un nouvel utilisateur avec mot de passe hashé et salt, puis retourne son DTO.
        /// </summary>
        /// <param name="username">Nom d'utilisateur.</param>
        /// <param name="email">Email de l'utilisateur.</param>
        /// <param name="password">Mot de passe brut.</param>
        /// <returns>UserDto représentant l'utilisateur enregistré.</returns>
        /// <exception cref="ArgumentException">Lancé si le mot de passe est invalide.</exception>
        public async Task<UserDto> RegisterUserAsync(string username, string email, string password)
        {
            var passwordErrors = ValidatePassword(password);
            if (passwordErrors.Any())
                throw new ArgumentException(string.Join(" ", passwordErrors));

            string salt = GenerateSalt();
            string hashedPassword = HashPassword(password, salt);

            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = hashedPassword,
                PasswordSalt = salt
            };

            var createdUser = await _userRepository.RegisterUserAsync(user);
            return _mapper.Map<UserDto>(createdUser);
        }

        /// <summary>
        /// Hash un mot de passe avec un salt donné en utilisant SHA256.
        /// </summary>
        /// <param name="password">Mot de passe brut.</param>
        /// <param name="salt">Valeur de salt à concaténer.</param>
        /// <returns>Mot de passe hashé en base64.</returns>
        private string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            var combinedBytes = Encoding.UTF8.GetBytes(password + salt);
            var hashBytes = sha256.ComputeHash(combinedBytes);
            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Vérifie si un mot de passe donné correspond au hash et salt stockés.
        /// </summary>
        /// <param name="storedHash">Mot de passe hashé stocké.</param>
        /// <param name="storedSalt">Salt associé au mot de passe.</param>
        /// <param name="passwordToCheck">Mot de passe à vérifier.</param>
        /// <returns>True si le mot de passe est valide, sinon false.</returns>
        public bool VerifyPassword(string storedHash, string storedSalt, string passwordToCheck)
        {
            string hashedInput = HashPassword(passwordToCheck, storedSalt);
            return storedHash == hashedInput;
        }

        /// <summary>
        /// Valide la complexité d’un mot de passe selon plusieurs critères (longueur, majuscules, chiffres, etc.).
        /// </summary>
        /// <param name="password">Mot de passe à valider.</param>
        /// <returns>Liste des erreurs de validation. Vide si le mot de passe est valide.</returns>
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

        /// <summary>
        /// Génère un salt aléatoire sécurisé utilisé pour le hashage des mots de passe.
        /// </summary>
        /// <param name="size">Taille du salt en octets (par défaut : 32).</param>
        /// <returns>Salt encodé en base64.</returns>
        private string GenerateSalt(int size = 32)
        {
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[size];
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }
    }
}
