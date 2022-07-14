using Application.Interfaces.Repos.Auth;
using Application.Interfaces.Repos.BusinessOwner;
using Application.Interfaces.Repos.General;
using Application.Interfaces.Repositories.General;
using Application.Interfaces.Services.BusinessOwner;
using Application.Interfaces.Services.General;
using AutoMapper;
using Domain.Dto.Admin;
using Domain.Dto.Business;
using Domain.Dto.General;
using Domain.Entites;
using Domain.Entites.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Presistence.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BusinussOwner
{
    public class BusinessService : ServiceBase, IBusinessService
    {
        private readonly IBusinessRepository _businussRepository;
        private readonly IServicesRepository _servicesRepository;
        private readonly ICityAreaRepository<City> _cityRepository;
        private readonly ICityAreaRepository<Area> _AreaRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly IFileService _fileService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAppUserRepository _appUserRepository;





        public BusinessService(IBusinessRepository businussRepository,
        IServicesRepository servicesRepository,
        ICityAreaRepository<City> cityRepository,
        ICityAreaRepository<Area> AreaRepository,
        IUnitOfWork unitOfWork, IFileService fileService,
        UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
        IMapper mapper,
        IAppUserRepository appUserRepository,IAttachmentRepository attachmentRepository)
        {
            _businussRepository = businussRepository;
            _servicesRepository = servicesRepository;
            _cityRepository = cityRepository;
            _AreaRepository = AreaRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _userManager = userManager;
            _signInManager=signInManager;
            _appUserRepository = appUserRepository;
            _attachmentRepository = attachmentRepository;
        }

        public async Task<ServiceResponse<int>> CreateBusiness(CreateBusinessDto createBusinessDto)
        {
            try
            {
                if (createBusinessDto == null) return new ServiceResponse<int> { Success = false, Message = "Data Is null", Data = 0 };
                var map = _mapper.Map<Business>(createBusinessDto);
                map.IsActive = false;
               
                _businussRepository.Create(map);

                await _fileService.UploadFile(map.Id, null, new List<IFormFile> { createBusinessDto.BusinessPic }, "BusinessPic", "000", "BussnuisPic", 500000);
                await _fileService.UploadFile(map.Id, null, new List<IFormFile> { createBusinessDto.LicencePic }, "BusinessLinc", "000", "BussnuisLincPic", 500000);
                await _unitOfWork.CommitAsync();
                await _businussRepository.AddServicesToBusinessAsync(map.Id, createBusinessDto.ServiceId);
                


                var res = await _unitOfWork.CommitAsync();
                return new ServiceResponse<int>
                {
                    Success = true,
                    Message = "Added Succesfully Wait for Approve",
                    Data = res
                };

            }
            catch (Exception ex)
            {

                return await LogError(ex, 0);

            }




        }

        public async Task<ServiceResponse<int>> UpdateBusiness(Guid Id, CreateBusinessDto createBusinessDto)
        {
            try
            {
                if (createBusinessDto == null) return new ServiceResponse<int> { Success = false, Message = "Data Is null", Data = 0 };
                var map = _mapper.Map<Business>(createBusinessDto);
                if (createBusinessDto.Id != null)
                {
                    var bus = _businussRepository.GetById(Id);
                    bus.BusinessName = createBusinessDto.BusinessName;
                    bus.BusinessDesc = createBusinessDto.BusinessDesc;
                    bus.BusinussPhone = createBusinessDto.BusinussPhone;
                    bus.CityId = createBusinessDto.CityId;
                    bus.AreaId = createBusinessDto.AreaId;
                    var attach = _attachmentRepository.GetById(map.Id);
                    if (attach.File_Name != createBusinessDto.BusinessPic.FileName)
                    {
                        _attachmentRepository.PhysiscalDelete(map.Id);
                    }
                    await _fileService.UploadFile(map.Id, null, new List<IFormFile> { createBusinessDto.BusinessPic }, nameof(Business), "000", "BussnuisPic", 500000);

                }
                var res = await _unitOfWork.CommitAsync();
                return new ServiceResponse<int>
                {
                    Success = true,
                    Message = "Saved Succesfully",
                    Data = res
                };


            }
            catch (Exception ex)
            {

                return await LogError(ex, 0);
            }
        }

        public async Task<ServiceResponse<int>> DeleteBusiness(Guid id)
        {
            try
            {
                _businussRepository.Delete(id);
                var att = _attachmentRepository.GetAll(a => a.Row_Id == id.ToString()).Select(a=>a.Row_Id);
                foreach (var item in att)
                {
                    _attachmentRepository.PhysiscalDelete(Guid.Parse(item));
                }
                var res = await _unitOfWork.CommitAsync();
                return new ServiceResponse<int>
                {
                    Data = res,
                    Success = true,
                    Message = "DeletedSuccessfully",
                };
            }
            catch (Exception ex)
            {
                return await LogError(ex, 0);
            }
        }

        public async Task<ServiceResponse<List<GetBusinessDto>>> GetBusinuss(Guid userId)
        {
          var business=  _businussRepository.GetAll(a => a.ApplicationUserId == userId &&a.IsActive==true);
            var map = await GetBusinessDtoList(business);
            return new ServiceResponse<List<GetBusinessDto>>
            {
                Data = map,
                Success = true
            };
        }

        public async Task<List<GetBusinessDto>> GetBusinessDtoList(List<Business> businessObj)
        {

            List<GetBusinessDto> getBusinessDtos = new List<GetBusinessDto>();
            var map = _mapper.Map<List<GetBusinessDto>>(businessObj);
            for (int i = 0; i < map.Count; i++)
            {
                var x = _attachmentRepository.GetAttach(map[i].Id, "BusinessPic");
                if(x is not null)
                {
                    map[i].BusinessPic = x.File_Path;

                }
                map[i].MangerName = _appUserRepository.GetUserFullName(businessObj[i].ApplicationUserId);
                map[i].CityName = _cityRepository.GetById(businessObj[i].CityId).Name;
                map[i].AreaName = _AreaRepository.GetById(businessObj[i].AreaId).Name;
                map[i].Services = await _businussRepository.GetServicesNameAsync(map[i].Id);
            }
            return map;
        }

        public async Task<GetAdminBusinessDetailsDto> GetBusinessDetailesDtoList(Business businessObj)
        {
            var map = _mapper.Map<GetAdminBusinessDetailsDto>(businessObj);

                //var x = (await _attachmentRepository.GetAllAsync(p => p.Row_Id == map.Id.ToString() && p.Table_Name=="BusinessPic")).FirstOrDefault().File_Path;
                //map.BusinessPic = x;
                //var y = (await _attachmentRepository.GetAllAsync(p => p.Row_Id == map.Id.ToString()&& p.Table_Name== "BusinessLinc")).FirstOrDefault().File_Path;
                //map.LicencePic = y;
                map.MangerName = _appUserRepository.GetUserFullName(businessObj.ApplicationUserId);
                map.CityName = _cityRepository.GetById(businessObj.CityId).Name;
                map.AreaName = _AreaRepository.GetById(businessObj.AreaId).Name;
                map.Services = await _businussRepository.GetServicesNameAsync(map.Id);
            return map;
        }
    }
}
