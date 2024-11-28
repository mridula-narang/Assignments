using AutoMapper;
using InsuraceClaimApp.Interfaces;
using InsuraceClaimApp.Models;
using InsuraceClaimApp.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuraceClaimApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;
        private readonly IMapper _mapper;

        public PolicyController(IPolicyService policyService, IMapper mapper)
        {
            _policyService = policyService;
            _mapper = mapper;
        }

        // Get all policies for a specific user
        [HttpGet("GetPoliciesForUser/{username}")]
        public async Task<IActionResult> GetPoliciesForUser(string username)
        {
            var policies = await _policyService.GetAllPoliciesAsync();
            var userPolicies = policies.Where(p => p.UserName == username).ToList();

            if (userPolicies == null || !userPolicies.Any())
            {
                return NotFound("No policies found for this user.");
            }

            return Ok(userPolicies);
        }

        // Add a new policy
        [HttpPost("AddPolicy")]
        public async Task<IActionResult> AddPolicy([FromBody] PolicyDTO newPolicyDto)
        {
            if (newPolicyDto == null || string.IsNullOrEmpty(newPolicyDto.PolicyName) || string.IsNullOrEmpty(newPolicyDto.PolicyType) || string.IsNullOrEmpty(newPolicyDto.UserName))
            {
                return BadRequest("Invalid policy details.");
            }

            try
            {
                // Map DTO to Policy entity
                var newPolicy = _mapper.Map<Policy>(newPolicyDto);
                var addedPolicy = await _policyService.AddPolicyAsync(newPolicy);
                return CreatedAtAction(nameof(GetPolicyById), new { id = addedPolicy.PolicyId }, addedPolicy);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get a single policy by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolicyById(int id)
        {
            var policy = await _policyService.GetPolicyByIdAsync(id);
            if (policy == null)
            {
                return NotFound();
            }
            return Ok(policy);
        }
    }
}
