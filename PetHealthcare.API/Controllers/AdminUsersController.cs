using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.API.Abstractions;

namespace PetHealthcare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUsersController : ControllerBase
    {
        private readonly IAdminUserService _adminUserService;

        public AdminUsersController(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _adminUserService.GetAllUsersAsync();
            if (result.IsSuccess) return Ok(result.UserInfoDTOs);
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0)
                {
                    return BadRequest(result.ErrorMessage);
                }
                else return BadRequest("Unknown error getting users. Please contact support.");
            }
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> LockUser(string id)
        {
            var result = await _adminUserService.LockAsync(id);
            if (result.IsSuccess) return StatusCode(StatusCodes.Status204NoContent);
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0)
                {
                    return BadRequest(result.ErrorMessage);
                }
                else return BadRequest("Unknown error locking user. Please contact support.");
            }
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> UnlockUser(string id)
        {
            var result = await _adminUserService.UnLockAsync(id);
            if (result.IsSuccess) return StatusCode(StatusCodes.Status204NoContent);
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0)
                {
                    return BadRequest(result.ErrorMessage);
                }
                else return BadRequest("Unknown error unlocking user. Please contact support.");
            }
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> FlipUserAdminRole(string id)
        {
            var result = await _adminUserService.FlipUserAdminRole(id);
            if (result.IsSuccess)
            {
                if (result.SuccessMessage != null && result.SuccessMessage.Length > 0) return Ok(result.SuccessMessage);
                else return StatusCode(StatusCodes.Status204NoContent);
            }
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0)
                {
                    return BadRequest(result.ErrorMessage);
                }
                else return BadRequest("Unknown error managing user roles. Please contact support.");
            }
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> FlipUserManagerRole(string id)
        {
            var result = await _adminUserService.FlipUserManagerRole(id);
            if (result.IsSuccess)
            {
                if (result.SuccessMessage != null && result.SuccessMessage.Length > 0) return Ok(result.SuccessMessage);
                else return StatusCode(StatusCodes.Status204NoContent);
            }
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0)
                {
                    return BadRequest(result.ErrorMessage);
                }
                else return BadRequest("Unknown error managing user roles. Please contact support.");
            }
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> FlipUserEmployeeRole(string id)
        {
            var result = await _adminUserService.FlipUserEmployeeRole(id);
            if (result.IsSuccess)
            {
                if (result.SuccessMessage != null && result.SuccessMessage.Length > 0) return Ok(result.SuccessMessage);
                else return StatusCode(StatusCodes.Status204NoContent);
            }
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0)
                {
                    return BadRequest(result.ErrorMessage);
                }
                else return BadRequest("Unknown error managing user roles. Please contact support.");
            }
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> FlipUserCustomerRole(string id)
        {
            var result = await _adminUserService.FlipUserCustomerRole(id);
            if (result.IsSuccess)
            {
                if (result.SuccessMessage != null && result.SuccessMessage.Length > 0) return Ok(result.SuccessMessage);
                else return StatusCode(StatusCodes.Status204NoContent);
            }
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0)
                {
                    return BadRequest(result.ErrorMessage);
                }
                else return BadRequest("Unknown error managing user roles. Please contact support.");
            }
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> IsUserLocked(string id)
        {
            var result = await _adminUserService.UserIsLockedAsync(id);
            if (result.IsLocked) return Ok("true");
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0)
                {
                    return BadRequest(result.ErrorMessage);
                }
                else return BadRequest("Unknown error checking lock status. Please contact support.");
            }
        }

        [Authorize(Policy = "IsAdmin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> IsUserUnlocked(string id)
        {
            var result = await _adminUserService.UserIsUnlockedAsync(id);
            if (result.IsUnlocked) return Ok("true");
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0)
                {
                    return BadRequest(result.ErrorMessage);
                }
                else return BadRequest("Unknown error checking unlock status. Please contact support.");
            }
        }
    }
}
