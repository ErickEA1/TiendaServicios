
using System.Threading.Tasks;
using System.Threading;
using System;
using FluentValidation;
using TiendaServicios.Api.Libro.Persistencia;
using TiendaServicios.Api.Libro.Modelo;
using MediatR;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Titulo { get; set; }
            public DateTime FechaPublicacion { get; set; }
            public Guid AutorLibro { get; set; }
            public double Precio { get; set; }
        }

        public class EjcutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjcutaValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
                RuleFor(x => x.AutorLibro).NotEmpty();
                RuleFor(x => x.Precio).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoLibreria _context;
            public Manejador(ContextoLibreria contexto)
            {
                _context = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var id = Guid.NewGuid();
                //Se crea la instancia del autor-libro ligado al contexto
                var libro = new LibreriaMaterial
                {
                    LibreriaMaterialId=id,
                    Titulo = request.Titulo,
                    FechaPublicacion = request.FechaPublicacion,
                    AutorLibro =request.AutorLibro,
                    Precio = request.Precio,
                };
                //Agregar el objeto
                _context.LibreriasMaterial.Add(libro);
                //Insertamos el valor de insercion 
                var respuesta = await _context.SaveChangesAsync();
                if (respuesta > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el libro");
            }
        }
    }
}
