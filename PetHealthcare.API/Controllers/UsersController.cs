using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.DTOs;
using System.Security.Claims;

namespace PetHealthcare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRegisterUserService _registerUserService;
        private readonly ILoginUserService _loginUserService;
        private readonly IUpdatePasswordService _updatePasswordService;
        private readonly IDeleteAccountService _deleteAccountService;

        public UsersController(IRegisterUserService registerUserService, ILoginUserService loginUserService, 
            IUpdatePasswordService updatePasswordService, IDeleteAccountService deleteAccountService)
        {
            _registerUserService = registerUserService;
            _loginUserService = loginUserService;
            _updatePasswordService = updatePasswordService;
            _deleteAccountService = deleteAccountService;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterUserDTO registerUserDTO)
        {
            var result = await _registerUserService.RegisterUserAsync(registerUserDTO);
            if (result.IsSuccess)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                if (result.ErrorMessages != null && result.ErrorMessages.Count > 0)
                {
                    return BadRequest(result.ErrorMessages);
                }
                else return BadRequest(new List<string> { "Unknown error registering user. Please contact support." });
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserDTO loginUserDTO)
        {
            var result = await _loginUserService.LoginUserAsync(loginUserDTO);
            if (result.IsSuccess)
            {
                return Ok(result.ResponseDTO);
            }
            else
            {
                if (result.ErrorMessages != null && result.ErrorMessages.Count > 0)
                {
                    return BadRequest(result.ErrorMessages);
                }
                else return BadRequest(new List<string> { "Unknown error logging in user. Please contact support if this continues." });
            }
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDTO updatePasswordDTO)
        {
            string userId = string.Empty;
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null) userId = userIdClaim.Value;

            var result = await _updatePasswordService.UpdateUserPasswordAsync(userId, updatePasswordDTO);
            if (result.IsSuccess)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
            else
            {
                if (result.ErrorMessages != null && result.ErrorMessages.Count > 0)
                {
                    return BadRequest(result.ErrorMessages);
                }
                else return BadRequest(new List<string> { "Unknown error updating password. Please contact support." });
            }
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteAccount([FromBody] DeleteAccountDTO deleteAccountDTO)
        {
            var result = await _deleteAccountService.DeleteAccountAsync(deleteAccountDTO);
            if (result.IsSuccess)
            {
                if (result.SuccessMessage != null) return Ok(result.SuccessMessage);
                else return StatusCode(StatusCodes.Status204NoContent);  // should return a string with entity counts
            }
            else
            {
                if (result.ErrorMessages != null && result.ErrorMessages.Count > 0)
                {
                    return BadRequest(result.ErrorMessages);
                }
                else return BadRequest(new List<string> { "Unknown error deleting account. Please contact support." });
            }
        }
    }
}
