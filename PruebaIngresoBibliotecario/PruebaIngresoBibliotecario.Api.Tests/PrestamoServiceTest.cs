using Api.Test;
using Moq;
using PruebaIngresoBibliotecario.Application.Interfaces;
using PruebaIngresoBibliotecario.Application.Services;
using PruebaIngresoBibliotecario.Domain.Models.Dto.Response;
using PruebaIngresoBibliotecario.Domain.Models.Dto.Resquest;
using PruebaIngresoBibliotecario.Infraestructure.Entities;
using PruebaIngresoBibliotecario.Infraestructure.Interfaces;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PruebaIngresoBibliotecario.Api.Tests
{
    public class PrestamoServiceTest : IntegrationTestBuilder
    {
        private readonly Mock<IPrestamoRepository> prestamoAdapter;

        

        public PrestamoServiceTest()
        {
            Prestamo prestamo1 = new Prestamo()
            {
                Id = Guid.NewGuid(),
                Isbn = Guid.NewGuid().ToString(),
                FechaMaximaDevolucion = CalcularFechaEntrega(TipoUsuarioPrestamo.INVITADO),
                TipoUsuario = 3,
                IdentificacionUsuario = "1007"
                
            };

            this.prestamoAdapter = new Mock<IPrestamoRepository>();
            prestamoAdapter.Setup(x => x.CreateAsync(It.IsAny<Prestamo>())).ReturnsAsync(prestamo1);
        }

        [Fact]
        public async Task Create_OK()
        {
            PrestamoRequestDto requestDto = new PrestamoRequestDto()
            {
                identificacionUsuario = "123",
                Isbn = Guid.NewGuid().ToString(),
                tipoUsuario = 3
            };

            IPrestamoService prestamo = new PrestamoService(prestamoAdapter.Object);

            var prestamoService = new Mock<IPrestamoService>();
            prestamoService.Setup(x => x.CreateAsync(requestDto))
                .ReturnsAsync(new Domain.Models.Dto.Response.PrestamoResponseDto { });

            var res = await prestamo.CreateAsync(requestDto);
            Assert.NotNull(res);
        }

        [Fact]
        public async Task Validation_ERROR()
        {
            PrestamoRequestDto requestDto = new PrestamoRequestDto()
            {
                identificacionUsuario = "1231321321321321",
                Isbn = Guid.NewGuid().ToString(),
                tipoUsuario = 3
            };

            IPrestamoService prestamo = new PrestamoService(prestamoAdapter.Object);

            var prestamoService = new Mock<IPrestamoService>();
            prestamoService.Setup(x => x.CreateAsync(requestDto))
                .ReturnsAsync(new Domain.Models.Dto.Response.PrestamoResponseDto { });

            var res = await prestamo.CreateAsync(requestDto);

            Assert.IsType<PrestamoResponseFailDto>(res);
            Assert.NotNull((res as PrestamoResponseFailDto).mensaje);
        }
    }
}
