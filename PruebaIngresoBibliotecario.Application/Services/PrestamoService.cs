using PruebaIngresoBibliotecario.Application.Base;
using PruebaIngresoBibliotecario.Application.Interfaces;
using PruebaIngresoBibliotecario.Domain.Models.Dto.Response;
using PruebaIngresoBibliotecario.Domain.Models.Dto.Resquest;
using PruebaIngresoBibliotecario.Infraestructure.Entities;
using PruebaIngresoBibliotecario.Infraestructure.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Application.Services
{
    public class PrestamoService : BaseService<Prestamo, Guid, PrestamoResponseDto>, IPrestamoService
    {
        private const int diasAfiliado = 10;
        private const int diasEmpleado = 8;
        private const int diasInvitado = 7;
        private readonly int usuarioInvitado = 3;
        private readonly int maxLongIdentificacionUsuario = 10;
        private readonly IPrestamoRepository prestamoRepository;

        public PrestamoService(IPrestamoRepository prestamoRepository) : base(prestamoRepository)
        {
            this.prestamoRepository = prestamoRepository;
        }

        public async Task<PrestamoResponseDto> CreateAsync(PrestamoRequestDto prestamoRequest)
        {
            var validationResponse = ValidateRequest(prestamoRequest);
            if (validationResponse != null)
            {
                return validationResponse;
            }

            var userBooks = await prestamoRepository.GetAllByUserAsync(prestamoRequest.identificacionUsuario);            
            
            if (prestamoRequest.tipoUsuario == usuarioInvitado &&
                    prestamoRequest.identificacionUsuario == userBooks.FirstOrDefault
                        (x => x.TipoUsuario == usuarioInvitado)?.IdentificacionUsuario 
                            && userBooks.ToList().Count >= 1)
            {
                return SetFailDtoObject($"El usuario con identificacion {prestamoRequest.identificacionUsuario} " +
                        $"ya tiene un libro prestado por lo cual no se le puede realizar otro prestamo");
            }

            var prestamoEntity = prestamoRequest.ToEntity();
            prestamoEntity.FechaMaximaDevolucion = CalculateExpirationDate(prestamoEntity.TipoUsuario);
            var result = await baseRepository.CreateAsync(prestamoEntity);
            return new PrestamoResponseDto
            {
                id = result.Id.ToString(),
                fechaMaximaDevolucion = result.FechaMaximaDevolucion                
            };
        }

        private PrestamoResponseFailDto ValidateRequest(PrestamoRequestDto prestamoRequest)
        {
            if (prestamoRequest != null && prestamoRequest.identificacionUsuario.Length > maxLongIdentificacionUsuario)
            {
                return SetFailDtoObject($"El número de identificación de usuario no puede superar los " +
                    $"{maxLongIdentificacionUsuario} caracteres");
            }

            if (prestamoRequest.tipoUsuario > 3 || prestamoRequest.tipoUsuario < 1)
            {
                return SetFailDtoObject("El tipo de usuario solo puede ser uno de los siguientes " +
                    "[1 - Afiliado 2 - Empleado de la biblioteca 3 - Invitado]");                
            }

            return null;
        }

        private PrestamoResponseFailDto SetFailDtoObject(string message)
        {
            return new PrestamoResponseFailDto
            {
                mensaje = message
            };
        }

        private DateTime CalculateExpirationDate(int tipoUsuario)
        {
            switch (tipoUsuario)
            {
                case 1:
                    return AddBussinesDays(diasAfiliado);
                case 2:
                    return AddBussinesDays(diasEmpleado);
                case 3:
                    return AddBussinesDays(diasInvitado);
            }

            return DateTime.Now;
        }

        private DateTime AddBussinesDays(int daysToAdd)
        {
            DateTime date = DateTime.Now;

            while (daysToAdd > 0)
            {
                date = date.AddDays(1);

                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    daysToAdd -= 1;
                }
            }

            return date;
        }

        public async Task<PrestamoResponseDto> GetByID(Guid id)
        {
            var result = await prestamoRepository.GetByPKAsync(id);

            if (result != null)
            {
                return new PrestamoResponseDto
                {
                    fechaMaximaDevolucion = result.FechaMaximaDevolucion,
                    id = result.Id.ToString(),
                    isbn = result.Isbn,
                    identificacionUsuario = result.IdentificacionUsuario,
                    tipoUsuario = result.TipoUsuario
                };
            }

            return new PrestamoResponseNotFoundDto
            {
                mensaje = $"El prestamo con id {id} no existe"
            };
        }
    }
}
