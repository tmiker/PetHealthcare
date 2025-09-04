using PetHealthcare.API.DTOs;

namespace PetHealthcare.API.Abstractions
{
    public interface IRegisterUserService
    {
        Task<(bool IsSuccess, List<string>? ErrorMessages)> RegisterUserAsync(RegisterUserDTO registerUserDTO);
    }
}
