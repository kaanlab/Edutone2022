using Edutone2022.Common.Models.Auth;
using Edutone2022.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Edutone2022.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(LoginResponse))]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await authService.Login(request);
            return response.IsSuccessful ? Ok(response) : Unauthorized(response);
        }
    }
}
