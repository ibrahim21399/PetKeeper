using Presistence.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ApplicationDbContext _dbcontext;   

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public async Task<int> CommitAsync()
        {
            return await _dbcontext.SaveChangesAsync();
        }
    }
}
