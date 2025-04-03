using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Authentification.JWT.DAL.Context;
using Authentification.JWT.Service.Repositories;

namespace Authentification.JWT.Service.Extensions
{
    /// <summary>
    /// Classe d'extension pour ajouter les services de la couche DAL (Data Access Layer) au conteneur d'injection de dépendances.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Ajoute les services nécessaires pour accéder à la base de données et les repositories dans le conteneur de services.
        /// Configure le DbContext pour utiliser SQL Server et enregistre les repositories.
        /// </summary>
        /// <param name="services">Le conteneur d'injection de dépendances.</param>
        /// <param name="connectionString">La chaîne de connexion pour la base de données SQL Server.</param>
        /// <returns>Retourne le conteneur de services mis à jour.</returns>
        public static IServiceCollection AddDalServices(this IServiceCollection services, string connectionString)
        {
            // Enregistre le DbContext avec la chaîne de connexion pour SQL Server
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Enregistre IUserRepository avec sa mise en œuvre concrète UserRepository dans le conteneur de services
            services.AddScoped<IUserRepository, UserRepository>(); // Important : permet l'injection de dépendance du repository

            return services; // Retourne le conteneur des services mis à jour pour chaîner d'autres appels si nécessaire
        }
    }
}
