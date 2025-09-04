using PetHealthcare.API.DTOs;

namespace PetHealthcare.API.Abstractions
{
    public interface IAdminUserService
    {
        Task<(bool IsSuccess, ICollection<UserInfoDTO>? UserInfoDTOs, string? ErrorMessage)> GetAllUsersAsync();
        Task<(bool IsSuccess, string? ErrorMessage)> LockAsync(string id);
        Task<(bool IsSuccess, string? ErrorMessage)> UnLockAsync(string id);
        Task<(bool IsSuccess, string? SuccessMessage, string? ErrorMessage)> FlipUserAdminRole(string id);
        Task<(bool IsSuccess, string? SuccessMessage, string? ErrorMessage)> FlipUserManagerRole(string id);
        Task<(bool IsSuccess, string? SuccessMessage, string? ErrorMessage)> FlipUserEmployeeRole(string id);
        Task<(bool IsSuccess, string? SuccessMessage, string? ErrorMessage)> FlipUserCustomerRole(string id);
        Task<(bool IsLocked, string? ErrorMessage)> UserIsLockedAsync(string id);
        Task<(bool IsUnlocked, string? ErrorMessage)> UserIsUnlockedAsync(string id);
    }
}
