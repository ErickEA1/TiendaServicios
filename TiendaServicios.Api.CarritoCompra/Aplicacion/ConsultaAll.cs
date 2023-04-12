using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Persistencia;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class ConsultaAll
    {
        public class Ejecuta: IRequest<List<CarritoDto>>
        {
           
        }

        public class Manejador : IRequestHandler<Ejecuta, List<CarritoDto>>
        {
            private readonly CarritoContexto carritoContexto;
            private readonly ILibroService libroService;
            public Manejador(CarritoContexto _carritoContexto, ILibroService _libroService)
            {
                carritoContexto = _carritoContexto;
                libroService = _libroService;
            }

            public async Task<List<CarritoDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await carritoContexto.CarritoSesiones.ToArrayAsync();

                var Compras = new List<CarritoDto>();

                foreach (var item in carritoSesion)
                {

                    var carritoSesionDetalle = await carritoContexto.CarritoSesionDetalle.Where(x => x.CarritoSesionId ==
                    item.CarritoSesionId).ToListAsync();

                    var listaCarritoDto = new List<CarritoDetalleDto>();

                    foreach (var libro in carritoSesionDetalle)
                    {
                        var response = await libroService.GetLibro(new System.Guid(libro.ProductoSeleccionado));
                        if (response.resultado)
                        {
                            var objetoLibro = response.libro;
                            var carritoDetalle = new CarritoDetalleDto
                            {

                                TituloLibro = objetoLibro.Titulo,
                                FechaPublicacion = objetoLibro.FechaPublicacion,
                                Precio = objetoLibro.Precio,
                                LibroId = objetoLibro.LibreriaMateriaId
                            };
                            listaCarritoDto.Add(carritoDetalle);
                        }
                    }

                    //se llena el objeto que se va a retornar 
                    var carritoSessionDto = new CarritoDto
                    {
                        CarritoId = item.CarritoSesionId,
                        IdUser=item.IdUser,
                        FechaCreacionSesion = item.FechaCreacion,
                        listaDeProductos = listaCarritoDto
                    };
                    Compras.Add(carritoSessionDto);
                }

                return Compras;
            }
        }
       
    }
}
