using Application.Interfaces.Repos.BusinessOwner;
using Application.Interfaces.Repos.General;
using Application.Interfaces.Services.CLient;
using AutoMapper;
using Domain.Dto.Client;
using Domain.Entites;
using Presistence.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Client
{
    public class BookingService:ServiceBase,IBookingService 
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IBookingRepository bookingRepository,IUnitOfWork unitOfWork,IBusinessRepository businessRepository)
        {
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
            _businessRepository = businessRepository;
        }

        public async Task<ServiceResponse<int>> BooKBusiness(Guid SceduleId,Guid BusinessId,Guid UserId,DateTime BookDate)
        {
            try
            {
                if (SceduleId == null||BusinessId==null) return new ServiceResponse<int> { Success = false, Message = "Data Is null", Data = 0 };
                Booking booking = new Booking();
                booking.status = false;
                booking.IsCanceled = false;
                booking.ScheduleId = SceduleId;
                booking.BusinessId = BusinessId;
                booking.BookDate = BookDate.Date;
                booking.ApplicationUserId = UserId;
                _bookingRepository.Create(booking);
               var res= await _unitOfWork.CommitAsync();
                return new ServiceResponse<int>
                {
                    Success = true,
                    Message = "You Booking Was Submitted",
                    Data = res
                };

            }
            catch (Exception ex)
            {

                return await LogError(ex, 0);
            }
        }
        public async Task<ServiceResponse<List<GetClientBookingDto>>> GetAllClientBooking(Guid userId)
        {

            var UserBookings =  _bookingRepository.GetAll(a => a.ApplicationUserId == userId);

                List<GetClientBookingDto> res = new List<GetClientBookingDto>();   
               
                foreach (var book in UserBookings)
                {
                    GetClientBookingDto getClientBookingDto = new GetClientBookingDto();
                    getClientBookingDto.BusinessName = _businessRepository.GetBusinessNameAsync(book.BusinessId);
                    getClientBookingDto.BookDate = book.BookDate;
                    if (book.IsCanceled==true)
                    {
                        getClientBookingDto.AppoientmentState = "Cancelled";
                    }
                    else if(book.status==true)
                    {
                        getClientBookingDto.AppoientmentState = "Approved"; 
                    }
                    else if(book.status ==false && book.IsCanceled==true)
                    {
                    getClientBookingDto.AppoientmentState = "Waiting";
                    }
                    else if(book.status==true && book.IsCanceled==false)
                    {
                        getClientBookingDto.AppoientmentState = "Completed";
                    }
                    res.Add(getClientBookingDto);
                }

                return new ServiceResponse<List<GetClientBookingDto>>
                {
                    Success = true,
                    Data = res
                };

        }

        public async Task<ServiceResponse<List<Booking>>> GetAllUnApproveBooking(Guid BusinessId)
        {

            var res = await _bookingRepository.GetAllBookingAsync(BusinessId, false,false);

            return new ServiceResponse<List<Booking>>
            {
                Success = true,
                Data = res
            };


        }

        public async Task<ServiceResponse<int>> AcceptBooking(Guid BookId)
        {
            try
            {
                var booking = _bookingRepository.GetById(BookId);
                if (booking != null)
                {
                    booking.status = true;
                    booking.IsCanceled = false;
                }
                var res = await _unitOfWork.CommitAsync();
                return new ServiceResponse<int>
                {
                    Success = true,
                    Message = "Booking Was Accepted",
                    Data = res
                };
            }
            catch (Exception ex)
            {

                return await LogError(ex, 0);
            }
        }
        public async Task<ServiceResponse<int>> CancelBooking(Guid BookId)
        {
            try
            {
                var booking = _bookingRepository.GetById(BookId);
                if (booking != null)
                {
                    booking.status = false;
                    booking.IsCanceled = true;
                }
                var res = await _unitOfWork.CommitAsync();
                return new ServiceResponse<int>
                {
                    Success = true,
                    Message = "Booking was Cancelled",
                    Data = res
                };
            }
            catch (Exception ex)
            {

                return await LogError(ex, 0);
            }
        }

    }

}
