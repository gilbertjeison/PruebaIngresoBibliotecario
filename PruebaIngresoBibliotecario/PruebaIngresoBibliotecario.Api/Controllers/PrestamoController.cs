using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaIngresoBibliotecario.Api.Base;
using PruebaIngresoBibliotecario.Application.Interfaces;
using PruebaIngresoBibliotecario.Domain.Models.Dto.Response;
using PruebaIngresoBibliotecario.Domain.Models.Dto.Resquest;
using PruebaIngresoBibliotecario.Infraestructure.Entities;
using System;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : BaseController<Prestamo, Guid, PrestamoRequestDto, PrestamoResponseDto>
    {
        private readonly IPrestamoService prestamoService;

        public PrestamoController(IPrestamoService prestamoService)
            : base(prestamoService)
        {
            this.prestamoService = prestamoService;
        }

        [ProducesResponseType(StatusCodes.Status201Created)] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        [HttpPost]
        public async override Task<ActionResult<PrestamoResponseDto>> Post([FromBody] PrestamoRequestDto request)
        {
            var result = await prestamoService.CreateAsync(request);
            return ValidateResponse(result);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        [Route("{id-prestamo}")]
        public async Task<ActionResult<PrestamoResponseDto>> Get([FromRoute][FromQuery(Name = "id-prestamo")] Guid id)
        {
            var result = await prestamoService.GetByID(id);
            return ValidateResponse(result);
        }
    }
}
