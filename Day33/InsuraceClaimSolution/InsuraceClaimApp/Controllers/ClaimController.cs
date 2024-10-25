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

        public ClaimController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        [HttpPost("report")]
        public async Task<IActionResult> ReportClaim([FromForm] ClaimRequestDTO claimRequest)
        {
            try
            {
                var claim = await _claimService.AddClaimAsync(claimRequest);
                return CreatedAtAction(nameof(GetClaim), new { id = claim.ClaimId }, claim);

            }
            catch (Exception)
            {
                return BadRequest();
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
