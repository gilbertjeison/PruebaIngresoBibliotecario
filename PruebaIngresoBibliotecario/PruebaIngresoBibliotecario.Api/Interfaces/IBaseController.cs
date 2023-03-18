using Microsoft.AspNetCore.Mvc;
using PruebaIngresoBibliotecario.Domain.Models.Interfaces;
using PruebaIngresoBibliotecario.Infraestructure.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api.Interfaces
{
    public interface IBaseController<TEntity, T, DtoRequest, DtoResponse>
        where TEntity : BaseEntity<T>
        where T : struct
        where DtoRequest : IBaseDtoRequest<TEntity, T>
        where DtoResponse : IBaseDtoResponse
    {
        Task<ActionResult<IEnumerable<DtoResponse>>> Get();

        Task<ActionResult<DtoResponse>> Get(T id);

        Task<ActionResult<DtoResponse>> Post(DtoRequest request);

        Task Put(T id, DtoRequest request);

        Task Delete(T id);
    }
}
