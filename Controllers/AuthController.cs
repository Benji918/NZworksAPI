using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZworks.Data;
using NZworks.Models.DTO;

namespace NZworks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        //private readonly NzWalksAuthDBContext nzWalksAuthDBContext;
        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
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
            if (user != null && request.Roles != null)
            {
                var result = await userManager.AddToRolesAsync(user, request.Roles);

                if (!result.Succeeded)
                {
                    return BadRequest(identityResult.Errors);
                }
            }


            return Ok(new { Message = "User registered successfully. Please login!" });
        }
    }
}
