using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.DTOs;
using System.Security.Claims;

namespace PetHealthcare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VetsController : ControllerBase
    {
        private readonly IVetService _vetService;

        public VetsController(IVetService vetService)
        {
            _vetService = vetService;
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllVets()
        {
            // get current user id
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
            {
                (bool IsSuccess, IEnumerable<VetDTO>? Vets, string? ErrorMessage) result = new();
                // if admin return all pets without filtering by user id

                if (User.IsInRole("Admin"))     // ... get all no filter
                {
                    result = await _vetService.GetAllVetsAsync();

                }
                else
                {
                    string userId = userIdClaim!.Value;
                    result = await _vetService.GetAllVetsAsync(userId);
                }

                if (result.IsSuccess) return Ok(result.Vets);
                else
                {
                    if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                    else return BadRequest("Unknown Error getting vets.");
                }
            }
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetVet(int id)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
            {
                (bool IsSuccess, VetDTO? Vet, string? ErrorMessage) result = new();

                if (User.IsInRole("Admin"))     // ... get all no filter
                {
                    result = await _vetService.GetVetAsync(id);

                }
                else
                {
                    string userId = userIdClaim!.Value;
                    result = await _vetService.GetVetAsync(id, userId);
                }

                if (result.IsSuccess) return Ok(result.Vet);
                else
                {
                    if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                    else return BadRequest("Unknown Error getting the vet.");
                }
            }
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddVet([FromBody] VetDTO vetDTO)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
            {
                vetDTO.OwnedBy = userIdClaim.Value;
            }

            var result = await _vetService.AddVetAsync(vetDTO);
            if (result.IsSuccess) return StatusCode(StatusCodes.Status201Created);
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                else return BadRequest("Unknown Error adding the vet dude!");
            }
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> EditVet(int id, [FromBody] VetDTO vetDTO)
        {
            // verify vet is owned by current user (vetDTO.OwnedBy will be null at this point as comes from MVC and want to be client agnostic)
            if (!User.IsInRole("Admin"))
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var validationResult = await _vetService.GetVetAsync(id);
                if (validationResult.IsSuccess && validationResult.Vet != null)
                {
                    if (validationResult.Vet.OwnedBy != userIdClaim!.Value) return StatusCode(StatusCodes.Status401Unauthorized);
                    else vetDTO.OwnedBy = userIdClaim.Value;
                }
            }

            var result = await _vetService.EditVetAsync(id, vetDTO);
            if (result.IsSuccess) return StatusCode(StatusCodes.Status204NoContent);
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                else return BadRequest("Unknown Error editing the vet dude!");
            }
        }

        [Authorize]
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteVet(int id)
        {
            // verify vet is owned by current user
            if (!User.IsInRole("Admin"))
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var validationResult = await _vetService.GetVetAsync(id);
                if (validationResult.IsSuccess && validationResult.Vet != null)
                {
                    if (validationResult.Vet.OwnedBy != userIdClaim!.Value) return StatusCode(StatusCodes.Status401Unauthorized);
                }
            }

            var result = await _vetService.DeleteVetAsync(id);
            if (result.IsSuccess) return StatusCode(StatusCodes.Status204NoContent);
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                else return BadRequest("Unknown Error deleting the vet dude!");
            }
        }

    }
}
