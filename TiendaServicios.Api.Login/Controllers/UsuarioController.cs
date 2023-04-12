using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Login.Aplicacion;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TiendaServicios.Api.Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }
// GET: api/<UsuarioController>
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await _mediator.Send(data);
        }

        /*[HttpGet]
        public async Task<ActionResult<List<UsuarioDto>>> GetAutores()
        {
            return await _mediator.Send(new Consulta.Lista());
        }*/

        [HttpGet]
        public async Task<DtaRes> GetAutorLibro(string Username, string Password)
        {
            return await _mediator.Send(new ConsultaFiltro.UsuarioUnico { Username=Username, Password=Password });
        }
    }
}
