using Domain.Dto.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.CLient
{
    public interface ICommentService
    {
        Task<ServiceResponse<int>> AddComment(Guid BusId,CreateCommentDto createCommentDto);
        Task<ServiceResponse<int>> DeleteComment(Guid CommentId);
    }
}
