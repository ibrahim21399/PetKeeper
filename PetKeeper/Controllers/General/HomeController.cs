using Application.Interfaces.Repos.General;
using Application.Interfaces.Services.BusinessOwner;
using Application.Interfaces.Services.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetKeeper.Controllers.General
{
   
    [ApiController]
    [AllowAnonymous]
    [Route("[Controller]/[Action]")]
    public class HomeController : ApiBaseController
    {
        private readonly IHomeService _homeService;
        private readonly IBusinessService _businessService;
        public HomeController(IHomeService homeService ,IBusinessService businessService)
        {
            _homeService = homeService;   
            _businessService = businessService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var response = await _homeService.GetCities();
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAreasOfCity(int CityId)
        {
            var response = await _homeService.GetAreas(CityId);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var response = await _homeService.GetServices();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> FilterBusiness(Guid? ServiceId, int? CityId ,int? AreaId)
        {

            var response = await _homeService.FilterBusiness(ServiceId,CityId,AreaId);
            return Ok(response);
        }







    }
}
