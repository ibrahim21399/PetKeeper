using Domain.Dto.Business;
using Domain.Dto.Client;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.Client
{
    public class BookingProfile:MappingProfileBase
    {
        public BookingProfile()
        {
            CreateMap<Booking, GetBookingDto>()
                .ForMember(a => a.BusinessName, a => a.Ignore())
                .ForMember(a => a.AppoientmentState, a => a.Ignore())
                .ReverseMap();
            CreateMap<Booking, GetBOwnerBookingDto>()
                 .ForMember(a => a.BusinessName, a => a.Ignore())
                 .ReverseMap();



        }
    }
}
