using Domain.Entites;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Application.Interfaces.Repos.General;

namespace Presistence.Repos.General
{
    public class CityAreaRepository<T> : ICityAreaRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbcontext;
        public CityAreaRepository(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)

                return await _dbcontext.Set<T>().ToListAsync();
            else
                return await _dbcontext.Set<T>().Where(predicate).ToListAsync();


        }

        public T GetById(int id)
        {
            return _dbcontext.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }


    }
}
