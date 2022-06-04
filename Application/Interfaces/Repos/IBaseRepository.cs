using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repos
{
    public interface IBaseRepository<T>
    {
        T GetById(Guid id);  
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
        Task<int> Count(Expression<Func<T, bool>> predicate = null);
        void Create(T obj);
        Task CreateRangeAsync(IEnumerable<T> objList);
        //void Delete(Action<T> deleteFunction, Expression<Func<T, bool>> predicate = null);
        void Delete(Guid id);
        void Update(T obj, params string[] excludedFields);
    }
}
