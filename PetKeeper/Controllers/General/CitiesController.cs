using Application.Interfaces.Repos.General;
using Application.Interfaces.Services.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetKeeper.Controllers.General
{
   
    [ApiController]
    public class CitiesController : ApiBaseController
    {
        private readonly ICitiesAreasService _citiesAreasService;
        public CitiesController(ICitiesAreasService citiesAreasService)
        {
            _citiesAreasService = citiesAreasService;   
        }
        [HttpGet]
        [Route("/Cities/GetAllCities")]

        public async Task<IActionResult> GetAllCities()
        {
            var response = await _citiesAreasService.GetCitiesAsync();
            return Ok(response);
        }
        [HttpGet]
        [Route("/Cities/GetAllAreas")]
        public async Task<IActionResult> GetAreasOfCity(int CityId)
        {
            var response = await _citiesAreasService.GetAreasAsync(CityId);
            return Ok(response);
        }





    }
}
