using Application.Interfaces.Services.Auth;
using Domain.Dto.General.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PetKeeper.Controllers.General
{
    [ApiController]

    public class AuthController : ApiBaseController
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
            var token = await _userService.RegisterAccounUser(RegisterDto, true);
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
        [HttpGet]
        [Route("/Account")]
        public async Task<IActionResult> GetAccount()
        {
            var res = await _userService.GetUserAccount(Guid.Parse(CurrentUserId));
            return Ok(res);
        }

        [HttpPut]
        [Route("/Account/Edit")]
        public async Task<IActionResult> Update([FromForm] UserDto userDto)
        {
            var res = await _userService.UpdateUser(Guid.Parse(CurrentUserId),userDto);
            return Ok(res);
        }

        [HttpPost]
        [Route("/Account/ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromForm]string current ,[FromForm]string NewPass)
        {
            var res = await _userService.ChangePassword(Guid.Parse(CurrentUserId),current,NewPass);
            return Ok(res);
        }
        [HttpDelete]
        [Route("/Account/DeleteMyAccount")]
        public async Task<IActionResult> DeleteMyAcc()
        {
            var res = _userService.DeletAccountUser(Guid.Parse(CurrentUserId));
            return Ok(res);
        }
    }
}
