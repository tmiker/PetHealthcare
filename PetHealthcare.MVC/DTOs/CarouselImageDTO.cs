using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.MVC.DTOs
{
    public class CarouselImageDTO
    {
        public int Id { get; set; }
        public string? Subject { get; set; }
        public string? Caption { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageURL { get; set; }

        [Display(Name = "Image File Name")]
        public string? ImageFileName { get; set; }

        public IFormFile? Image { get; set; }

    }
}
