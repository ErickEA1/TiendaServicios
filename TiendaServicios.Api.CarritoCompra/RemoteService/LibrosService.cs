using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.Api.CarritoCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoCompra.RemoteService
{
    public class LibrosService : ILibroService
    {
        private readonly IHttpClientFactory httpClient;
        private readonly ILogger<LibrosService> logger;

        public LibrosService(IHttpClientFactory _httpClient, ILogger<LibrosService> _logger)
        {
        httpClient = _httpClient;
        logger=_logger;
        }
        
        public async Task<(bool resultado, LibroRemote libro, string ErrorMessage)> GetLibro(Guid LibroId)
        {
            try
            {
                var cliente = httpClient.CreateClient("Libro");
                var response = await cliente.GetAsync($"https://localhost:44343/api/libro/{LibroId}");
                if (response.IsSuccessStatusCode)
                {
                    var contenido=await response.Content.ReadAsStringAsync();
                    var options=new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var resultado= JsonSerializer.Deserialize<LibroRemote>(contenido, options);
                    return (true, resultado, null);
                }
                return (false,null,response.ReasonPhrase);
            }catch(Exception e)
            {
                logger.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }
    }
}
