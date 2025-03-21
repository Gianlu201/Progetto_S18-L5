using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Progetto_S18_L5.Models;

namespace Progetto_S18_L5.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<
            ApplicationUser,
            ApplicationRole,
            string,
            IdentityUserClaim<string>,
            ApplicationUserRole,
            IdentityUserLogin<string>,
            IdentityRoleClaim<string>,
            IdentityUserToken<string>
        >
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura la relazione tra ApplicationUserRole e ApplicationUser
            modelBuilder
                .Entity<ApplicationUserRole>()
                .HasOne(ur => ur.User) // Un ApplicationUserRole ha un utente associato
                .WithMany(u => u.ApplicationUserRole) // Un utente può avere più ruoli
                .HasForeignKey(ur => ur.UserId); // Definizione della chiave esterna che collega l'utente al ruolo

            // Configura la relazione tra ApplicationUserRole e ApplicationRole
            modelBuilder
                .Entity<ApplicationUserRole>()
                .HasOne(ur => ur.Role) // Un ApplicationUserRole ha un ruolo associato
                .WithMany(u => u.ApplicationUserRole) // Un ruolo può essere assegnato a più utenti
                .HasForeignKey(ur => ur.RoleId); // Definizione della chiave esterna che collega il ruolo all'utente

            // Configura la relazione tra ApplicationUser e Class
        }
    }
}
