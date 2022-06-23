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
    public class CommentService:ServiceBase,ICommentService
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService (ICommentsRepository commentsRepository, IUnitOfWork unitOfWork,IMapper mapper)
        {
            _commentsRepository = commentsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<int>> AddComment(Guid BusId,CreateCommentDto createCommentDto)
        {
            try
            {
                if (createCommentDto == null) return new ServiceResponse<int> { Success = false, Message = "Please Fill comment and Rate", Data = 0 };
                
                var map = _mapper.Map<Comments>(createCommentDto);
                map.BusinessId = BusId;
                _commentsRepository.Create(map);
                await _unitOfWork.CommitAsync();
                return new ServiceResponse<int>
                {
                    Success = true,
                    Message = "Comment Added",
                    Data = 1
                };
            }
            catch (Exception ex)
            {

                return await LogError(ex, 0);
            }
        }

        public async Task<ServiceResponse<int>> DeleteComment(Guid CommentId)
        {
            try
            {
                _commentsRepository.Delete(CommentId);
                 await _unitOfWork.CommitAsync();
                return new ServiceResponse<int>
                {
                    Success = true,
                    Data=1,
                    Message="comment Deleted"
                };


            }
            catch (Exception ex)
            {

                return await LogError(ex, 0);
            }
        }
    }
}
