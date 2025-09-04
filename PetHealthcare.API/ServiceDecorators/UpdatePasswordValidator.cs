using Microsoft.AspNetCore.Identity;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.DTOs;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.API.ServiceDecorators
{
    public class UpdatePasswordValidator : IUpdatePasswordService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IUpdatePasswordService _baseService;

        public UpdatePasswordValidator(UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> passwordHasher, IUpdatePasswordService baseService)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _baseService = baseService;
        }

        public async Task<(bool IsSuccess, List<string>? ErrorMessages)> UpdateUserPasswordAsync(string userId, UpdatePasswordDTO updatePasswordDTO)
        {
            List<string> errorMessages = new List<string>();
            if (string.IsNullOrWhiteSpace(updatePasswordDTO.OldPassword) || string.IsNullOrWhiteSpace(updatePasswordDTO.Email) ||
                string.IsNullOrWhiteSpace(updatePasswordDTO.NewPassword) || string.IsNullOrWhiteSpace(updatePasswordDTO.ConfirmNewPassword))
            {
                errorMessages.Add("Invalid email or password.");
                
            }
            if (updatePasswordDTO.NewPassword != updatePasswordDTO.ConfirmNewPassword)
            {
                errorMessages.Add("The new Password and confirm  new Password must be the same.");
            }

            if (errorMessages.Any()) return (false, errorMessages);
            else
            {
                ApplicationUser? userFromId = await _userManager.FindByIdAsync(userId);

                ApplicationUser? userFromEmail = await _userManager.FindByEmailAsync(updatePasswordDTO.Email!);

                if (userFromId is null || userFromEmail is null || userFromEmail != userFromId || userFromEmail == null)
                {
                    errorMessages.Add("Invalid email or original password.");
                    return (false, errorMessages);
                }

                var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(userFromId, userFromId.PasswordHash!, updatePasswordDTO.OldPassword!);

                if (passwordVerificationResult != PasswordVerificationResult.Success)
                {
                    errorMessages.Add("Invalid email or original password.");
                }

                if (!errorMessages.Any()) return await _baseService.UpdateUserPasswordAsync(userId, updatePasswordDTO);
                else return (false, errorMessages);
            }
        }
    }
}
