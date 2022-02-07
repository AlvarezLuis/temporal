using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Impexium.Entities.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        Task Remove(T entity);
        Task RemoveRange(IEnumerable<T> entities);
        Task<T> UpdateAsync(T entity, object key);
    }
}
