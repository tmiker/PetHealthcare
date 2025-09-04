using PetHealthcare.API.DTOs;

namespace PetHealthcare.API.Abstractions
{
    public interface IDeleteAccountService
    {
        Task<(bool IsSuccess, string? SuccessMessage, List<string>? ErrorMessages)> DeleteAccountAsync(DeleteAccountDTO deleteAccountDTO);
    }
}
