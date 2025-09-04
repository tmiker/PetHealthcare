using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHealthcare.API.Abstractions;
using PetHealthcare.API.DTOs;
using System.Security.Claims;

namespace PetHealthcare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitsController : ControllerBase
    {
        private readonly IVisitService _visitService;

        public VisitsController(IVisitService visitService)
        {
            _visitService = visitService;
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllVisits()
        {
            // get current user id
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
            {
                (bool IsSuccess, IEnumerable<VisitDTO>? Visits, string? ErrorMessage) result = new();
                // if admin return all pets without filtering by user id
                if (User.IsInRole("Admin"))     // ... get all no filter
                {
                    result = await _visitService.GetAllVisitsAsync();
                }
                else
                {
                    string userId = userIdClaim!.Value;
                    result = await _visitService.GetAllVisitsAsync(userId);
                }

                if (result.IsSuccess) return Ok(result.Visits);
                else
                {
                    if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                    else return BadRequest("Unknown Error getting visits.");
                }
            }
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllVisitAggregates()
        {
            // get current user id
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
            {
                (bool IsSuccess, IEnumerable<VisitAggregateDTO>? VisitAggregates, string? ErrorMessage) result = new();
                // if admin, return all visits without filtering by user id
                if (User.IsInRole("Admin"))     // ... get all no filter
                {
                    result = await _visitService.GetAllVisitAggregatesAsync();
                }
                else
                {
                    string userId = userIdClaim!.Value;
                    result = await _visitService.GetAllVisitAggregatesAsync(userId);
                }

                if (result.IsSuccess) return Ok(result.VisitAggregates);
                else
                {
                    if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                    else return BadRequest("Unknown Error getting visits.");
                }
            }
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllVisitAggregatesByPet(int id)
        {
            // get current user id
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
            {
                (bool IsSuccess, IEnumerable<VisitAggregateDTO>? VisitAggregates, string? ErrorMessage) result = new();
                // if admin, return all visits without filtering by user id
                if (User.IsInRole("Admin"))     // ... get all no filter
                {
                    result = await _visitService.GetAllVisitAggregatesByPetAsync(id);
                }
                else
                {
                    string userId = userIdClaim!.Value;
                    result = await _visitService.GetAllVisitAggregatesByPetAsync(id, userId);
                }

                if (result.IsSuccess) return Ok(result.VisitAggregates);
                else
                {
                    if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                    else return BadRequest("Unknown Error getting visits.");
                }
            }
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetVisit(int id)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
            {
                (bool IsSuccess, VisitDTO? Visit, string? ErrorMessage) result = new();

                if (User.IsInRole("Admin"))     // ... get all no filter
                {
                    result = await _visitService.GetVisitAsync(id);

                }
                else
                {
                    string userId = userIdClaim!.Value;
                    result = await _visitService.GetVisitAsync(id, userId);
                }

                if (result.IsSuccess) return Ok(result.Visit);
                else
                {
                    if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                    else return BadRequest("Unknown Error getting the visit.");
                }
            }
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetVisitAggregate(int id)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
            {
                (bool IsSuccess, VisitAggregateDTO? Visit, string? ErrorMessage) result = new();

                if (User.IsInRole("Admin"))     // ... get all no filter
                {
                    result = await _visitService.GetVisitAggregateAsync(id);

                }
                else
                {
                    string userId = userIdClaim!.Value;
                    result = await _visitService.GetVisitAggregateAsync(id, userId);
                }

                if (result.IsSuccess) return Ok(result.Visit);
                else
                {
                    if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                    else return BadRequest("Unknown Error getting the visit.");
                }
            }
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddVisit([FromBody] VisitDTO visitDTO)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            else
            {
                visitDTO.OwnedBy = userIdClaim.Value;
            }

            var result = await _visitService.AddVisitAsync(visitDTO);
            if (result.IsSuccess) return StatusCode(StatusCodes.Status201Created);
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                else return BadRequest("Unknown Error adding the visit.");
            }
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> EditVisit(int id, [FromBody] VisitDTO visitDTO)
        {
            // verify visit is owned by current user (visitDTO.OwnedBy will be null at this point as comes from MVC and want to be client agnostic)
            if (!User.IsInRole("Admin"))
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var validationResult = await _visitService.GetVisitAsync(id);
                if (validationResult.IsSuccess && validationResult.Visit != null)
                {
                    if (validationResult.Visit.OwnedBy != userIdClaim!.Value) return StatusCode(StatusCodes.Status401Unauthorized);
                    else visitDTO.OwnedBy = userIdClaim.Value;
                }
            }

            var result = await _visitService.EditVisitAsync(id, visitDTO);
            if (result.IsSuccess) return StatusCode(StatusCodes.Status204NoContent);
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                else return BadRequest("Unknown Error editing the visit.");
            }
        }

        [Authorize]
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteVisit(int id)
        {
            // verify visit is owned by current user
            if (!User.IsInRole("Admin"))
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                var validationResult = await _visitService.GetVisitAsync(id);
                if (validationResult.IsSuccess && validationResult.Visit != null)
                {
                    if (validationResult.Visit.OwnedBy != userIdClaim!.Value) return StatusCode(StatusCodes.Status401Unauthorized);
                }
            }

            var result = await _visitService.DeleteVisitAsync(id);
            if (result.IsSuccess) return StatusCode(StatusCodes.Status204NoContent);
            else
            {
                if (result.ErrorMessage != null && result.ErrorMessage.Length > 0) return BadRequest(result.ErrorMessage);
                else return BadRequest("Unknown Error deleting the visit.");
            }
        }
    }
}
