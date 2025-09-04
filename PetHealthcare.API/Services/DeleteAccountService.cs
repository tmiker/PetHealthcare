using Microsoft.AspNetCore.Identity;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.CustomExceptions;
using PetHealthcare.API.DTOs;
using PetHealthcare.API.Enums;
using PetHealthcare.Domain.Abstractions.ISqlServerRepositories;
using PetHealthcare.Domain.Models;
using System.Diagnostics;

namespace PetHealthcare.API.Services
{
    public class DeleteAccountService : IDeleteAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAzureBlobStorageService _azureBlobStorageService;

        public DeleteAccountService(UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> passwordHasher, 
            IUnitOfWork unitOfWork, IAzureBlobStorageService azureBlobStorageService)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
            _azureBlobStorageService = azureBlobStorageService;
        }

        public async Task<(bool IsSuccess, string? SuccessMessage, List<string>? ErrorMessages)> DeleteAccountAsync(DeleteAccountDTO deleteAccountDTO)
        {
            List<string> errorMessages = new List<string>();
            ApplicationUser? user = await _userManager.FindByEmailAsync(deleteAccountDTO.Email);
            if (user == null) errorMessages.Add("Invalid email or password.");
            else
            {
                var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash!, deleteAccountDTO.Password);
                if (passwordVerificationResult == PasswordVerificationResult.Failed) errorMessages.Add("Invalid email or password.");
            }

            if (errorMessages.Count > 0) return (false, null, errorMessages);
            else
            {

                int petCount = 0;
                int vetCount = 0;
                int visitCount = 0;

                // delete all user visits, persist so no issues deleting pets and vets
                IEnumerable<Visit> userVisits = await _unitOfWork.Visits.GetAllAsync(filter: v => v.OwnedBy == user!.Id);
                visitCount = userVisits.Count();
                foreach (var visit in userVisits) await _unitOfWork.Visits.RemoveAsync(visit);
                await _unitOfWork.SaveAsync();

                // add pet image url to deleteimages as queue for blob storage deletion, delete all user pets
                IEnumerable<Pet> userPets = await _unitOfWork.Pets.GetAllAsync(filter: v => v.OwnedBy == user!.Id);
                petCount = userPets.Count();
                Debug.WriteLine($"\n*** USER ACCOUNT DELETION *** Pet images to remove: ");
                foreach (var pet in userPets)
                {
                    // delete existing image if exists
                    if (pet.ImageFileName != null)
                    {
                        try
                        {
                            await _azureBlobStorageService.DeleteImagesFromAzure(ContainerTypeEnum.PetImage, pet.ImageFileName, user!.Id);  // delete the image in the current petDTO.ImageFileName property
                        }
                        catch (AzureBlobStorageException ex)
                        {
                            Debug.WriteLine($"AZURE BLOB DELETE EXCEPTION: {ex.Message}");
                            errorMessages.Add("Azure Blob Storage exception on delete. The previous image could not be deleted.");
                        }
                    }
                    // DeleteImage deleteImage = new DeleteImage() { ImageURL = pet.ImageURL, OwnedBy = user!.Id, IsDeleted = false };
                    // await _unitOfWork.DeleteImages.AddAsync(deleteImage);
                    await _unitOfWork.Pets.RemoveAsync(pet);
                }

                // delete all user vets
                IEnumerable<Vet> userVets = await _unitOfWork.Vets.GetAllAsync(filter: v => v.OwnedBy == user!.Id);
                vetCount = userVets.Count();
                foreach (var vet in userVets) await _unitOfWork.Vets.RemoveAsync(vet);
                await _unitOfWork.SaveAsync();

                await _userManager.DeleteAsync(user!);

                string successMessage = $"{petCount} pets were deleted.\n{vetCount} veterinarians were deleted.\n{visitCount} care visits were deleted.";
                return (true, successMessage, null);
            }
        }
    }
}
