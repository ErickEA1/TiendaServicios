using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta: IRequest<CarritoDto>
        {
            public int CarritoSesionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {
            private readonly CarritoContexto carritoContexto;
            private readonly ILibroService libroService;
            public Manejador(CarritoContexto _carritoContexto, ILibroService _libroService)
            {
                carritoContexto = _carritoContexto;
                libroService = _libroService;
            }

            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await carritoContexto.CarritoSesiones.FirstOrDefaultAsync(x => x.CarritoSesionId == 
                request.CarritoSesionId);

                var carritoSesionDetalle = await carritoContexto.CarritoSesionDetalle.Where(x => x.CarritoSesionId == 
                request.CarritoSesionId).ToListAsync();

                var listaCarritoDto = new List<CarritoDetalleDto>();

                foreach (var libro in carritoSesionDetalle)
                {
                    var response = await libroService.GetLibro(new Guid(libro.ProductoSeleccionado));
                    if (response.resultado)
                    {
                        var objetoLibro = response.libro;
                        var carritoDetalle = new CarritoDetalleDto
                        {
                         
                            TituloLibro = objetoLibro.Titulo,
                            FechaPublicacion = objetoLibro.FechaPublicacion,
                            Precio= objetoLibro.Precio,
                            LibroId = objetoLibro.LibreriaMateriaId
                        };
                        listaCarritoDto.Add(carritoDetalle);
                    }
                }

                //se llena el objeto que se va a retornar 
                var carritoSessionDto = new CarritoDto
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    listaDeProductos = listaCarritoDto
                };

                return carritoSessionDto;
            }
        }
       
    }
}
