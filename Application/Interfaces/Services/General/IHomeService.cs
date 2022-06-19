using Domain.Dto.Business;
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
        Task<ServiceResponse<List<GetBusinessDto>>> FilterBusiness(Guid? ServiceId = null, int? CityId = null, int? AreaId = null);

    }
}
