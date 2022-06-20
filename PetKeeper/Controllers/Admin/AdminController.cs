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
        private readonly IAcceptOrRefuseBusiness _acceptOrRefuseBusiness;
        public AdminController(IAdminGetUsers adminGetUsers,IAcceptOrRefuseBusiness acceptOrRefuseBusiness)
        {
            _adminGetUsers = adminGetUsers;
            _acceptOrRefuseBusiness = acceptOrRefuseBusiness;
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
        [HttpGet]
        public async Task<IActionResult> GetAllUnApprovedBusiness()
        {
            var res = await _acceptOrRefuseBusiness.GetAllBusinuss(false);
            return Ok(res);   
        }
        [HttpGet]
        public async Task<IActionResult> GetAllApprovedBusiness()
        {
            var res = await _acceptOrRefuseBusiness.GetAllBusinuss(true);
            return Ok(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetBusinessDetails(Guid id)
        {
            var res = await _acceptOrRefuseBusiness.GetBussinesDetails(id);
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> ApproveBusiness(Guid Busid)
        {
            var res = await _acceptOrRefuseBusiness.ApproveBusiness(Busid);
            return Ok(res);
        }


    }
}
