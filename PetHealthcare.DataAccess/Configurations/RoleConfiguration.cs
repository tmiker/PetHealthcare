using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetHealthcare.DataAccess.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole() { Id = "aa66eef8-f787-4050-8bfb-8aaa1936b4c3", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole() { Id = "9496b506-150a-43e3-a69c-9655e624aa38", Name = "Manager", NormalizedName = "MANAGER" },
                new IdentityRole() { Id = "93c319b5-9985-4108-9ce5-0fd689233e70", Name = "Employee", NormalizedName = "EMPLOYEE" },
                new IdentityRole() { Id = "707d9453-e031-4682-b69b-5b45ce57e4fd", Name = "Customer", NormalizedName = "CUSTOMER" }
                );
        }
    }
}
