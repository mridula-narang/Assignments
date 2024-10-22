using EventBookingApp.Exceptions;
using EventBookingApp.Interfaces;
using EventBookingApp.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;

namespace EventBookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> Get()
        {
            try
            {
                var employees = await _employeeService.GetAll();
                return Ok(employees);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        //post method to create employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeDTO employee)
        {
            try
            {
                var employeeId = await _employeeService.Add(employee);
                return Ok(employeeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating customer");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        //linq query to fetch the employee name that belongs to marketing department
        [HttpGet("marketing")]
        public async Task<ActionResult<IEnumerable<string>>> GetMarketingEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAll();
                var marketingEmployees = (from e in employees
                                          where e.Department == "marketing"
                                          select e.Name).ToList();

                if (!marketingEmployees.Any())
                {
                    throw new NotFoundException("No employees found in the marketing department.");
                }

                return Ok(marketingEmployees);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
