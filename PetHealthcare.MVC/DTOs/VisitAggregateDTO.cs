using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PetHealthcare.MVC.DTOs
{
    public class VisitAggregateDTO
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Please select a pet.")]
        [Display(Name = "Pet")]
        public int? PetId { get; set; }

        [Display(Name = "Pet")]
        public string? PetName { get; set; }

        [Display(Name = "Pet Image URL")]
        public string? PetImageURL { get; set; }

        [Required(ErrorMessage = "Please select a vet.")]
        [Display(Name = "Vet")]
        public int? VetId { get; set; }

        [Display(Name = "Doctor")]
        public string? Doctor { get; set; }

        [Display(Name = "Hospital")]
        public string? Hospital { get; set; }

        [Required]
        [Display(Name = "Date of Visit")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfVisit { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Please select a visit type.")]
        [StringLength(50)]
        [Display(Name = "Visit Type")]
        public string? VisitType { get; set; }

        [StringLength(100)]
        public string? Reason { get; set; }

        public double? Weight { get; set; }

        [StringLength(500)]
        public string? Diagnosis { get; set; }

        [StringLength(500)]
        public string? Prescriptions { get; set; }

        [StringLength(500)]
        public string? Instructions { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        public bool Heartworm { get; set; }

        public bool Bordatella { get; set; }

        public bool Rabies { get; set; }

        [Display(Name = "DA2PPV")]
        public bool Da2ppv { get; set; }

        public bool Leptospirosis { get; set; }

        [Display(Name = "Influenza H3N2")]

        public bool InfluenzaH3N2 { get; set; }

        [Display(Name = "Influenza H3N8")]
        public bool InfluenzaH3N8 { get; set; }

        [Display(Name = "Fecal Test")]
        public bool FecalTest { get; set; }

        [Display(Name = "Other Tests")]
        public string? OtherTests { get; set; }
    }
}
