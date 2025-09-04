using AutoMapper;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.DTOs;
using PetHealthcare.Domain.Abstractions.ISqlServerRepositories;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.API.Services
{
    public class CarouselImageService : ICarouselImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CarouselImageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<(bool IsSuccess, IEnumerable<CarouselImageDTO>? Images, string? ErrorMessage)> GetAllCarouselImagesAsync()
        {
            IEnumerable<CarouselImage> images = await _unitOfWork.CarouselImages.GetAllAsync();
            IEnumerable<CarouselImageDTO> imageDTOs = _mapper.Map<IEnumerable<CarouselImageDTO>>(images);
            return (true, imageDTOs, null);
        }

        public async Task<(bool IsSuccess, CarouselImageDTO? Image, string? ErrorMessage)> GetCarouselImageAsync(int id)
        {
            CarouselImage? image = await _unitOfWork.CarouselImages.GetFirstOrDefaultAsync(filter: c => c.Id == id);
            if (image == null) return (false, null, $"No image was found with Id of {id}");
            CarouselImageDTO imageDTO = _mapper.Map<CarouselImageDTO>(image);
            return (true, imageDTO, null);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> AddCarouselImageAsync(CarouselImageDTO imageDTO)
        {
            CarouselImage image = _mapper.Map<CarouselImage>(imageDTO);
            await _unitOfWork.CarouselImages.AddAsync(image);
            await _unitOfWork.SaveAsync();
            return (true, null);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> EditCarouselImageAsync(int id, CarouselImageDTO imageDTO)
        {
            if (imageDTO.Id != id) return (false, "Id does not match the object Id");
            CarouselImage? image = await _unitOfWork.CarouselImages.GetFirstOrDefaultAsync(filter: c => c.Id == id);
            if (image == null) return (false, $"No image was found with Id of {id}");

            image.Subject = imageDTO.Subject;
            image.Caption = imageDTO.Caption;
            image.ImageURL = imageDTO.ImageURL;
            image.ImageFileName = imageDTO.ImageFileName;

            await _unitOfWork.SaveAsync();
            return (true, null);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> DeleteCarouselImageAsync(int id)
        {
            CarouselImage? image = await _unitOfWork.CarouselImages.GetFirstOrDefaultAsync(filter: c => c.Id == id);
            if (image == null) return (false, $"No image was found with an Id of {id}.");
            await _unitOfWork.CarouselImages!.RemoveAsync(image);
            await _unitOfWork.SaveAsync();
            return (true, null);
        }
    }
}
