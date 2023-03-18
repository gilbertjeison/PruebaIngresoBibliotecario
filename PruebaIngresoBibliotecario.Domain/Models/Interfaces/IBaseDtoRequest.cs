using PruebaIngresoBibliotecario.Infraestructure.Base;

namespace PruebaIngresoBibliotecario.Domain.Models.Interfaces
{
    public interface IBaseDtoRequest<TEntity, T> 
        where TEntity : BaseEntity<T> 
        where T : struct
    {
        TEntity ToEntity();
    }
}
