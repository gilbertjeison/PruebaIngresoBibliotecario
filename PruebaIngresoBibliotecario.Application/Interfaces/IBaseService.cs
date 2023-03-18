using PruebaIngresoBibliotecario.Domain.Models.Interfaces;
using PruebaIngresoBibliotecario.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Application.Interfaces
{
    public interface IBaseService<TEntity, T, DtoResponse> 
        where TEntity : IBaseEntity<T> 
        where T : struct
        where DtoResponse : IBaseDtoResponse

    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetAllFilterAsync(Func<TEntity, bool> predicate);

        Task<TEntity> GetByPKAsync(T id);

        Task<DtoResponse> CreateAsync(TEntity entity);

        Task UpdateAsync(T id, TEntity entity);

        Task DeleteAsync(T id);
    }
}
