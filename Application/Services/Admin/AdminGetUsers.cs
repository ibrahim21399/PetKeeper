using Application.Interfaces.Repos.Auth;
using Application.Interfaces.Repos.BusinessOwner;
using Application.Interfaces.Services.Admin;
using Application.Interfaces.Services.BusinessOwner;
using AutoMapper;
using Domain.Dto.Admin;
using Presistence.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Admin
{
    public class AdminGetUsers : ServiceBase, IAdminGetUsers
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBusinessService _businessService;
        private readonly IBusinessRepository _businessRepository;
       public AdminGetUsers(IAppUserRepository appUserRepository,IMapper mapper,IUnitOfWork unitOfWork,IBusinessRepository businessRepository,IBusinessService businessService)
        {
            _appUserRepository = appUserRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _businessRepository = businessRepository;
            _businessService = businessService;
        }
        public async Task<ServiceResponse<int>> DeleteUser(Guid userid)
        {
            try
            {
                var busId = await _businessRepository.GetBusIdForUser(userid);
                foreach (var item in busId)
                {
                   await _businessService.DeleteBusiness(item);
                }
                await _unitOfWork.CommitAsync();

                var user = _appUserRepository.GetUserById(userid);
                await _appUserRepository.RemoveUser(user);
                await _unitOfWork.CommitAsync();
                return new ServiceResponse<int>
                {
                    Success = true,
                    Message = "Deleted Succesfully",
                    Data = 1
                };
            }
            catch (Exception ex)
            {
                return await LogError(ex ,0);

            }


        }

        public async Task<List<GetUserDto>> GetUsersbyStatus(bool status)
        {
                List<GetUserDto> getUserDtos = new List<GetUserDto>();
                var users = await _appUserRepository.GetAllAsync(status);
                var map = _mapper.Map<List<GetUserDto>>(users);
                return map;

        }
    }
}
