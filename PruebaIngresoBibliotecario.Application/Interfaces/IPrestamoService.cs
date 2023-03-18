using PruebaIngresoBibliotecario.Domain.Models.Dto.Response;
using PruebaIngresoBibliotecario.Domain.Models.Dto.Resquest;
using PruebaIngresoBibliotecario.Infraestructure.Entities;
using System;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Application.Interfaces
{
    public interface IPrestamoService : IBaseService<Prestamo, Guid, PrestamoResponseDto>
    {
        Task<PrestamoResponseDto> CreateAsync(PrestamoRequestDto prestamoRequest);
        Task<PrestamoResponseDto> GetByID(Guid id);

    }
}
