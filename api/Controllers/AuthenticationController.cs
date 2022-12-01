using Application.Helpers;
using Application.Services;
using Domain.Dtos;
using Domain.Enums;
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
        private readonly IMedicService _medicService;
        private readonly IPatientService _patientService;

        public AuthenticationController(IAuthService authService, IAuthHelper authHelper, IMedicService medicService, IPatientService patientService)
        {
            _authService = authService;
            _authHelper = authHelper;
            _medicService = medicService;
            _patientService = patientService;   
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUserDto userDto)
        {
            var user = _authService.ValidateUser(userDto);
            if (user == null)
            {
                return Unauthorized();
            }

            var jwt = _authHelper.GetAuthToken(user);
            return Ok(jwt);
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(RegisterUserDto userDto)
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
