using Application.Interfaces.Repos.General;
using Application.Interfaces.Services.General;
using Domain.Dto.General;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.General
{
    public class HomeService :ServiceBase, Interfaces.Services.General.IHomeService
    {

        private readonly ICityAreaRepository<City> _cityRepository;
        private readonly ICityAreaRepository<Area> _AreaRepository;
        private readonly IServicesRepository _servicesRepository;

        public HomeService(ICityAreaRepository<City> cityRepository, ICityAreaRepository<Area> areaRepository, IServicesRepository servicesRepository)
        {
            _cityRepository = cityRepository;
            _AreaRepository = areaRepository;
            _servicesRepository = servicesRepository;   
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

    }
}
