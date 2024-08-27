using AuthandAuthrizations.Data;
using AuthandAuthrizations.Models;
using AuthandAuthrizations.Models.DTO;
using EmployeeManagementAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthandAuthrizations.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly EmployeeDbContext _context;
        private readonly TokenService _tokenService;
        public AuthController(EmployeeDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Employee employee)
        {
            employee.Password = BCrypt.Net.BCrypt.HashPassword(employee.Password);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var employee = await _context.Employees.SingleOrDefaultAsync(e => e.Email == login.Email);
            if (employee == null)
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }

            // Verify the provided password against the stored hash
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(login.Password, employee.Password);
            if (!isPasswordValid)
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }

            // Generate JWT token if the password is valid
            var token = _tokenService.GenerateToken(employee);
            return Ok(new
            {
                Employee = employee,
                Token = token
            });
        }
    }

}
