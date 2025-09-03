using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.DataAccess.Configurations
{
    internal class VetConfiguration : IEntityTypeConfiguration<Vet>
    {
        public void Configure(EntityTypeBuilder<Vet> builder)
        {
            builder.HasData(
                new Vet { Id = 1, Hospital = "Unspecified", Doctor = "Unspecified", Phone = "phone", Street1 = "Street 1", Street2 = "Street 2", City = "City", State = "State", ZipCode = "Zip Code", ImageBytes = null, OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
                new Vet { Id = 2, Hospital = "Austin Pets Alive, Inc.", Doctor = "Shelby Asquith", Phone = "phone", Street1 = "Street 1", Street2 = "Street 2", City = "City", State = "State", ZipCode = "Zip Code", ImageBytes = null, OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
                new Vet { Id = 3, Hospital = "Austin Pets Alive, Inc.", Doctor = "Kristina Bevers", Phone = "phone", Street1 = "Street 1", Street2 = "Street 2", City = "City", State = "State", ZipCode = "Zip Code", ImageBytes = null, OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
                new Vet { Id = 4, Hospital = "Austin Pets Alive, Inc.", Doctor = "Annie Hoelle", Phone = "phone", Street1 = "Street 1", Street2 = "Street 2", City = "City", State = "State", ZipCode = "Zip Code", ImageBytes = null, OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
                new Vet { Id = 5, Hospital = "VCA Arbor Animal Hospital", Doctor = "Jenette Lucia", Phone = "(512) 782-0374", Street1 = "5114 Balcones Woods Dr", Street2 = "Suite 312", City = "Austin", State = "TX", ZipCode = "78759", ImageBytes = null, OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
                new Vet { Id = 6, Hospital = "VCA Arbor Animal Hospital", Doctor = "Jennifer Renner", Phone = "(512) 782-0374", Street1 = "5114 Balcones Woods Dr", Street2 = "Suite 312", City = "Austin", State = "TX", ZipCode = "78759", ImageBytes = null, OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" }
                );
        }
    }
}
