using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.CustomExceptions;
using PetHealthcare.API.DTOs;
using PetHealthcare.API.Enums;
using System.Diagnostics;
using System.Security.Claims;

namespace PetHealthcare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly IAzureBlobStorageService _azureBlobStorageService;

        public PetsController(IPetService petService, IAzureBlobStorageService azureBlobStorageService)
        {
            _petService = petService;
            _azureBlobStorageService = azureBlobStorageService;
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllPets()
        {
            // get current user id
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
            {
                (bool IsSuccess, IEnumerable<PetDTO>? Pets, string? ErrorMessage) result = new();
                // if admin return all pets without filtering by user id

                if (User.IsInRole("Admin"))     // ... get all no filter
                {
                    result = await _petService.GetAllPetsAsync();
                }
                else
                {
                    string userId = userIdClaim!.Value;
                    result = await _petService.GetAllPetsAsync(userId);
                }

                if (result.IsSuccess) return Ok(result.Pets);
                else
                {
                    if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                    else return BadRequest("Unknown Error getting pets.");
                }
            }
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPet(int id)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
            {
                (bool IsSuccess, PetDTO? Pet, string? ErrorMessage) result = new();

                if (User.IsInRole("Admin"))     // ... get all no filter
                {
                    result = await _petService.GetPetAsync(id);
                }
                else
                {
                    string userId = userIdClaim!.Value;
                    result = await _petService.GetPetAsync(id, userId);
                }

                if (result.IsSuccess) return Ok(result.Pet);
                else
                {
                    if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                    else return BadRequest("Unknown Error getting the pet.");
                }
            }
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddPet([FromForm] PetDTO petDTO)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
            {
                petDTO.OwnedBy = userIdClaim.Value;
            }

            // for image upload to azure blob storage
            if (petDTO.Image != null)
            {
                string[] permittedExtensions = { ".png", ".jpg", ".jpeg", ".bmp" };
                var extension = Path.GetExtension(petDTO.Image.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                {
                    return BadRequest("Images must be in png, jpg, jpeg, or bmp format.");
                }

                try
                {
                    petDTO.ImageURL = await _azureBlobStorageService.UploadImageToAzure(petDTO.Image, ContainerTypeEnum.PetImage, userIdClaim.Value);
                    petDTO.ImageFileName = petDTO.Image.FileName;
                }
                catch (AzureBlobStorageException ex)    // Azure.RequestFailedException
                {
                    Debug.WriteLine($"AZURE BLOB UPLOAD EXCEPTION: {ex.Message}");
                    return BadRequest("The image could not be uploaded. It is probably a duplicate name. \nPlease change the image name or select another image and try again");
                }
            }

            var result = await _petService.AddPetAsync(petDTO);
            if (result.IsSuccess) return StatusCode(StatusCodes.Status201Created);
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                else return BadRequest("Unknown Error adding the pet.");
            }
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> EditPet(int id, [FromForm] PetDTO petDTO)  // imagefilename is null here when should not be
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            // verify pet is owned by current user (petDTO.OwnedBy will be null at this point as comes from MVC and want to be client agnostic)
            if (!User.IsInRole("Admin"))
            {
                var validationResult = await _petService.GetPetAsync(id);
                if (validationResult.IsSuccess && validationResult.Pet != null)
                {
                    if (validationResult.Pet.OwnedBy != userIdClaim!.Value) return StatusCode(StatusCodes.Status401Unauthorized);
                    else petDTO.OwnedBy = userIdClaim.Value;
                }
            }
            // for image upload to azure blob storage
            if (petDTO.Image != null)
            {
                // delete existing image if exists
                if (petDTO.ImageFileName != null)
                {
                    try
                    {
                        await _azureBlobStorageService.DeleteImagesFromAzure(ContainerTypeEnum.PetImage, petDTO.ImageFileName, userIdClaim!.Value);  // delete the image in the current petDTO.ImageFileName property
                        // petDTO.ImageFileName value will be overridden when upload new file below so don't need set to nulll
                    }
                    catch (AzureBlobStorageException ex)
                    {
                        Debug.WriteLine($"AZURE BLOB DELETE EXCEPTION: {ex.Message}");
                        return BadRequest("Azure Blob Storage exception on delete. The previous image could not be deleted.");
                    }
                }

                // upload new image

                string[] permittedExtensions = { ".png", ".jpg", ".jpeg", ".bmp" };
                var extension = Path.GetExtension(petDTO.Image.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                {
                    return BadRequest("Images must be in png, jpg, jpeg, or bmp format.");
                }

                try
                {
                    petDTO.ImageURL = await _azureBlobStorageService.UploadImageToAzure(petDTO.Image, ContainerTypeEnum.PetImage, userIdClaim!.Value);
                    petDTO.ImageFileName = petDTO.Image.FileName;
                }
                catch (AzureBlobStorageException ex)
                {
                    Debug.WriteLine($"AZURE BLOB UPLOAD EXCEPTION: {ex.Message}");
                    return BadRequest("The image could not be uploaded. It is probably a duplicate name. \nPlease change the image name or select another image and try again");
                }

            }

            var result = await _petService.EditPetAsync(id, petDTO);
            if (result.IsSuccess) return StatusCode(StatusCodes.Status204NoContent);
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                else return BadRequest("Unknown Error editing the pet.");
            }
        }

        [Authorize]
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeletePet(int id)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var petResult = await _petService.GetPetAsync(id);
            // verify pet is owned by current user
            if (!User.IsInRole("Admin"))
            {

                if (petResult.IsSuccess && petResult.Pet != null)
                {
                    if (petResult.Pet.OwnedBy != userIdClaim!.Value) return StatusCode(StatusCodes.Status401Unauthorized);
                }
            }

            // delete existing image in Azure if exists
            if (petResult.Pet != null)
            {
                if (petResult.Pet.ImageFileName != null)
                {
                    try
                    {
                        await _azureBlobStorageService.DeleteImagesFromAzure(ContainerTypeEnum.PetImage, petResult.Pet.ImageFileName, userIdClaim!.Value);  // delete the pet image, should delete folder if no images in it
                    }
                    catch (AzureBlobStorageException ex)
                    {
                        Debug.WriteLine($"AZURE BLOB DELETE EXCEPTION: {ex.Message}");
                        return BadRequest("Azure Blob Storage exception on delete. The pet image could not be deleted.");
                    }
                }
            }

            var result = await _petService.DeletePetAsync(id);
            if (result.IsSuccess)
            {
                //// delete existing image folder based on user id
                //try
                //{
                //    await _azureBlobStorageService.DeleteImagesFromAzure(ContainerTypeEnum.PetImage, userIdClaim!.Value);    // should delete the users whole folder
                //}
                //catch (AzureBlobStorageException ex)
                //{
                //    Debug.WriteLine($"AZURE BLOB DELETE EXCEPTION: {ex.Message}");
                //    return BadRequest("Azure Blob Storage exception on delete. The previous image could not be deleted.");
                //}

                return StatusCode(StatusCodes.Status204NoContent);
            }
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                else return BadRequest("Unknown Error deleting the pet.");
            }
        }
    }
}
