using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Authentification.JWT.DAL.Context
{
    /// <summary>
    /// Usine de création du DbContext utilisée uniquement à des fins de conception (ex : migrations).
    /// Permet à Entity Framework Core CLI de créer une instance d'ApplicationDbContext sans démarrer l'application.
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        /// <summary>
        /// Crée une instance d'ApplicationDbContext avec les options nécessaires à la connexion SQL Server.
        /// Utilisé principalement par les outils de ligne de commande EF Core (dotnet ef).
        /// </summary>
        /// <param name="args">Arguments passés par l'outil CLI.</param>
        /// <returns>Instance configurée d'ApplicationDbContext.</returns>
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Data Source=RABDY1JN53\\SQLEXPRESS;Initial Catalog=JWTTP;Integrated Security=True;TrustServerCertificate=True");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
