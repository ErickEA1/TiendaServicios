using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp;
using FireSharp.Response;
using Newtonsoft.Json;

namespace TiendaServicios.Api.Librerias.Aplicacion
{
    public class Consulta
    {
        public class ListaLibrerias : IRequest<List<LibreriaDto>>
        {

        }

        public class Manejador : IRequestHandler<ListaLibrerias, List<LibreriaDto>>
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

            public async Task<List<LibreriaDto>> Handle(ListaLibrerias request, CancellationToken cancellationToken)
            {
                List<LibreriaDto> libs = new List<LibreriaDto>();
               
               
                FirebaseResponse response = await contexto.GetAsync("librerias/");
                Dictionary<string,LibreriaDto> librerias = JsonConvert.DeserializeObject<Dictionary<string,LibreriaDto>>(response.Body);
                
                foreach(KeyValuePair<string,LibreriaDto> lib in librerias)
                {
                    libs.Add(new LibreriaDto
                    {
                        Identificador=lib.Key,
                        Nombre=lib.Value.Nombre,
                        CorreoContacto=lib.Value.CorreoContacto,
                        Direccion=lib.Value.Direccion,
                        Telefono=lib.Value.Telefono
                    });

                }

                return libs;
                
            }

           
        }
    }
}
