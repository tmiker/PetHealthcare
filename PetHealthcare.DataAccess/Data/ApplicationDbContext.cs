using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetHealthcare.DataAccess.Configurations;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Vet> Vets { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<CarouselImage> CarouselImages { get; set; }
        public DbSet<DeleteImage> DeleteImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.ApplyConfiguration(new RoleConfiguration());
            // modelBuilder.ApplyConfiguration(new UserConfiguration());
            // modelBuilder.ApplyConfiguration(new IdentityUserRoleConfiguration());
            // modelBuilder.ApplyConfiguration(new PetConfiguration());
            // modelBuilder.ApplyConfiguration(new VetConfiguration());
            // modelBuilder.ApplyConfiguration(new VisitConfiguration());
        }
    }
}
