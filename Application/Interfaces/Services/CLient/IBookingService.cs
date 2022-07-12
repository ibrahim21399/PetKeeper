using Domain.Dto.Business;
using Domain.Dto.Client;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.CLient
{
    public interface IBookingService
    {
        Task<ServiceResponse<int>> BooKBusiness(Guid SceduleId, Guid BusinessId, Guid UserId, DateTime BookDate);
        Task<ServiceResponse<List<GetBookingDto>>> GetAllClientBooking(Guid userId);
        Task<ServiceResponse<List<GetBOwnerBookingDto>>> GetAllUnApproveBooking(Guid UserId);
        Task<ServiceResponse<List<GetBookingDto>>> GetAllOwnerBooking(Guid UserId);
        Task<ServiceResponse<int>> AcceptBooking(Guid BookId);
        Task<ServiceResponse<int>> CancelBooking(Guid BookId);
        Task<ServiceResponse<List<Schedule>>> GetScheduleOfBusiness(Guid BusId);


    }
}
