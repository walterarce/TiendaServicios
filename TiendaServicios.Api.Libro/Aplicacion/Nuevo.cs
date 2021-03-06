﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.EventoQueue;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }

            public Guid? AutorLibro { get; set; }

        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
                RuleFor(x => x.AutorLibro).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoLibro _contexto;
            private readonly IRabbitEventBus _rabbitEventBus;
            public Manejador(ContextoLibro contexto, IRabbitEventBus rabbitEventBus)
            {
                _contexto = contexto;
                _rabbitEventBus = rabbitEventBus;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var Libronuevo = new LibreriaMaterial()
                {   titulo = request.Titulo,
                    FechaPublicacion = request.FechaPublicacion,
                    AutorLibro = request.AutorLibro
                };
                
                 _contexto.LibreriaMaterial.Add(Libronuevo);
                 var value = await _contexto.SaveChangesAsync();

                _rabbitEventBus.Publish(new EmailEventoQueue("walterarce@gmail.com", request.Titulo, "esto es un ejemplo"));

                if (value >0)
                {
                    return Unit.Value;
                }
                
                throw new Exception("No se pudo insertar");
            }
        }
    }
}
