using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.General
{
    public interface ICitiesAreasService
    {
        Task<City> GetCityByID(int id);
        Task<Area> GetAreaByID(int id);
        Task<List<City>> GetCitiesAsync();
        Task<List<Area>> GetAreasAsync(int CityId);
    }
}
