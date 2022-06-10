using Domain.Dto.Business;
using Domain.Dto.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.BusinessOwner
{
    public interface ICreateBusinessService
    {

        Task<ServiceResponse<List<GetBusinessDto>>> GetBusinuss(Guid userId);

        Task<ServiceResponse<int>> CreateBusiness(CreateBusinessDto createBusinessDto);
        Task<ServiceResponse<int>> UpdateBusiness(Guid Id,CreateBusinessDto createBusinessDto);
        Task<ServiceResponse<int>> DeleteBusiness(Guid id);

    }
}
