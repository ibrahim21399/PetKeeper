﻿using Application.Interfaces.Services.BusinessOwner;
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
    [Authorize (Roles =RolesName.BusinessOwner)]
    public class BusinessController : ApiBaseController
    {
        private readonly ICreateBusinessService _createBusinessService;
        public BusinessController(ICreateBusinessService createBusinessService)
        {
            _createBusinessService = createBusinessService;
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
