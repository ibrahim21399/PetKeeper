using Domain.Dto.Client;
using System;
using Domain.Entites;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.Comment
{
    public class CommentProfilecs : MappingProfileBase

    {
        public CommentProfilecs()
        {
            CreateMap<Comments,CreateCommentDto>().ReverseMap();
        
        
        }
    }
}
