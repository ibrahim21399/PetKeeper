using Application.Interfaces.Repos.General;
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
        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;   
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





    }
}
