using Application.Interfaces.Repos.Auth;
using Application.Interfaces.Repos.BusinessOwner;
using Application.Interfaces.Repos.General;
using Application.Interfaces.Repositories.General;
using Application.Interfaces.Services.BusinessOwner;
using Application.Interfaces.Services.General;
using AutoMapper;
using Domain.Dto.Business;
using Domain.Dto.General;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.General
{
    public class HomeService :ServiceBase,IHomeService
    {

        private readonly ICityAreaRepository<City> _cityRepository;
        private readonly ICityAreaRepository<Area> _AreaRepository;
        private readonly IServicesRepository _servicesRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly IBusinessService _businessService;


        public HomeService(ICityAreaRepository<City> cityRepository,
            ICityAreaRepository<Area> areaRepository,
            IServicesRepository servicesRepository,
            IBusinessRepository businessRepository,
            IBusinessService businessService)
        {
            _cityRepository = cityRepository;
            _AreaRepository = areaRepository;
            _servicesRepository = servicesRepository; 
            _businessRepository = businessRepository;
            _businessService = businessService;

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
                    Message = "created"
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
                var areas = await _AreaRepository.GetAllAsync(a => a.CityId == cityId);
                List<DropDownId> dropDowns = new List<DropDownId>();
                if (areas != null)
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

                var Services = _servicesRepository.GetAll();
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

        public async Task<ServiceResponse<List<GetBusinessDto>>> FilterBusiness(Guid? ServiceId=null,int? CityId= null,int? AreaId = null)
        {
            try
            {
                List<GetBusinessDto> res=null;
                if (CityId==null&&AreaId==null&&ServiceId==null)
                {
                    var businesses = _businessRepository.GetAll(a => a.IsActive == true);
                    res = await _businessService.GetBusinessDtoList(businesses);
                }
                else if (CityId == null && AreaId == null)
                {
                    var BusList = new List<Business>();
                    var BusinessIdOfService = _businessRepository.GetBusIdOfService(ServiceId);
                    foreach (var BusId in BusinessIdOfService)
                    {
                        var Business = _businessRepository.GetById(BusId);
                        BusList.Add(Business);
                    }
                    res = await _businessService.GetBusinessDtoList(BusList);
                }
                else if (CityId == null && ServiceId == null)
                {
                    var business = _businessRepository.GetAll(a => a.AreaId == AreaId && a.IsActive == true);
                    res = await _businessService.GetBusinessDtoList(business);
                }
                else if (AreaId == null && ServiceId == null)
                {
                    var business = _businessRepository.GetAll(a => a.CityId == CityId && a.IsActive == true);
                    res = await _businessService.GetBusinessDtoList(business);
                }
                else if (ServiceId == null)
                {
                    var business = _businessRepository.GetAll(a => a.CityId == CityId && a.AreaId==AreaId && a.IsActive==true);
                    res = await _businessService.GetBusinessDtoList(business);
                }
                else if (AreaId == null)
                {
                    var BusList = new List<Business>();
                    var BusinessIdOfService = _businessRepository.GetBusIdOfService(ServiceId);
                    foreach (var BusId in BusinessIdOfService)
                    {
                        var Business = _businessRepository.GetById(BusId);
                        BusList.Add(Business);
                    }
                    
                    var businesses = _businessRepository.GetAll( a => a.CityId == CityId && a.IsActive == true).Union(BusList).ToList();
                    res = await _businessService.GetBusinessDtoList(businesses);

                }
                else if (CityId == null)
                {
                    var BusList = new List<Business>();
                    var BusinessIdOfService = _businessRepository.GetBusIdOfService(ServiceId);
                    foreach (var BusId in BusinessIdOfService)
                    {
                        var Business = _businessRepository.GetById(BusId);
                        BusList.Add(Business);
                    }

                    var businesses = _businessRepository.GetAll(a => a.AreaId == AreaId && a.IsActive == true).Union(BusList).ToList();
                    res = await _businessService.GetBusinessDtoList(businesses);
                }
                else if (CityId !=null && AreaId !=null && ServiceId !=null)
                {
                    var BusList = new List<Business>();
                    var BusinessIdOfService = _businessRepository.GetBusIdOfService(ServiceId);
                    foreach (var BusId in BusinessIdOfService)
                    {
                        var Business = _businessRepository.GetById(BusId);
                        BusList.Add(Business);
                    }

                    var businesses = _businessRepository.GetAll(a => a.CityId == CityId && a.AreaId == AreaId && a.IsActive == true).Union(BusList).ToList();
                    res = await _businessService.GetBusinessDtoList(businesses);
                    if (res == null)
                    {
                        var business = _businessRepository.GetAll(a => a.CityId == CityId && a.AreaId == AreaId && a.IsActive == true).Union(BusList).ToList();
                        res = await _businessService.GetBusinessDtoList(business);
                        if (res == null)
                        {
                            var BusList2 = new List<Business>();
                            var BusinessIdOfService2 = _businessRepository.GetBusIdOfService(ServiceId);
                            foreach (var BusId in BusinessIdOfService2)
                            {
                                var Business = _businessRepository.GetById(BusId);
                                BusList2.Add(Business);
                            }
                            res = await _businessService.GetBusinessDtoList(BusList2);
                        }
                    }
                }
                if (res.Count()==0)
                {
                    return new ServiceResponse<List<GetBusinessDto>>
                    {
                        Success = false,
                        Data = res,
                        Message = "No Matched Business Found"
                    };

                }
                else
                {

                    return new ServiceResponse<List<GetBusinessDto>>
                    {
                        Success = true,
                        Data = res,
                        Message = "Your Matched Bussiness"
                    };
                }


            }
            catch (Exception ex)
            {

                return await LogError<List<GetBusinessDto>>(ex, null);
            }

        }
    }
}
