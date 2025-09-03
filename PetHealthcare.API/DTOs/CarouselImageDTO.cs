using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.API.DTOs
{
    public class CarouselImageDTO
    {
        public string? OwnedBy { get; set; }
        public int? Id { get; set; }
        public string? Subject { get; set; }
        public string? Caption { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageURL { get; set; }

        [Display(Name = "Image File Name")]
        public string? ImageFileName { get; set; }

        public IFormFile? Image { get; set; }

    }
}
