using Application.Interfaces.Services.CLient;
using Domain.Dto.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetKeeper.Controllers.Client
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class ClientController : ApiBaseController
    {
        private readonly IBookingService _clientBookingService;
        private readonly ICommentService _commentService;
        public ClientController(IBookingService clientBookingService, ICommentService commentService)
        {
            _clientBookingService = clientBookingService;
            _commentService = commentService;
        }
        [HttpPost]
        public async Task<IActionResult> BookAppoienment(DateTime BookDate)
        {
            Guid BusId = Guid.Parse("9568E0FAD4F94E0C2D1A08DA4FCC07C9");
            Guid SceduleId = Guid.Parse("BF9B04FBE5984DCD07EA08DA4FCC07DA");
            var res = await _clientBookingService.BooKBusiness(SceduleId,BusId,Guid.Parse(CurrentUserId), BookDate.Date);
            return Ok(res);
        }

        [HttpGet]
        public async Task <IActionResult> GetAppoientments()
        {

            var res = await _clientBookingService.GetAllClientBooking(Guid.Parse(CurrentUserId)); 
            return Ok(res);

        }

        [HttpPost]
        public async Task<IActionResult> CommentOnBusiness(Guid BusId,CreateCommentDto createCommentDto)
        {
            createCommentDto.ApplicationUserId=Guid.Parse(CurrentUserId);
            var res = await _commentService.AddComment(BusId,createCommentDto);
            return Ok(res);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment(Guid CommentId)
        {
            var res = await _commentService.DeleteComment(CommentId);
            return Ok(res);
        }
        [HttpGet]

        public async Task<IActionResult> GetAllSchedule (Guid busId)
        {
            var res = await _clientBookingService.GetScheduleOfBusiness(busId);
            return Ok(res);

        }
    }
}
