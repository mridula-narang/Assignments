using AutoMapper;
using InsuranceApp.Interfaces;
using InsuranceApp.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _claimService;

        public ClaimController(IClaimService claimService, IMapper mapper)
        {
            _claimService = claimService;
        }

        [HttpPost("report")]

        [Authorize(Roles = "Claimant")] //only claimant can report a claim
        public async Task<IActionResult> ReportClaim([FromForm] ClaimRequestDTO claimRequest)
        {
            if (ModelState.IsValid)
            {
                var claim = await _claimService.AddClaimAsync(claimRequest);
                return Ok(claim);
            }
            else
            {
                return BadRequest(new ErrorResponseDTO
                {
                    ErrorMessage = "one or more validation errors",
                    ErrorNumber = 400
                });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetClaimById(int id)
        {
            var claim = await _claimService.GetClaimByIdAsync(id);
            if (claim == null)
            {
                return NotFound();
            }
            return Ok(claim);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllClaims()
        {
            var claims = await _claimService.GetAllClaimsAsync();
            return Ok(claims);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] //only admin can delete a claim
        public async Task<IActionResult> DeleteClaim(int id)
        {
            await _claimService.DeleteClaimAsync(id);
            return NoContent();
        }

        //method to approve a claim. only admin can approve a claim
        [HttpPut("{id}/approve")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveClaim(int id)
        {
            var claim = await _claimService.ApproveClaimAsync(id);
            return Ok(claim);
        }

    }
}
