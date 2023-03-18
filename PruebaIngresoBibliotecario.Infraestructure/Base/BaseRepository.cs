using Microsoft.EntityFrameworkCore;
using PruebaIngresoBibliotecario.Infraestructure.Context;
using PruebaIngresoBibliotecario.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Infraestructure.Base
{
    public class BaseRepository<TEntity, T> : IBaseRepository<TEntity, T>
        where TEntity : BaseEntity<T>
        where T : struct
    {
        protected readonly PersistenceContext persistenceContext;

        public BaseRepository(PersistenceContext _persistenceContext)
        {
            this.persistenceContext = _persistenceContext;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var newEntity = await persistenceContext.Set<TEntity>().AddAsync(entity);
            await persistenceContext.SaveChangesAsync();

            var retrievedEntity = await newEntity.GetDatabaseValuesAsync();
            return retrievedEntity.ToObject() as TEntity;
        }

        public async Task CreateMultipleAsync(IEnumerable<TEntity> entities)
        {
            await persistenceContext.Set<TEntity>().AddRangeAsync(entities);
            await persistenceContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T id)
        {
            var entity = await GetByPKAsync(id);
            if (!Equals(entity, null))
            {
                persistenceContext.Set<TEntity>().Remove(entity);
                await persistenceContext.SaveChangesAsync();
            }
            else
            {
                throw new DbUpdateException($"Not Found PK {id} para la entidad {typeof(TEntity)}");
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            return persistenceContext.Set<TEntity>().AsNoTracking();
        }

        public IQueryable<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> func)
        {
            return func(persistenceContext.Set<TEntity>()).AsQueryable();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await persistenceContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllFilterAsync(Func<TEntity, bool> predicate)
        {
            if (!Equals(predicate, null))
            {
                return persistenceContext.Set<TEntity>().Where(predicate).ToList();
            }
                
            return await GetAllAsync();            
        }

        public async Task<TEntity> GetByPKAsync(T id)
        {
            return await persistenceContext.Set<TEntity>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task UpdateAsync(T id, TEntity entity)
        {
            var entityToUpdate = await GetByPKAsync(id);
            if (!Equals(entityToUpdate, null))
            {
                List<PropertyInfo> properties = entity.GetType().GetProperties().ToList();
                properties.ForEach(
                    prop =>
                    {
                        if (prop.Name != nameof(IBaseEntity<T>.Id))
                        {
                            var newValue = entity.GetType().GetProperty(prop.Name)?.GetValue(entity);
                            entityToUpdate.GetType().GetProperty(prop.Name)?.SetValue(entityToUpdate, newValue);
                        }
                    }
                );

                persistenceContext.Set<TEntity>().Update(entityToUpdate);
                await persistenceContext.SaveChangesAsync();
            }
            else
            {
                throw new DbUpdateException($"Not Found PK {id} para la entidad {typeof(TEntity)}");
            }
        }

        public async Task UpdateMultipleAsync(IEnumerable<TEntity> entities)
        {
            persistenceContext.Set<TEntity>().UpdateRange(entities);
            await persistenceContext.SaveChangesAsync();
        }
    }
}
