using System;
using Domain.Entites;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repos.BusinessOwner
{
    public interface IBusinessRepository:IBaseRepository<Business>
    {
        Task AddServicesToBusinessAsync(Guid BusId, ICollection<Guid> SerID);
        Task<List<string>> GetServicesNameAsync(Guid BusId);
        string GetBusinessNameAsync(Guid BusId);
    }
}
