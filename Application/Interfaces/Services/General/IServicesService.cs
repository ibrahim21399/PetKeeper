using Domain.Dto.General;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.General
{
    public interface IServicesService
    {
        Task<ServiceResponse<List<Service>>> GetBusinussServices();
    }
}
