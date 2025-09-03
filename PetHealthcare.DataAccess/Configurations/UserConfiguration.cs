using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.DataAccess.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            ApplicationUser user1 = new ApplicationUser()
            {
                Id = "6de17564-15cd-412f-9804-537b7cbeb4c8",
                CustomerNumber = 277620619,
                UserName = "mike",
                NormalizedUserName = "MIKE",
                Email = "mike@somemail.com",
                NormalizedEmail = "MIKE@SOMEMAIL.COM",
                LockoutEnabled = true
            };

            ApplicationUser user2 = new ApplicationUser()
            {
                Id = "d2bd63f7-abfb-4873-8e01-b4bac6ba2ba7",
                CustomerNumber = 486180454,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@somemail.com",
                NormalizedEmail = "ADMIN@SOMEMAIL.COM",
                LockoutEnabled = true
            };
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            user1.PasswordHash = passwordHasher.HashPassword(user1, "mswAmA4*dogs");
            user2.PasswordHash = passwordHasher.HashPassword(user2, "aswAmA4*dogs");
            builder.HasData(
                user1, user2
                );
        }
    }
}
