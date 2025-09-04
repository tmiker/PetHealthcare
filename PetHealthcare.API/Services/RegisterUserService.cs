using Microsoft.AspNetCore.Identity;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.DTOs;
using PetHealthcare.Domain.Models;

namespace PetHealthcare.API.Services
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly ICustomerNumberGenerator _customerNumberGenerator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public RegisterUserService(ICustomerNumberGenerator customerNumberGenerator, UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _customerNumberGenerator = customerNumberGenerator;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        public async Task<(bool IsSuccess, List<string>? ErrorMessages)> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = registerUserDTO.Username,
                Email = registerUserDTO.Email,
                CustomerNumber = _customerNumberGenerator.GenerateCustomerNumber()
            };

            string passwordHash = _passwordHasher.HashPassword(user, registerUserDTO.Password);
            user.PasswordHash = passwordHash;

            await _userManager.CreateAsync(user);
            await _userManager.AddToRoleAsync(user, "Customer");
            await _userManager.UpdateAsync(user);
            return (true, null);
        }
    }
}
