using PetHealthcare.MVC.DTOs;

namespace PetHealth.MVC.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public List<CarouselImageDTO>? CarouselImages { get; set; } = new List<CarouselImageDTO>();
    }
}
