using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace TiendaServicios.Api.Librerias.Persistencia
{
    public class ContextoLibrerias
    {
        public ContextoLibrerias(IFirebaseClient context)
        {
            IFirebaseConfig config = new FirebaseConfig
            {

                AuthSecret = "nwdofuLa5ZlwLQDjiOPQEx06H5klAoP3lK1MMeSn",
                BasePath = "https://apilibrerias-default-rtdb.firebaseio.com/"

            };

            context = new FirebaseClient(config);
        }
    }
}
