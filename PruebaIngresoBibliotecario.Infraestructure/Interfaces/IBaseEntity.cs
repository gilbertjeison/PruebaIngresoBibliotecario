namespace PruebaIngresoBibliotecario.Infraestructure.Interfaces
{
    public interface IBaseEntity<T> where T : struct
    {
        T? Id { get; }
    }
}
