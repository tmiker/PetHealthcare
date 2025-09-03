using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.API.DTOs
{
    public class VetDTO
    {
        public string? OwnedBy { get; set; }
        public int? Id { get; set; }

        [Required]
        [StringLength(250)]
        public string? Hospital { get; set; }

        [StringLength(250)]
        public string? Doctor { get; set; }

        [StringLength(50)]
        public string? Phone { get; set; }

        [StringLength(150)]
        [Display(Name = "Street 1")]
        public string? Street1 { get; set; }

        [StringLength(150)]
        [Display(Name = "Street 2")]
        public string? Street2 { get; set; }

        [StringLength(150)]
        public string? City { get; set; }

        [StringLength(50)]
        public string? State { get; set; }

        [StringLength(50)]
        [Display(Name = "Zip Code")]
        public string? ZipCode { get; set; }

        public byte[]? ImageBytes { get; set; }

    }
}
