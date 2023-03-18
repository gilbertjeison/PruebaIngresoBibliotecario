using PruebaIngresoBibliotecario.Infraestructure.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Infraestructure.Interfaces
{
    public interface IPrestamoRepository : IBaseRepository<Prestamo, Guid>
    {
        Task<IEnumerable<Prestamo>> GetAllByUserAsync(string identificacionUsuario);
        Task<Prestamo> GetByIsbn(string isbn);
    }
}
