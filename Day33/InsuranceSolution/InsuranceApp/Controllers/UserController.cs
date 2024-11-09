using InsuranceApp.Interfaces;
using InsuranceApp.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<LoginResponseDTO>> Register(UserCreateDTO createDTO)
        {

            if (ModelState.IsValid)
            {
                var user = await _userService.Register(createDTO);
                return Ok(user);
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

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO requestDTO)
        {
            var user = await _userService.Autheticate(requestDTO);
            return Ok(user);
        }
    }
}
