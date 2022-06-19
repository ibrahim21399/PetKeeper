using Application.Interfaces.Services.BusinessOwner;
using Application.Interfaces.Services.CLient;
using Domain.Common;
using Domain.Dto.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PetKeeper.Controllers.BusinessOwner
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    //[Authorize (Roles =RolesName.BusinessOwner)]
    public class BusinessController : ApiBaseController
    {
        private readonly IBusinessService _createBusinessService;
        private readonly IBookingService _bookingService;
        public BusinessController(IBusinessService createBusinessService,IBookingService bookingService)
        {
            _createBusinessService = createBusinessService;
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBusiness(CreateBusinessDto createBusinessDto)
        {
            createBusinessDto.ApplicationUserId =Guid.Parse(CurrentUserId);
            var res = await _createBusinessService.CreateBusiness(createBusinessDto);
            return Ok(res); 

        }

        [HttpGet]
        public async Task<IActionResult> GetBusiness()
        {
            var res = await _createBusinessService.GetBusinuss(Guid.Parse(CurrentUserId));
            return Ok(res);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBusiness(Guid Id)
        {
            var res = await _createBusinessService.DeleteBusiness(Id);
            return Ok(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAppoientments()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AcceptBooking(Guid BookId)
        {
            var res = await _bookingService.AcceptBooking(BookId);
            return Ok(res);

        }

        [HttpPost]
        public async Task<IActionResult> CancelBooking(Guid BookId)
        {
            var res = await _bookingService.CancelBooking(BookId);
            return Ok(res);

        }


    }
}
