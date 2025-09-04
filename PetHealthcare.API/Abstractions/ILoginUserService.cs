using PetHealthcare.API.DTOs;

namespace PetHealthcare.API.Abstractions
{
    public interface ILoginUserService
    {
        Task<(bool IsSuccess, LoginResponseDTO? ResponseDTO, List<string>? ErrorMessages)> LoginUserAsync(LoginUserDTO loginUserDTO);
    }
}
