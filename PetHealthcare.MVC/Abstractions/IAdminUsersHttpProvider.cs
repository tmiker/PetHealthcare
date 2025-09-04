using PetHealthcare.MVC.DTOs;

namespace PetHealthcare.MVC.Abstractions
{
    public interface IAdminUsersHttpProvider
    {
        Task<(bool IsSuccess, IEnumerable<UserInfoDTO>? UserInfoDTOs, string? ErrorMessage)> GetAllUsersAsync(string token = "");
        Task<(bool IsSuccess, string? ErrorMessage)> LockAsync(string id, string token = "");
        Task<(bool IsSuccess, string? ErrorMessage)> UnLockAsync(string id, string token = "");
        Task<(bool IsSuccess, string? SuccessMessage, string? ErrorMessage)> FlipUserAdminRoleAsync(string id, string token = "");
        Task<(bool IsSuccess, string? SuccessMessage, string? ErrorMessage)> FlipUserManagerRoleAsync(string id, string token = "");
        Task<(bool IsSuccess, string? SuccessMessage, string? ErrorMessage)> FlipUserEmployeeRoleAsync(string id, string token = "");
        Task<(bool IsSuccess, string? SuccessMessage, string? ErrorMessage)> FlipUserCustomerRoleAsync(string id, string token = "");
        Task<(bool IsLocked, string? ErrorMessage)> UserIsLockedAsync(string id, string token = "");
        Task<(bool IsUnlocked, string? ErrorMessage)> UserIsUnlockedAsync(string id, string token = "");
    }
}
