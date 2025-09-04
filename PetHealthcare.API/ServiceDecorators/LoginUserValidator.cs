using Microsoft.AspNetCore.Identity;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.DTOs;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.API.ServiceDecorators
{
    public class LoginUserValidator : ILoginUserService
    {
        private readonly ILoginUserService _baseService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public LoginUserValidator(ILoginUserService baseService, UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _baseService = baseService;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        public async Task<(bool IsSuccess, LoginResponseDTO? ResponseDTO, List<string>? ErrorMessages)> LoginUserAsync(LoginUserDTO loginUserDTO)
        {
            List<string> errorMessages = new List<string>();
            if (string.IsNullOrWhiteSpace(loginUserDTO.Password) || string.IsNullOrWhiteSpace(loginUserDTO.Email))
            {
                errorMessages.Add("Invalid email or password.");
            }

            // check if user exists and if password is correct
            ApplicationUser? existingUser = await _userManager.FindByEmailAsync(loginUserDTO.Email);
            if (existingUser == null) errorMessages.Add($"Invalid email or password.");
            else
            {
                var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(existingUser, existingUser.PasswordHash!, loginUserDTO.Password);
                if (passwordVerificationResult != PasswordVerificationResult.Success) errorMessages.Add($"Invalid email or password.");
            }

            if (errorMessages.Count == 0) return await _baseService.LoginUserAsync(loginUserDTO);
            else return (false, null, errorMessages);
        }
    }
}
