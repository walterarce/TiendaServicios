using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TiendaServicios.Mensajeria.Email.SendGridLibreria.Interface;
using TiendaServicios.Mensajeria.Email.SendGridLibreria.Modelo;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.EventoQueue;

namespace TiendaServicios.Api.Autor.ManejadorRabbit
{
    public class EmailEventoManejador : IEventoManejador<EmailEventoQueue>
    {
        private readonly ILogger<EmailEventoManejador> _logger;
        private readonly ISendGridEnviar _sendGridEnviar;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        public EmailEventoManejador(ILogger<EmailEventoManejador> logger, ISendGridEnviar sendGridEnviar, 
            Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _logger = logger;
            _sendGridEnviar = sendGridEnviar;
            _configuration = configuration;
        }
        public EmailEventoManejador()
        {
            
        }

        public async Task Handle(EmailEventoQueue @event)
        {
            _logger.LogInformation(@event.Titulo);

            var objData = new SendGridData();

            objData.Contenido = @event.Contenido;
            objData.Titulo = @event.Titulo;
            objData.EmailDestinatario = @event.Destinatario;
            objData.NombreDestinatario = "Walterio";
            objData.SendGridAPIKey = _configuration["SendGrid:ApiKey"];

          var resultado =  await _sendGridEnviar.EnviarEmail(objData);
          if (resultado.resultado)
          {
              await Task.CompletedTask;
              return;
          }

           
        }
    }
}
