using Domain.Dto.General;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.General
{
    public interface IHomeService
    {
        Task<ServiceResponse<List<DropDownId>>> GetCities();
        Task<ServiceResponse<List<DropDownId>>> GetAreas(int cityId);
        Task<ServiceResponse<List<DropDownGuid>>> GetServices();

    }
}
