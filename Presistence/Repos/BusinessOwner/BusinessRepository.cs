﻿using Application.Interfaces.Repos.BusinessOwner;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repos.BusinessOwner
{
    public class BusinessRepository : BaseRepository<Business>, IBusinessRepository
    {
        public BusinessRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddServicesToBusinessAsync(Guid BusId,ICollection<Guid> SerId )
        {
           var bus = await _dbContext.Businesses.FindAsync(BusId);
            foreach (Guid item in SerId)
            {
                BusinessService businessService = new BusinessService() { BusinessId = BusId, ServiceId = item };
                bus.BusinessServices.Add(businessService);
            }   
       }

        public async Task<List<string>> GetServicesNameAsync(Guid BusId)
        {
            List<Guid>ser = _dbContext.BusinessServices.Where(a=>a.BusinessId==BusId).Select(a=>a.ServiceId).ToList();
            List<string> ServicesNames = new List<string>();
            foreach (Guid item in ser)
            {
                var s=await _dbContext.Services.FindAsync(item);
                ServicesNames.Add(s.ServiceTitle);
            }
            return ServicesNames;
        }
    }
}