using Domain.Dto.General.Auth;
using Domain.Entites.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.Auth
{
    internal class Authprofile:MappingProfileBase
    {
        public Authprofile()
        {
            CreateMap<ApplicationUser, RegisterDto>()
                //.ForMember(c => c.Role, c => c.Ignore())
              .ForMember(c => c.ConfirmPassword, c => c.Ignore())
              .ForMember(c=>c.UserPic,C=>C.Ignore())
              .ReverseMap();
            CreateMap<TokenEntity, TokenDto>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserDto>()
                .ForMember(c => c.ProfilePicture, c => c.Ignore())
                .ReverseMap();
        }
    }
}
