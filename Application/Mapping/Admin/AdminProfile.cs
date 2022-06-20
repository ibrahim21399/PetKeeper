using Domain.Dto.Admin;
using Domain.Entites;
using Domain.Entites.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.Admin
{
    public class AdminProfile:MappingProfileBase
    {
        public AdminProfile()
        {
            CreateMap<ApplicationUser,GetUserDto>()
             .ReverseMap();
            CreateMap<Business, GetAdminBusinessDetailsDto>()
                .ForMember(a => a.Services, a => a.Ignore())
                .ForMember(a => a.CityName, a => a.Ignore())
                .ForMember(a => a.AreaName, a => a.Ignore())
                .ForMember(a => a.BusinessPic, a => a.Ignore())
                .ForMember(a => a.LicencePic, a => a.Ignore())
                .ReverseMap();
        }
    }
}
