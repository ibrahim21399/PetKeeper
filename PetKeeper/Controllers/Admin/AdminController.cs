using Application.Interfaces.Services.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetKeeper.Controllers.Admin
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminGetUsers _adminGetUsers;
        public AdminController(IAdminGetUsers adminGetUsers)
        {
            _adminGetUsers = adminGetUsers;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBussinessOwner()
        {
            var res = await _adminGetUsers.GetUsersbyStatus(false);
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var res = await _adminGetUsers.GetUsersbyStatus(true);
            return Ok(res);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var res = await _adminGetUsers.DeleteUser(id);
            return Ok(res);
        }
    }
}
