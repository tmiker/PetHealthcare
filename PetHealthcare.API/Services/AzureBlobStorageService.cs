using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.CustomExceptions;
using PetHealthcare.API.Enums;
using System.Diagnostics;

namespace PetHealthcare.API.Services
{
    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        private readonly IConfiguration _config;

        public AzureBlobStorageService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> UploadImageToAzure(IFormFile file, ContainerTypeEnum containerType, string? userId = null)
        {
            // uploads images to Azure blob storage container in a folder specific to the user (folder is userId)
            try
            {
                string? azureConnectionString = _config["AzureSettings:AzureBlobStorageConnection"];
                if (string.IsNullOrWhiteSpace(azureConnectionString)) throw new ArgumentNullException(nameof(azureConnectionString));

                string containerName = string.Empty;
                // add security checks - size, extension, etc.

                if (containerType == ContainerTypeEnum.PetImage) containerName = "pet-image-container";
                if (containerType == ContainerTypeEnum.CarouselImage) containerName = "pet-carousel-image-container";

                BlobContainerClient blobContainerClient = new BlobContainerClient(azureConnectionString, containerName);
                BlobClient blobClient = blobContainerClient.GetBlobClient($"{userId}/{file.FileName}");

                var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                await blobClient.UploadAsync(memoryStream);

                string imageUrl = blobClient.Uri.AbsoluteUri;                       // pass to controller in return for uploading to database

                return imageUrl;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"AZURE BLOB UPLOAD EXCEPTION: will rethrow as AzureBlobUploadException");
                throw new AzureBlobStorageException($"Exception uploading image to Azure Blob Storage.", ex);
            }
        }

        public async Task DeleteImagesFromAzure(ContainerTypeEnum containerType, string fileName, string? userId = null)
        {
            // deletes images in users folder from Azure blob storage - container specific to the user (folder is userId)
            try
            {
                string? azureConnectionString = _config["AzureSettings:AzureBlobStorageConnection"];
                if (string.IsNullOrWhiteSpace(azureConnectionString)) throw new ArgumentNullException(nameof(azureConnectionString));

                string containerName = string.Empty;
                // add security checks - size, extension, etc.

                if (containerType == ContainerTypeEnum.PetImage) containerName = "pet-image-container";
                if (containerType == ContainerTypeEnum.CarouselImage) containerName = "pet-carousel-image-container";

                BlobContainerClient blobContainerClient = new BlobContainerClient(azureConnectionString, containerName);
                BlobClient blobClient = blobContainerClient.GetBlobClient($"{userId}/{fileName}");
                await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots, default);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"AZURE BLOB DELETE EXCEPTION: will rethrow as AzureBlobUploadException");
                throw new AzureBlobStorageException($"Exception deleting image from Azure Blob Storage.", ex);
            }
        }
    }
}
