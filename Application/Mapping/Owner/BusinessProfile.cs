using Domain.Dto.Business;
using Domain.Entites;


namespace Application.Mapping.Owner
{
    public class BusinessProfile:MappingProfileBase
    {
        public BusinessProfile()
        {
            CreateMap<Business,CreateBusinessDto>()
                .ForMember(a => a.ServiceId, a => a.Ignore())
                .ForMember(a=>a.BusinessPic,a=>a.Ignore())
                .ForMember(a=>a.LicencePic,a=>a.Ignore())
                .ReverseMap();
            CreateMap<Business, GetBusinessDto>()
                .ForMember(a => a.CityName, a => a.Ignore())
                .ForMember(a => a.AreaName, a => a.Ignore())
                .ForMember(a => a.BusinessPic, a => a.Ignore())
                .ForMember(a => a.Services, a => a.Ignore())
                .ReverseMap();


        }
    }
}
