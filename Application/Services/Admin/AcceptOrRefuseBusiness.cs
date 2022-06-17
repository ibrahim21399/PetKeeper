using Application.Interfaces.Services.Admin;
using Domain.Dto.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Admin
{
    public class AcceptOrRefuseBusiness : IAcceptOrRefuseBusiness
    {
        public Task<ServiceResponse<List<GetBusinessDto>>> GetAllBusinuss()
        {
            throw new NotImplementedException();
        }
    }
}
