using System.Web;
using Microsoft.AspNetCore.Mvc;
using School.Application.DTOs.Identity;
using School.Application.Services.Interfaces.Authentication;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthenticationService authenticationService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Create(CreateUserDto user)
        {
            var result = await authenticationService.CreateUser(user);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUser user)
        {
            var result = await authenticationService.LoginUser(user);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> ReviveToken(string refreshToken)
        {
            var result = await authenticationService.ReviveToken(refreshToken);
            return result.Success ? Ok(result) : BadRequest(result);
        }



    }
}
