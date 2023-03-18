using PruebaIngresoBibliotecario.Domain.Models.Interfaces;
using PruebaIngresoBibliotecario.Infraestructure.Base;

namespace PruebaIngresoBibliotecario.Domain.Models.Dto
{
    public abstract class BaseDtoRequest<TEntity, T> : IBaseDtoRequest<TEntity, T>
        where TEntity : BaseEntity<T>
        where T : struct
    {
        public abstract TEntity ToEntity();
    }
}
