using PetHealthcare.MVC.DTOs;
using PetHealthcare.MVC.Paging;

namespace PetHealthcare.MVC.ViewModels.CarouselImages
{
    public class CarouselImageIndexViewModel
    {
        public List<CarouselImageDTO> CarouselImages { get; set; } = new List<CarouselImageDTO>();
        public PagingInfo? PagingInfo { get; set; }
        public string? Message { get; set; }
    }
}
