using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class ConsultarFiltro
    {
        public class AutorUnico : IRequest<AutorDto>
        {
            public Guid AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDto>
        {
            private readonly ContextoAutor _contextoAutor;
            private readonly IMapper _mapper;

            public Manejador(ContextoAutor contextoAutor, IMapper mapper)
            {
                this._contextoAutor = contextoAutor;
                this._mapper = mapper;
            }
            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await _contextoAutor.AutorLibros
                    .Where(p => p.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync();

                if (autor == null)
                {
                    throw new Exception("No se encontro el autor");
                }
                var autorDto = _mapper.Map<AutorLibro,AutorDto>(autor);
                return autorDto;
            }
        }
    }
}
