using Microsoft.AspNetCore.Mvc;
using PruebaIngresoBibliotecario.Api.Interfaces;
using PruebaIngresoBibliotecario.Application.Interfaces;
using PruebaIngresoBibliotecario.Domain.Models.Dto.Response;
using PruebaIngresoBibliotecario.Domain.Models.Interfaces;
using PruebaIngresoBibliotecario.Infraestructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api.Base
{
    [ApiController]
    public class BaseController<TEntity, T, DtoRequest, DtoResponse> : ControllerBase, IBaseController<TEntity, T, DtoRequest, DtoResponse>
        where TEntity : BaseEntity<T>
        where T : struct
        where DtoRequest : IBaseDtoRequest<TEntity, T>
        where DtoResponse : IBaseDtoResponse
    {
        protected readonly IBaseService<TEntity, T, DtoResponse> baseService;

        public BaseController(IBaseService<TEntity, T, DtoResponse> baseService)
        {
            this.baseService = baseService;
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual async Task Delete([FromBody]T id)
        {
            await baseService.DeleteAsync(id);
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<DtoResponse>>> Get()
        {
            var list = await baseService.GetAllAsync();
            return Ok(list?.Select(x => (DtoResponse)
                Activator.CreateInstance(typeof(DtoResponse), new object[] { x })));
        }

        [HttpGet]
        [Route("base/{id}")]
        public virtual async Task<ActionResult<DtoResponse>> Get([FromRoute] T id)
        {
            var result = await baseService.GetByPKAsync(id);

            if (Equals(result, null))
            {
                return NotFound();
            }
            else
            {
                return Ok((DtoResponse)Activator.CreateInstance(
                    typeof(DtoResponse), new object[] { result }));
            }
        }

        [HttpPost]
        public virtual async Task<ActionResult<DtoResponse>> Post([FromBody] DtoRequest request)
        {
            return await baseService.CreateAsync(request.ToEntity());
        }

        [HttpPut]
        [Route("{id}")]
        public virtual async Task Put([FromRoute] T id, [FromBody] DtoRequest request)
        {
            await baseService.UpdateAsync(id, request.ToEntity());
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [NonAction]
        public ActionResult<PrestamoResponseDto> ValidateResponse(PrestamoResponseDto result)
        {        
            if (result is PrestamoResponseNotFoundDto)
            {
                return NotFound(result);
            }

            if (result is PrestamoResponseFailDto)
            {
                return BadRequest(result);
            }

            return result;
        }
    }
}
