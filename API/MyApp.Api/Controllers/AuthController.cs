using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MyApp.Api.Models.Domain.DTO;

namespace CodePulse.API.Controllers
{
    //https://localhost:xxxx/api/auth   //AuthController is changed to Auth by [conroller]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
      
        //POST: {apibaseutl}/api/auth/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            // Create the identity user object
            var user = new IdentityUser{
                UserName = request.Email?.Trim(),
                Email = request.Email?.Trim(),

            };

            // Creatte user
            var identityResult = await userManager.CreateAsync(user, request.Password);
            if (identityResult.Succeeded)
            {
                // Add role to user (Reader)
                identityResult = await userManager.AddToRoleAsync(user, "Reader");

                if (identityResult.Succeeded)
                {
                    return Ok();
                }
                else {
                    if (identityResult.Errors.Any())
                    {
                        foreach(var error in identityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else {
                if (identityResult.Errors.Any())
                {
                    foreach(var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return ValidationProblem(ModelState);
        }

        //POST: {apibaseutl}/api/auth/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            // Check if user exists
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                 // Check password
                var result = await userManager.CheckPasswordAsync(user, request.Password);
                if (result)
                {   
                    var roles = await userManager.GetRolesAsync(user);
                    // create a token and response

                    var response = new LoginResponseDto()
                    {
                        Email = request.Email,
                        Roles = roles.ToList(),
                        Token = "TOKEN" // This should be replaced with actual token generation logic
                    };

                    return Ok(response);
                }
                
            }

            ModelState.AddModelError("", "User not found");
            return ValidationProblem(ModelState);

           
        }

    }
}