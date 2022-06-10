using Application.Interfaces.Repos;
using Domain.Entites.General;
using Presistence.Interfaces.Repos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {

            _dbContext = dbContext;
        }
        
        public T GetById(Guid id)
        {
            return _dbContext.Set<T>().FirstOrDefault(z => z.Id == id);
         }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(z => z.Id == id);
        }

        public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return await _dbContext.Set<T>().ToListAsync();
            else
                return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public  List<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return  _dbContext.Set<T>().ToList();
            else
                return  _dbContext.Set<T>().Where(predicate).ToList();
        }



        public Task<int> Count(Expression<Func<T, bool>> predicate = null) =>
                        predicate == null
                ? _dbContext.Set<T>().CountAsync()
                : _dbContext.Set<T>().Where(predicate).CountAsync();

        public virtual T FindByID(Guid Id)
        {
            return _dbContext.Set<T>().FirstOrDefault(z => z.Id == Id);

        }

        public virtual void Create(T obj)
        {
            _dbContext.Attach(obj);
        }

        public virtual async Task CreateRangeAsync(IEnumerable<T> objList)
        {
            await _dbContext.AddRangeAsync(objList);
        }


        public virtual void Delete(Guid id)
        {
            T existing = _dbContext.Set<T>().Find(id);
            if(existing != null)
                _dbContext.Set<T>().Remove(existing);        }

        public void Update(T obj, params string[] excludedFields)
        {
            foreach (string fld in excludedFields)
                _dbContext.Entry(obj).Property(fld).IsModified = false;
        }

    }   
}
