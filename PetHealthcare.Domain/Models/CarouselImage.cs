using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PetHealthcare.Domain.Models
{
    public class CarouselImage : BaseEntity
    {
        [StringLength(50)]
        public string? Subject { get; set; }
        [StringLength(50)]
        public string? Caption { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageURL { get; set; }

        [Display(Name = "Image File Name")]
        public string? ImageFileName { get; set; }
    }
}
