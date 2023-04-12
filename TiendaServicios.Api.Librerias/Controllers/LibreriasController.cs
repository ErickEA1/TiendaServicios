using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Firebase.Database;
using Firebase.Database.Query;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using TiendaServicios.api.librerias.Aplicacion;
using TiendaServicios.Api.Librerias.Aplicacion;

namespace TiendaServicios.Api.Librerias.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibreriasController : ControllerBase
    {

        private readonly IMediator _mediator;

        public LibreriasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<LibreriaDto>>> GetAutores()
        {
            return await _mediator.Send(new Consulta.ListaLibrerias());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibreriaDto>> GetAutorLibro(string id)
        {
            return await _mediator.Send(new ConsultaFiltro.LibreriaUnica { LibreriaGuid = id });
        }




    }
}
