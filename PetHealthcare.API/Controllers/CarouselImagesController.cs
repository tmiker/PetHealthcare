using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.CustomExceptions;
using PetHealthcare.API.DTOs;
using PetHealthcare.API.Enums;
using System.Diagnostics;

namespace PetHealthcare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarouselImagesController : ControllerBase
    {
        private readonly ICarouselImageService _carouselImageService;
        private readonly IAzureBlobStorageService _azureBlobStorageService;

        public CarouselImagesController(ICarouselImageService carouselImageService, IAzureBlobStorageService azureBlobStorageService)
        {
            _carouselImageService = carouselImageService;
            _azureBlobStorageService = azureBlobStorageService;
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllCarouselImages()
        {
            var result = await _carouselImageService.GetAllCarouselImagesAsync();
            if (result.IsSuccess) return Ok(result.Images);
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                else return BadRequest("Unknown Error getting images.");
            }
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCarouselImage(int id)
        {
            var result = await _carouselImageService.GetCarouselImageAsync(id);
            if (result.IsSuccess) return Ok(result.Image);
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                else return BadRequest("Unknown Error getting the image.");
            }
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddCarouselImage([FromForm] CarouselImageDTO imageDTO)
        {
            // for image upload to azure blob storage
            if (imageDTO.Image != null)
            {
                string[] permittedExtensions = { ".png", ".jpg", ".jpeg", ".bmp" };
                var extension = Path.GetExtension(imageDTO.Image.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                {
                    return BadRequest("Images must be in png, jpg, jpeg, or bmp format.");
                }

                try
                {
                    imageDTO.ImageURL = await _azureBlobStorageService.UploadImageToAzure(imageDTO.Image, ContainerTypeEnum.CarouselImage);
                    imageDTO.ImageFileName = imageDTO.Image.FileName;
                }
                catch (AzureBlobStorageException ex)    // Azure.RequestFailedException
                {
                    Debug.WriteLine($"AZURE BLOB UPLOAD EXCEPTION: {ex.Message}");
                    return BadRequest("The image could not be uploaded. It is probably a duplicate name. \nPlease change the image name or select another image and try again");
                }
            }

            var result = await _carouselImageService.AddCarouselImageAsync(imageDTO);
            if (result.IsSuccess) return StatusCode(StatusCodes.Status201Created);
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                else return BadRequest("Unknown Error adding the image.");
            }
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> EditCarouselImage(int id, [FromForm] CarouselImageDTO imageDTO)
        {
            // for image upload to azure blob storage
            if (imageDTO.Image != null)
            {
                // delete existing image if exists
                if (imageDTO.ImageFileName != null)
                {
                    try
                    {
                        await _azureBlobStorageService.DeleteImagesFromAzure(ContainerTypeEnum.CarouselImage, imageDTO.ImageFileName);
                    }
                    catch (AzureBlobStorageException ex)
                    {
                        Debug.WriteLine($"AZURE BLOB DELETE EXCEPTION: {ex.Message}");
                        return BadRequest("Azure Blob Storage exception on delete. The image could not be deleted.");
                    }
                }

                string[] permittedExtensions = { ".png", ".jpg", ".jpeg", ".bmp" };
                var extension = Path.GetExtension(imageDTO.Image.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                {
                    return BadRequest("Images must be in png, jpg, jpeg, or bmp format.");
                }

                try
                {
                    imageDTO.ImageURL = await _azureBlobStorageService.UploadImageToAzure(imageDTO.Image, ContainerTypeEnum.CarouselImage);
                    imageDTO.ImageFileName = imageDTO.Image.FileName;
                }
                catch (AzureBlobStorageException ex)    // Azure.RequestFailedException
                {
                    Debug.WriteLine($"AZURE BLOB UPLOAD EXCEPTION: {ex.Message}");
                    return BadRequest("The image could not be uploaded. It is probably a duplicate name. \nPlease change the image name or select another image and try again");
                }
            }

            var result = await _carouselImageService.EditCarouselImageAsync(id, imageDTO);
            if (result.IsSuccess) return StatusCode(StatusCodes.Status204NoContent);
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                else return BadRequest("Unknown Error editing the image.");
            }
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteCarouselImage(int id)
        {
            var imageResult = await _carouselImageService.GetCarouselImageAsync(id);

            // delete existing image in Azure if exists
            if (imageResult.Image != null)
            {
                if (imageResult.Image.ImageFileName != null)
                {
                    try
                    {
                        await _azureBlobStorageService.DeleteImagesFromAzure(ContainerTypeEnum.PetImage, imageResult.Image.ImageFileName);
                    }
                    catch (AzureBlobStorageException ex)
                    {
                        Debug.WriteLine($"AZURE BLOB DELETE EXCEPTION: {ex.Message}");
                        return BadRequest("Azure Blob Storage exception on delete. The pet image could not be deleted.");
                    }
                }
            }

            // delete existing image based on imageDTO.ImageURL ???
            var result = await _carouselImageService.DeleteCarouselImageAsync(id);
            if (result.IsSuccess) return StatusCode(StatusCodes.Status204NoContent);
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                else return BadRequest("Unknown Error deleting the image.");
            }
        }
    }
}
