using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Infraestructure.Interfaces
{
    public interface IBaseRepository<TEntity, T> 
        where TEntity : IBaseEntity<T> 
        where T : struct
    {
        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> func);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetAllFilterAsync(Func<TEntity, bool> predicate);

        Task<TEntity> GetByPKAsync(T id);

        Task<TEntity> CreateAsync(TEntity entity);

        Task UpdateAsync(T id, TEntity entity);

        Task DeleteAsync(T id);

        Task CreateMultipleAsync(IEnumerable<TEntity> entities);

        Task UpdateMultipleAsync(IEnumerable<TEntity> entities);
    }
}
