using Authentification.JWT.DAL.Models;
using Microsoft.EntityFrameworkCore; // ✅ À ajouter ici
using System;

namespace Authentification.JWT.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
