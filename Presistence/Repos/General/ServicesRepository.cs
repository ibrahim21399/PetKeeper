using Application.Interfaces.Repos.General;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repos.General
{
    public class ServicesRepository:BaseRepository<Service> ,IServicesRepository
    {
       
        public ServicesRepository(ApplicationDbContext dbContext):base(dbContext)   
        {
            
        }
    }
}
