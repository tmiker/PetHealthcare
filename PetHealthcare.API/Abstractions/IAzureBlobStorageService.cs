using PetHealthcare.API.Enums;

namespace PetHealthcare.API.Abstractions
{
    public interface IAzureBlobStorageService
    {
        Task<string> UploadImageToAzure(IFormFile file, ContainerTypeEnum containerType, string? userId = null);

        Task DeleteImagesFromAzure(ContainerTypeEnum containerType, string fileName, string? userId = null);
    }
}
