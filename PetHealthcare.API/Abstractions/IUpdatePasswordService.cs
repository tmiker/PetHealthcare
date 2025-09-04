using PetHealthcare.API.DTOs;

namespace PetHealthcare.API.Abstractions
{
    public interface IUpdatePasswordService
    {
        Task<(bool IsSuccess, List<string>? ErrorMessages)> UpdateUserPasswordAsync(string userId, UpdatePasswordDTO updatePasswordDTO);
    }
}
