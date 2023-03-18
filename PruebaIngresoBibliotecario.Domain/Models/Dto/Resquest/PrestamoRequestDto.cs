using PruebaIngresoBibliotecario.Infraestructure.Entities;
using System;

namespace PruebaIngresoBibliotecario.Domain.Models.Dto.Resquest
{
    public class PrestamoRequestDto : BaseDtoRequest<Prestamo, Guid>
    {
        public string identificacionUsuario { get; set; }

        public string Isbn { get; set; }

        public int tipoUsuario { get; set; }

        public override Prestamo ToEntity()
        {
            return new Prestamo
            {
                IdentificacionUsuario = identificacionUsuario,
                Isbn = Isbn,
                TipoUsuario = tipoUsuario
            };
        }
    }
}
