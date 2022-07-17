using Application.Interfaces.Repos.BusinessOwner;
using Application.Interfaces.Repos.General;
using Application.Interfaces.Services.CLient;
using AutoMapper;
using Domain.Dto.Business;
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
        private readonly IMapper _mapper;
        private readonly IScheduleRepository _scheduleRepository;
        public BookingService(IBookingRepository bookingRepository,IUnitOfWork unitOfWork,
            IBusinessRepository businessRepository, IMapper mapper,
            IScheduleRepository scheduleRepository)
        {
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
            _businessRepository = businessRepository;
            _mapper = mapper;
            _scheduleRepository = scheduleRepository;
        }

        public async Task<ServiceResponse<int>> BooKBusiness(Guid SceduleId,Guid BusinessId,Guid UserId,DateTime BookDate)
        {
            try
            {
                if (SceduleId == null||BusinessId==null) return new ServiceResponse<int> { Success = false, Message = "Data Is Empty", Data = 0 };
                var x = _scheduleRepository.GetAll(a => a.Id == SceduleId).FirstOrDefault();

                if (BookDate.Day.ToString()==x.DayOfWeek)
                {
                    Booking booking = new Booking();
                    booking.status = false;
                    booking.IsCanceled = false;
                    booking.ScheduleId = SceduleId;
                    booking.BusinessId = BusinessId;
                    booking.BookDate = BookDate.Date;
                    booking.ApplicationUserId = UserId;
                    _bookingRepository.Create(booking);
                    var res = await _unitOfWork.CommitAsync();
                    return new ServiceResponse<int>
                    {
                        Success = true,
                        Message = "You Booking Was Submitted",
                        Data = res
                    };

                }
                else
                {
                    return new ServiceResponse<int>
                    {
                        Success = true,
                        Message = "you Entered Date with Day not match Schedule Day",
                        Data = 0
                    };
                }


            }
            catch (Exception ex)
            {

                return await LogError(ex, 0);
            }
        }
        public async Task<ServiceResponse<List<GetBookingDto>>> GetAllClientBooking(Guid userId)
        {

            var UserBookings =  _bookingRepository.GetAll(a => a.ApplicationUserId == userId);

                List<GetBookingDto> res = new List<GetBookingDto>();   
               
                foreach (var book in UserBookings)
                {
                    GetBookingDto getClientBookingDto = new GetBookingDto();
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

                return new ServiceResponse<List<GetBookingDto>>
                {
                    Success = true,
                    Data = res
                };

        }

        public async Task<ServiceResponse<List<GetBOwnerBookingDto>>> GetAllUnApproveBooking(Guid UserId)
        {
            var BusinessId = await _businessRepository.GetBusIdForUser(UserId);
            List < GetBOwnerBookingDto> getBOwnerBookingDtos = new List<GetBOwnerBookingDto>();
            for (int i = 0; i < BusinessId.Count; i++)
            {
                var UnApprovedbookings= await _bookingRepository.GetAllBookingAsync(BusinessId[i], false, false);
                var map = _mapper.Map<List<GetBOwnerBookingDto>>(UnApprovedbookings);
                for (int j = 0; j < UnApprovedbookings.Count; j++)
                {
                    foreach (var item in map)
                    {
                        item.BusinessName = _businessRepository.GetBusinessNameAsync(UnApprovedbookings[j].BusinessId);
                    }
                }

                getBOwnerBookingDtos.AddRange(map);
            }

            return new ServiceResponse<List<GetBOwnerBookingDto>>
            {
                Success = true,
                Data = getBOwnerBookingDtos
            };

        }
        public async Task<ServiceResponse<List<GetBookingDto>>> GetAllOwnerBooking(Guid UserId)
        {
            var BusIds = await _businessRepository.GetBusIdForUser(UserId);

            List<GetBookingDto> getBookingDtos = new List<GetBookingDto>();
            for (int i = 0; i < BusIds.Count; i++)
            {
                var Allbookings = await _bookingRepository.GetAllBookingAsync(BusIds[i], true, false);
                var map = _mapper.Map<List<GetBookingDto>>(Allbookings);
                for (int j = 0; j < Allbookings.Count; j++)
                {
                    foreach (var item in map)
                    {
                        item.BusinessName = _businessRepository.GetBusinessNameAsync(Allbookings[j].BusinessId);
                        if (Allbookings[j].IsCanceled)
                        {
                            item.AppoientmentState = "Cancelled";
                        }
                        else
                        {
                            item.AppoientmentState = "Approved";
                        }

                    }
                }

                getBookingDtos.AddRange(map);
            }

            return new ServiceResponse<List<GetBookingDto>>
            {
                Success = true,
                Data = getBookingDtos
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

        public async Task<ServiceResponse<List<Schedule>>> GetScheduleOfBusiness(Guid BusId)
        {
            try
            {
                var res = await _scheduleRepository.GetAllAsync(a => a.BusinessId == BusId);
                return new ServiceResponse<List<Schedule>>
                {
                    Data = res,
                    Success = true,
                };

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

    }

}
