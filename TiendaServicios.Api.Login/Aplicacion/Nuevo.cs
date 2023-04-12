using FluentValidation;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using TiendaServicios.Api.Login.Persistencia;
using TiendaServicios.Api.Autor.Modelo;

namespace TiendaServicios.Api.Login.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class EjcutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjcutaValidacion()
            {
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoUsuario _context;
            public Manejador(ContextoUsuario contexto)
            {
                _context = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Se crea la instancia del Usuario ligado al contexto
                var UserNew = new Usuarios
                {
                    Username = request.Username,
                    Password = request.Password,
                    IdUser = Guid.NewGuid()
                };
                //Agregar el objeto
                _context.UsuariosLogin.Add(UserNew);
                //Insertamos el valor de insercion 
                var respuesta = await _context.SaveChangesAsync();
                if (respuesta > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo insertar el Autor del Libro");
            }
        }
    }
}
