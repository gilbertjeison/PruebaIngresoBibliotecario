using PruebaIngresoBibliotecario.Application.Interfaces;
using PruebaIngresoBibliotecario.Domain.Models.Interfaces;
using PruebaIngresoBibliotecario.Infraestructure.Base;
using PruebaIngresoBibliotecario.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Application.Base
{
    public class BaseService<TEntity, T, DtoResponse> : IBaseService<TEntity, T, DtoResponse>
        where TEntity : BaseEntity<T>
        where T : struct
        where DtoResponse : IBaseDtoResponse
    {
        protected readonly IBaseRepository<TEntity, T> baseRepository;

        public BaseService(IBaseRepository<TEntity, T> baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        public async Task<DtoResponse> CreateAsync(TEntity entity)
        {
            var result = await baseRepository.CreateAsync(entity);
            return (DtoResponse)Activator.CreateInstance(typeof(DtoResponse), new object[] { result });
            
        }

        public async Task DeleteAsync(T id)
        {
            await this.baseRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.baseRepository.GetAllAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllFilterAsync(Func<TEntity, bool> predicate)
        {
            return await this.baseRepository.GetAllFilterAsync(predicate);
        }

        public async Task<TEntity> GetByPKAsync(T id)
        {
            return await this.baseRepository.GetByPKAsync(id);
        }

        public async Task UpdateAsync(T id, TEntity entity)
        {
            await this.baseRepository.UpdateAsync(id, entity);
        }
    }
}
