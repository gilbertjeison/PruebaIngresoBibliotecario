using PruebaIngresoBibliotecario.Infraestructure.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace PruebaIngresoBibliotecario.Infraestructure.Entities
{
    public class Prestamo : BaseEntity<Guid>
    {
        [Key]
        public override Guid? Id => base.Id;

        [Required]
        public string Isbn { get; set; }

        [MaxLength(10)]
        [Required]
        public string IdentificacionUsuario { get; set; }

        [Required]
        public int TipoUsuario { get; set; }

        [Required]
        public DateTime FechaMaximaDevolucion { get; set; }
    }
}
