using Application.Interfaces.Repos.General;
using Application.Interfaces.Services.General;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.General
{
    public class CitiesAreasService : ICitiesAreasService
    {

        private readonly ICityAreaRepository<City> _cityRepository;
        private readonly ICityAreaRepository<Area> _AreaRepository;

        public CitiesAreasService(ICityAreaRepository<City> cityRepository, ICityAreaRepository<Area> areaRepository)
        {
            _cityRepository = cityRepository;
            _AreaRepository = areaRepository;
        }


        public async Task<List<City>> GetCitiesAsync()
        {
            return await _cityRepository.GetAllAsync();
        }
        public async Task<List<Area>> GetAreasAsync(int CityId)
        {
            return await _AreaRepository.GetAllAsync(a => a.CityId == CityId);
        }

        public async Task<City> GetCityByID(int id)
        {
            return await _cityRepository.GetByIdAsync(id);
        }


        public async Task<Area> GetAreaByID(int id)
        {
            return await _AreaRepository.GetByIdAsync(id);
        }


    }
}
