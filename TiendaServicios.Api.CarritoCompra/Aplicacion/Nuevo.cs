using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Modelo;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
         public class Ejecuta : IRequest
        {
            public DateTime FechaCreacionSesion { get; set; }
            public Guid IdUser { get; set; }
            public List<string> listaDeProductos { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CarritoContexto _context;
            public Manejador(CarritoContexto context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion,
                    IdUser=request.IdUser
                };
                _context.CarritoSesiones.Add(carritoSesion);
                var result = await _context.SaveChangesAsync();
                if (result == 0)
                    throw new Exception("No se Pudo insertar en el carrito");
                int id = carritoSesion.CarritoSesionId;
                foreach (var p in request.listaDeProductos)
                {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionado = p
                    };
                    _context.CarritoSesionDetalle.Add(detalleSesion);
                }
                var value = await _context.SaveChangesAsync();

                if(value > 0)
                    return Unit.Value;

                throw new Exception("No se Pudo insertar el detalle del carrito de compras");
            }
        }
    }
}
