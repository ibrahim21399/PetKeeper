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
    public class ServicesService : ServiceBase, IServicesService
    {
        private readonly IServicesRepository _servicesRepository;
        public ServicesService(IServicesRepository servicesRepository)
        {
            _servicesRepository = servicesRepository;
        }

        public async Task<ServiceResponse<List<Service>>> GetBusinussServices()
        {
            try
            {
                var services = await _servicesRepository.GetAllAsync();
                return new ServiceResponse<List<Service>>
                {
                    Data = services,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                return await LogError<List<Service>>(ex, null); 
            }
        }


    }
}
