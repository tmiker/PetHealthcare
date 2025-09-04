using Microsoft.AspNetCore.Identity;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.DTOs;
using PetHealthcare.Domain.Models;
using System.Text.RegularExpressions;

namespace PetHealthcare.API.ServiceDecorators
{
    public class RegisterUserValidator : IRegisterUserService
    {
        private readonly IRegisterUserService _baseService;
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterUserValidator(IRegisterUserService baseService, UserManager<ApplicationUser> userManager)
        {
            _baseService = baseService;
            _userManager = userManager;
        }

        public async Task<(bool IsSuccess, List<string>? ErrorMessages)> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            List<string> errorMessages = new List<string>();
            var isValidPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$");
            var isValidEmail = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");

            if (registerUserDTO.Username.Contains(" "))
            {
                errorMessages.Add("UserName cannot contain any spaces.");
            }
            if (registerUserDTO.Username == null)
            {
                errorMessages.Add("Please provide an Email.");
            }
            if (registerUserDTO.Password == null)
            {
                errorMessages.Add("Please provide a Password.");
            }
            else
            {
                if (!isValidPassword.IsMatch(registerUserDTO.Password))
                {
                    errorMessages.Add("Password must be from 8 to 15 characters and contain one upper case letter, one lower case letter, and one number.");
                }
            }
            if (registerUserDTO.Password != registerUserDTO.ConfirmPassword)
            {
                errorMessages.Add("Password and Confirm Password must be the same.");
            }
            if (registerUserDTO.Email == null)
            {
                errorMessages.Add("Please provide an Email.");
            }
            else
            {
                if (!isValidEmail.IsMatch(registerUserDTO.Email))
                {
                    errorMessages.Add("Email format is not valid.");
                }
            }
            if (errorMessages.Count > 0) return (false, errorMessages);

            // check if email is already registered and add to list
            ApplicationUser? existingUser = await _userManager.FindByEmailAsync(registerUserDTO.Email!);
            if (existingUser != null) errorMessages.Add($"A user with email {registerUserDTO.Email} is already registered.");

            if (errorMessages.Count == 0) return await _baseService.RegisterUserAsync(registerUserDTO);
            else return (false, errorMessages);
        }
    }
}
