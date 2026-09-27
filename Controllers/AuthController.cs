using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZworks.Data;
using NZworks.Models.DTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZworks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
        {
            var identityResult = await userManager.CreateAsync(new IdentityUser { UserName = request.Username, Email = request.Username },
                 request.Password);

            if (!identityResult.Succeeded)
            {
                return BadRequest(identityResult.Errors);
            }

            // Add roles to this user if needed
            var user = await userManager.FindByNameAsync(request.Username);
            var existingRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

            if (request.Roles.Any(role => !existingRoles.Contains(role)))
            {
                return BadRequest(new { Message = "One or more roles do not exist" });
            }
            if (user != null && request.Roles != null)
            {
                var result = await userManager.AddToRolesAsync(user, request.Roles);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
            }

            return Ok(new { Message = "User registered successfully. Please login!" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var user = await userManager.FindByNameAsync(request.Username);
            if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
            {
                return Unauthorized(new { Message = "Invalid username or password" });
            }
            // Generate JWT token here (not implemented in this snippet)
            // You can use libraries like System.IdentityModel.Tokens.Jwt to generate the token
            return Ok(new { Message = "Login successful" });
        }


    }
}
