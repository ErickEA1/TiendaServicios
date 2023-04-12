using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
    using TiendaServicios.Api.Librerias.Modelo;

    namespace TiendaServicios.api.librerias.Aplicacion
    {
        public class Nuevo
        {
            public class Ejecuta : IRequest
            {
                public string Nombre { get; set; }
                public string CorreoContacto { get; set; }
                public string Direccion { get; set; }
                public string Telefono { get; set; }

            }

            public class EjecutaValidacion : AbstractValidator<Ejecuta>
            {
                // 

                public EjecutaValidacion()
                {
                    RuleFor(p => p.Nombre).NotEmpty();
                    RuleFor(p => p.CorreoContacto).NotEmpty();
                    RuleFor(p => p.Direccion).NotEmpty();
                    RuleFor(p => p.Telefono).NotEmpty();

                }


            }

            public class Manejador : IRequestHandler<Ejecuta>
            {
                private readonly IFirebaseClient contexto;

                public Manejador()
                {
                    IFirebaseConfig config = new FirebaseConfig
                    {

                        AuthSecret = "nwdofuLa5ZlwLQDjiOPQEx06H5klAoP3lK1MMeSn",
                        BasePath = "https://apilibrerias-default-rtdb.firebaseio.com/"

                    };

                    contexto = new FirebaseClient(config);

                }

                public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
                {
                    string id = Guid.NewGuid().ToString("N");

                    var libreria = new LibreriaModel
                    {
                        Nombre = request.Nombre,
                        CorreoContacto = request.CorreoContacto,
                        Telefono = request.Telefono,
                        Direccion = request.Direccion
                    };

                    SetResponse response = contexto.Set("librerias/" + id, libreria);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return Unit.Value;
                    }
                    throw new Exception("error");
                }
        }
    }
}
