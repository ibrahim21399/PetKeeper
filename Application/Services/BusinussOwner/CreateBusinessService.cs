using Application.Interfaces.Repos.BusinessOwner;
using Application.Interfaces.Repos.General;
using Application.Interfaces.Repositories.General;
using Application.Interfaces.Services.BusinessOwner;
using Application.Interfaces.Services.General;
using AutoMapper;
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
    public class CreateBusinessService : ServiceBase, ICreateBusinessService
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




        public CreateBusinessService(IBusinessRepository businussRepository,
        IServicesRepository servicesRepository,
        ICityAreaRepository<City> cityRepository,
        ICityAreaRepository<Area> AreaRepository,
        IUnitOfWork unitOfWork, IFileService fileService,
        UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
        IMapper mapper)
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
        }

        public async Task<ServiceResponse<List<DropDownId>>> GetCities()
        {
            try
            {

                var cities = await _cityRepository.GetAllAsync();
                List<DropDownId> dropDowns = new();
                cities.ForEach(d =>
                {
                    DropDownId dropDown = new()
                    {
                        Id = d.Id,
                        Name = d.Name
                    };
                    dropDowns.Add(dropDown);
                });
                return new ServiceResponse<List<DropDownId>>
                {
                    Success = true,
                    Data = dropDowns,
                    Message ="created"
                };
            }
            catch (Exception ex)
            {

                return await LogError<List<DropDownId>>(ex, null);
            }
        }
        public async Task<ServiceResponse<List<DropDownId>>> GetAreas(int cityId)
        {
            try
            {
                var areas = await _AreaRepository.GetAllAsync(a=>a.CityId==cityId);
                List<DropDownId> dropDowns = new List<DropDownId>();
                if (areas !=null)
                {
                    areas.ForEach(d =>
                    {
                        DropDownId dropDown = new()
                        {
                            Id = d.Id,
                            Name = d.Name
                        };
                        dropDowns.Add(dropDown);
                    });
                    return new ServiceResponse<List<DropDownId>>
                    {
                        Success = true,
                        Data = dropDowns,
                        Message = "created"
                    };

                }
                return new ServiceResponse<List<DropDownId>>
                {
                    Success = true,
                    Data = dropDowns,
                    Message = "created"
                };


            }
            catch (Exception ex)
            {

                return await LogError<List<DropDownId>>(ex, null);
            }
        }

        public async Task<ServiceResponse<List<DropDownGuid>>> GetServices()
        {
            try
            {

                var Services =  _servicesRepository.GetAll();
                List<DropDownGuid> dropDowns = new();
                Services.ForEach(d =>
                {
                    DropDownGuid dropDown = new()
                    {
                        Id = d.Id,
                        Name = d.ServiceTitle
                    };
                    dropDowns.Add(dropDown);
                });
                return new ServiceResponse<List<DropDownGuid>>
                {
                    Success = true,
                    Data = dropDowns,
                    Message = "created"
                };
            }
            catch (Exception ex)
            {

                return await LogError<List<DropDownGuid>>(ex, null);
            }
        }

        public async Task<ServiceResponse<int>> CreateBusiness(CreateBusinessDto createBusinessDto)
        {
            try
            {
                if (createBusinessDto == null) return new ServiceResponse<int> { Success = false, Message = "Data Is null", Data = 0 };
                var map = _mapper.Map<Business>(createBusinessDto);

                //var userid = _userManager.GetUserId(_signInManager.Context.User);
                map.ApplicationUserId = Guid.Parse("1B175170-D256-4B29-9266-08DA47602B1A");
                map.IsActive = false;
                _businussRepository.Create(map);
                await _fileService.UploadFile(map.Id, null, new List<IFormFile> { createBusinessDto.BusinessPic }, nameof(map), "000", "BussnuisPic", 500000);
                await _fileService.UploadFile(map.Id, null, new List<IFormFile> { createBusinessDto.LicencePic }, nameof(map), "000", "BussnuisLincPic", 500000);
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
          var business=  _businussRepository.GetAll(a => a.ApplicationUserId == userId);
            List<GetBusinessDto> getBusinessDtos = new List<GetBusinessDto>();
            var map = _mapper.Map<List<GetBusinessDto>>(business);

            for (int i = 0; i < map.Count; i++)
            {
                //var x = (await _attachmentRepository.GetAllAsync(p => p.Row_Id == map[i].Id.ToString())).FirstOrDefault().File_Path;
                //map[i].BusinessPic = x;
                map[i].CityName = _cityRepository.GetById(business[i].CityId).Name;
                map[i].AreaName = _AreaRepository.GetById(business[i].AreaId).Name;
                map[i].Services = await _businussRepository.GetServicesNameAsync(map[i].Id);

            }
            return new ServiceResponse<List<GetBusinessDto>>
            {
                Data = map,
                Success = true
            };
        }
    }
}
