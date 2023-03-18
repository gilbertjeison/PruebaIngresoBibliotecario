using PruebaIngresoBibliotecario.Domain.Models.Interfaces;
using PruebaIngresoBibliotecario.Infraestructure.Entities;
using System;

namespace PruebaIngresoBibliotecario.Domain.Models.Dto.Response
{
    public class PrestamoResponseDto : IBaseDtoResponse
    {
        public string? id { get; set; }

        public string isbn { get; set; }

        public string identificacionUsuario { get; set; }

        public int? tipoUsuario { get; set; }

        public DateTime? fechaMaximaDevolucion { get; set; }

        public PrestamoResponseDto()
        {
        }

        public PrestamoResponseDto(Prestamo prestamo)
        {
            id = prestamo.Id.ToString();
            isbn = prestamo.Isbn;
            identificacionUsuario = prestamo?.IdentificacionUsuario;
            tipoUsuario = prestamo.TipoUsuario;
        }
    }
}
