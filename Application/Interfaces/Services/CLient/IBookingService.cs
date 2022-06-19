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
        Task<ServiceResponse<List<GetClientBookingDto>>> GetAllClientBooking(Guid userId);
        Task<ServiceResponse<List<Booking>>> GetAllUnApproveBooking(Guid BusinessId);
        Task<ServiceResponse<int>> AcceptBooking(Guid BookId);
        Task<ServiceResponse<int>> CancelBooking(Guid BookId);

    }
}
