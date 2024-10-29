using AutoMapper;
using InsuraceClaimApp.Interfaces;
using InsuraceClaimApp.Models.DTOs;
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
        public async Task<IActionResult> GetAllClaims()
        {
            var claims = await _claimService.GetAllClaimsAsync();
            return Ok(claims);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClaim(int id)
        {
            await _claimService.DeleteClaimAsync(id);
            return NoContent();
        }
    }
}
