using Authentification.JWT.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentification.JWT.DAL.Context
{
    /// <summary>
    /// Représente le contexte de base de données principal utilisé par Entity Framework Core.
    /// Permet d'accéder et de manipuler les entités de la base de données.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Constructeur du contexte prenant en paramètre les options de configuration.
        /// </summary>
        /// <param name="options">Options de configuration du DbContext (fournies via l'injection de dépendances).</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        /// <summary>
        /// Représente la table des utilisateurs dans la base de données.
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
}
