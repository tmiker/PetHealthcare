using PetHealthcare.MVC.DTOs;

namespace PetHealthcare.MVC.Abstractions
{
    public interface ICarouselImagesHttpProvider
    {
        Task<(bool IsSuccess, IEnumerable<CarouselImageDTO>? Images, string? ErrorMessage)> GetAllCarouselImagesAsync(string token = "");
        Task<(bool IsSuccess, CarouselImageDTO? Image, string? ErrorMessage)> GetCarouselImageAsync(int id, string token = "");
        Task<(bool IsSuccess, string? ErrorMessage)> AddCarouselImageAsync(CarouselImageDTO imageDTO, string token = "");
        Task<(bool IsSuccess, string? ErrorMessage)> EditCarouselImageAsync(int id, CarouselImageDTO imageDTO, string token = "");
        Task<(bool IsSuccess, string? ErrorMessage)> DeleteCarouselImageAsync(int id, string token = "");
    }
}
