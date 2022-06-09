using Application.Interfaces.Services.BusinessOwner;
using Domain.Dto.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PetKeeper.Controllers.BusinessOwner
{
    [ApiController]
    [Route("[Controller]/[Action]")]

    public class BusinessController : ApiBaseController
    {
        private readonly ICreateBusinessService _createBusinessService;
        public BusinessController(ICreateBusinessService createBusinessService)
        {
            _createBusinessService = createBusinessService;
        }

        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
           var res = await _createBusinessService.GetServices();
            return Ok(res); 
        }

        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var res = await _createBusinessService.GetCities();
            return Ok(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetAreas(int cityId)
        {
            var res = await _createBusinessService.GetAreas(cityId);
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBusiness(CreateBusinessDto createBusinessDto)
        {
            createBusinessDto.ApplicationUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var res = await _createBusinessService.CreateBusiness(createBusinessDto);
            return Ok(res); 

        }

        [HttpGet]
        public async Task<IActionResult> GetBusiness()
        {
            var res = await _createBusinessService.GetBusinuss(Guid.Parse("1b175170d2564b29926608da47602b1a"));
            return Ok(res);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBusiness(Guid Id)
        {
            var res = await _createBusinessService.DeleteBusiness(Id);
            return Ok(res);
        }


    }
}
