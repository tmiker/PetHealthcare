using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.DataAccess.Configurations
{
    internal class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.HasData(
                new Pet { Id = 1, Name = "Wendy", Gender = "Female", Breed = "Chi-Princess", DateOfBirth = DateTime.Parse("2016-12-02"), DateOfAdoption = DateTime.Parse("2018-12-02"), ChipNumber = "981020025911645", Allergies = null, ImageURL = null, OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" },
                new Pet { Id = 2, Name = "Marlow", Gender = "Male", Breed = "Chi-Raptor", DateOfBirth = DateTime.Parse("2016-06-04"), DateOfAdoption = DateTime.Parse("2018-07-22"), ChipNumber = "985112004775656", Allergies = null, ImageURL = null, OwnedBy = "6de17564-15cd-412f-9804-537b7cbeb4c8" }
                );
        }
    }
}
