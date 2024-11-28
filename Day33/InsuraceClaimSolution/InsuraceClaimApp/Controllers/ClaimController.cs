using AutoMapper;
using InsuraceClaimApp.Interfaces;
using InsuraceClaimApp.Models;
using InsuraceClaimApp.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuraceClaimApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _claimService;

        public ClaimController(IClaimService claimService,IMapper mapper)
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
        public async Task<IActionResult> GetClaim(int id)
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

        //only admin can approve a claim
        [HttpPut("approve/{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ApproveClaim(int id)
        {
            var claim = await _claimService.GetClaimByIdAsync(id);
            if (claim == null)
            {
                return NotFound();
            }
            claim.Status = ClaimStatus.Approved;
            var claimRequest = new ClaimRequestDTO
            {
                PolicyId = claim.PolicyNumber, 
                ClaimType = claim.ClaimType,
                IncidentDate = claim.IncidentDate,
                UserName = claim.UserName,
                PhoneNumber = claim.PhoneNumber,
                Email = claim.Email,
                Amount = claim.Amount
            };
            await _claimService.UpdateClaimAsync(id, claimRequest);
            //return status approved with status code 200
            return Ok("Claim approved");
        }
    }
}
