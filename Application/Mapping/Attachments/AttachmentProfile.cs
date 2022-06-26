using Domain.Dto.General;
using Domain.Entites.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.Attachments
{
    public class AttachmentProfile:MappingProfileBase
    {
        public AttachmentProfile()
        {
            CreateMap<Attachment,AttachmentDto>().ReverseMap();
        }
    }
}
