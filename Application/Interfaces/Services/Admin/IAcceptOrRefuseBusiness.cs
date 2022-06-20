using Domain.Dto.Admin;
using Domain.Dto.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Admin
{
    public  interface IAcceptOrRefuseBusiness
    {
        Task<ServiceResponse<List<GetBusinessDto>>> GetAllBusinuss(bool IsActive);
        Task<ServiceResponse<GetAdminBusinessDetailsDto>> GetBussinesDetails(Guid BusId);
        Task<ServiceResponse<int>> ApproveBusiness(Guid BusId);
        Task<ServiceResponse<int>> UnApproveBusiness(Guid BusId);



    }
}
