using Microsoft.EntityFrameworkCore;
using PruebaIngresoBibliotecario.Infraestructure.Base;
using PruebaIngresoBibliotecario.Infraestructure.Context;
using PruebaIngresoBibliotecario.Infraestructure.Entities;
using PruebaIngresoBibliotecario.Infraestructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace PruebaIngresoBibliotecario.Infraestructure.Repository
{
    public class PrestamoRepository : BaseRepository<Prestamo, Guid>, IPrestamoRepository
    {
        public PrestamoRepository(PersistenceContext _persistenceContext) 
            : base(_persistenceContext)
        {
        }

        public async Task<IEnumerable<Prestamo>> GetAllByUserAsync(string identificacionUsuario)
        {
            return await persistenceContext.Set<Prestamo>().AsNoTracking()
                         .Where(p => p.IdentificacionUsuario.Equals(identificacionUsuario)).ToListAsync();
        }

        public async Task<Prestamo> GetByIsbn(string isbn)
        {
            return await persistenceContext.Set<Prestamo>().AsNoTracking()
                         .FirstOrDefaultAsync(p => p.Isbn.Equals(isbn));
        }
    }
}
