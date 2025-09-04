using Microsoft.AspNetCore.Identity;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.DTOs;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.API.Services
{
    public class UpdatePasswordService : IUpdatePasswordService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public UpdatePasswordService(UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        public async Task<(bool IsSuccess, List<string>? ErrorMessages)> UpdateUserPasswordAsync(string userId, UpdatePasswordDTO updatePasswordDTO)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId);

            var hashedNewPassword = _passwordHasher.HashPassword(user!, updatePasswordDTO.NewPassword!);
            user!.PasswordHash = hashedNewPassword;
            await _userManager.UpdateAsync(user);

            return (true, null);
        }
    }
}
