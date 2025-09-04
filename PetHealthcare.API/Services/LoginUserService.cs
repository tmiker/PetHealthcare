using Microsoft.AspNetCore.Identity;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.DTOs;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.API.Services
{
    public class LoginUserService : ILoginUserService
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginUserService(ITokenProvider tokenProvider, UserManager<ApplicationUser> userManager)
        {
            _tokenProvider = tokenProvider;
            _userManager = userManager;
        }

        public async Task<(bool IsSuccess, LoginResponseDTO? ResponseDTO, List<string>? ErrorMessages)> LoginUserAsync(LoginUserDTO loginUserDTO)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(loginUserDTO.Email);

            LoginResponseDTO responseDTO = new LoginResponseDTO()
            {
                JwtToken = await _tokenProvider.GetJwtToken(user),
                RefreshToken = _tokenProvider.GetRefreshToken()
            };

            user.RefreshToken = responseDTO.RefreshToken;
            await _userManager.UpdateAsync(user);

            return (true, responseDTO, null);
        }
    }
}
