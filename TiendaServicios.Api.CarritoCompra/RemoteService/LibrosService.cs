using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TiendaServicios.Api.CarritoCompra
{
    public class LibrosService : ILibrosService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<LibrosService> _logger;

        public LibrosService(IHttpClientFactory clienteFactory, ILogger<LibrosService> logger)
        {
            _clientFactory = clienteFactory;
            _logger = logger;
        }
        public async Task<(bool resultado, LibroRemote libro, string ErrorMessage)> GetLibro(Guid LibroId)
        {
            try
            {
                var cliente = _clientFactory.CreateClient("Libros");
                var response = await cliente.GetAsync($"api/LibroMaterial/{LibroId}"); //invoca al endpoint que necesito
                if (response.IsSuccessStatusCode)
                {
                    var contenido = await  response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
                    var resultado = JsonSerializer.Deserialize<LibroRemote>(contenido, options);
                    return (true, resultado, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception e)
            {
               _logger?.LogError(e.ToString());
               return (false, null, e.Message);
            }
        }
    }
}
