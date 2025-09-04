using PetHealthcare.API.DTOs;

namespace PetHealthcare.API.Abstractions
{
    public interface ICarouselImageService
    {
        Task<(bool IsSuccess, IEnumerable<CarouselImageDTO>? Images, string? ErrorMessage)> GetAllCarouselImagesAsync();
        Task<(bool IsSuccess, CarouselImageDTO? Image, string? ErrorMessage)> GetCarouselImageAsync(int id);
        Task<(bool IsSuccess, string? ErrorMessage)> AddCarouselImageAsync(CarouselImageDTO imageDTO);
        Task<(bool IsSuccess, string? ErrorMessage)> EditCarouselImageAsync(int id, CarouselImageDTO imageDTO);
        Task<(bool IsSuccess, string? ErrorMessage)> DeleteCarouselImageAsync(int id);
    }
}
