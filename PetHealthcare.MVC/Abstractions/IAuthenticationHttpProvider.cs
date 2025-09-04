using PetHealthcare.MVC.DTOs;

namespace PetHealthcare.MVC.Abstractions
{
    public interface IAuthenticationHttpProvider
    {
        Task<(bool IsSuccess, List<string>? ErrorMessages)> RegisterUserAsync(RegisterUserDTO registerDTO);
        Task<(bool IsSuccess, LoginResponseDTO? ResponseDTO, List<string>? ErrorMessages)> LoginUserAsync(LoginUserDTO loginDTO);
        Task<(bool IsSuccess, List<string>? ErrorMessages)> UpdatePasswordAsync(UpdatePasswordDTO updatePasswordDTO, string token = "");
        Task<(bool IsSuccess, string? SuccessMessage, List<string>? ErrorMessages)> DeleteAccountAsync(DeleteAccountDTO deleteAccountDTO, string token = "");

    }
}
