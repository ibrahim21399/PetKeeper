using AutoMapper;
using System.Collections.Generic;
using System;
using System.Text;

namespace Application
{
    public class MappingProfileBase : Profile
    {
        public MappingProfileBase()
        {
            SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();
            ReplaceMemberName("_", "");
        }
        


    }
}

