using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TiendaServicios.Api.Gateway.InterfaceRemote;

namespace TiendaServicios.Api.Gateway.ImplementRemote
{
    public class AutorRemote : IAutorRemote
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<AutorRemote> _logger;

        public AutorRemote(IHttpClientFactory clientFactory, ILogger<AutorRemote> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }
        public async Task<(bool resultado, AutorModeloRemote autor, string ErrorMessage)> GetAutor(Guid AutorId)
        {
            try
            {
                var cliente = _clientFactory.CreateClient("AutorService");
                var response = await cliente.GetAsync($"/Autor/{AutorId}");
                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions(){PropertyNameCaseInsensitive = true};
                    var resultado = JsonSerializer.Deserialize<AutorModeloRemote>(contenido,options);
                    return (true, resultado, null);
                }

                return (false, null, response.ReasonPhrase);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return (false, null, e.Message);
            }
        }

       
    }
}
