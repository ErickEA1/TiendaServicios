using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using AutoMapper;
using TiendaServicios.Api.Librerias.Modelo;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using static TiendaServicios.Api.Librerias.Aplicacion.Consulta;
using System.Collections.Generic;
using FireSharp;

namespace TiendaServicios.Api.Librerias.Aplicacion
{
    public class ConsultaFiltro
    {
        public class LibreriaUnica : IRequest<LibreriaDto>
        {
            public string LibreriaGuid { get; set; }
        }

        public class Manejador : IRequestHandler<LibreriaUnica, LibreriaDto>
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

            public async Task<LibreriaDto> Handle(LibreriaUnica request, CancellationToken cancellationToken)
            {
                FirebaseResponse response = await contexto.GetAsync($"librerias/{request.LibreriaGuid}");
                return response.ResultAs<LibreriaDto>();
            }
        }
    }
}
