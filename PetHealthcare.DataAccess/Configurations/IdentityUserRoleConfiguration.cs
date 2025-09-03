using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetHealthcare.DataAccess.Configurations
{
    internal class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>() { RoleId = "aa66eef8-f787-4050-8bfb-8aaa1936b4c3", UserId = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
                new IdentityUserRole<string>() { RoleId = "9496b506-150a-43e3-a69c-9655e624aa38", UserId = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
                new IdentityUserRole<string>() { RoleId = "93c319b5-9985-4108-9ce5-0fd689233e70", UserId = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
                new IdentityUserRole<string>() { RoleId = "707d9453-e031-4682-b69b-5b45ce57e4fd", UserId = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
                new IdentityUserRole<string>() { RoleId = "aa66eef8-f787-4050-8bfb-8aaa1936b4c3", UserId = "d2bd63f7-abfb-4873-8e01-b4bac6ba2ba7" },
                new IdentityUserRole<string>() { RoleId = "9496b506-150a-43e3-a69c-9655e624aa38", UserId = "d2bd63f7-abfb-4873-8e01-b4bac6ba2ba7" },
                new IdentityUserRole<string>() { RoleId = "93c319b5-9985-4108-9ce5-0fd689233e70", UserId = "d2bd63f7-abfb-4873-8e01-b4bac6ba2ba7" },
                new IdentityUserRole<string>() { RoleId = "707d9453-e031-4682-b69b-5b45ce57e4fd", UserId = "d2bd63f7-abfb-4873-8e01-b4bac6ba2ba7" }
                );

        }
    }
}
