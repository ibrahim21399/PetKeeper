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
        [Route("/Auth/ClientRegister")]
        public async Task<IActionResult> ClientRegister([FromForm] RegisterDto RegisterDto)
        {
            var token = await _userService.RegisterAccounUser(RegisterDto,true);
            return Ok(token);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("/Auth/OwnerRegister")]
        public async Task<IActionResult> OwnerRegister([FromForm] RegisterDto RegisterDto)
        {
            var token = await _userService.RegisterAccounUser(RegisterDto, false);
            return Ok(token);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("/Auth/LogOut")]
        public async Task<IActionResult> LogOut()
        {
            var res = await _userService.SigOutAsync();
            return Ok(res);
        }
    }
}
