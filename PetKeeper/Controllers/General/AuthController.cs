using Application.Interfaces.Services.Auth;
using Domain.Dto.General.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PetKeeper.Controllers.General
{
    [ApiController]

    public class AuthController :ApiBaseController
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("/Auth/Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await _userService.Token(loginDto);
            return Ok(token);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("/Auth/Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto RegisterDto)
        {
            var token = await _userService.RegisterAccounUser(RegisterDto);
            return Ok(token);
        }
    }
}
