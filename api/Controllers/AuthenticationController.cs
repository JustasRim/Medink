using Application.Helpers;
using Application.Services;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IAuthHelper _authHelper;

        public AuthenticationController(IAuthService authService, IAuthHelper authHelper )
        {
            _authService = authService;
            _authHelper = authHelper;
        }

        [HttpPost("login")]
        public IActionResult Login(AuthenticateUserDto userDto)
        {
            var user = _authService.ValidateUser(userDto);
            if (user == null)
            {
                return BadRequest();
            }

            var jwt = _authHelper.GetAuthToken(user);
            return Ok(jwt);
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(AuthenticateUserDto userDto)
        {
            var user = await _authService.SignUp(userDto);
            if (user == null)
            {
                return BadRequest();
            }

            var jwt = _authHelper.GetAuthToken(user);
            return Ok(jwt);
        }
    }
}
