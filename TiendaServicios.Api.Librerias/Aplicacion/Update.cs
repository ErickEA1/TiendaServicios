using AutoMapper;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Google.Apis.Requests.BatchRequest;

namespace TiendaServicios.Api.Librerias.Aplicacion
{
    public class Update
    {

        public class LibreriaUpdate : IRequest
        {
            public string Identificador { get; set; }
            public string Nombre { get; set; }
            public string CorreoContacto { get; set; }
            public string Direccion { get; set; }
            public string Telefono { get; set; }
        }

        public class Manejador : IRequestHandler<LibreriaUpdate>
        {
            private readonly IMapper _mapper;
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

            public async Task<Unit> Handle(LibreriaUpdate request, CancellationToken cancellationToken)
            {
                var response = await contexto.UpdateAsync($"librerias/{request.Identificador}", request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Unit.Value;
                }
                throw new Exception("error");
            }
        }
    }
}
