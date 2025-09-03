using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PetHealthcare.Domain.Models
{
    public class Pet : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        public string? Nickname { get; set; }

        [Display(Name = "Gender")]
        public string? Gender { get; set; }

        [StringLength(50)]
        public string? Breed { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Date of Adoption")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfAdoption { get; set; }

        [StringLength(50)]
        [Display(Name = "Chip Number")]
        public string? ChipNumber { get; set; }

        [StringLength(250)]
        public string? Allergies { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }

        [Display(Name ="Image URL")]
        public string? ImageURL { get; set; }

        [Display(Name = "Image File Name")]
        public string? ImageFileName { get; set; }

        [InverseProperty(nameof(Visit.PetNavigation))]
        public IEnumerable<Visit> Visits { get; set; } = new List<Visit>();
    }
}
