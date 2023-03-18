using PruebaIngresoBibliotecario.Infraestructure.Interfaces;

namespace PruebaIngresoBibliotecario.Infraestructure.Base
{
    public abstract class BaseEntity<T> : IBaseEntity<T> where T : struct
    {
        public virtual T? Id { get; set; }
    }
}
