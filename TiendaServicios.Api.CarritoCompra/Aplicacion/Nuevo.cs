using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TiendaServicios.Api.CarritoCompra.Modelo;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime FechaCreacionSession { get; set; }
            public List<string> ProductoLista { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly ContextoCarrito _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoCarrito contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritosession = new CarritoSession() {FechaCreacion = request.FechaCreacionSession };

                _contexto.CarritoSession.Add(carritosession);

                var value = await _contexto.SaveChangesAsync();

                if (value == 0)
                {
                    throw new Exception("Error en insersion de carrito de compras");
                }
                int id = carritosession.CarritoSessionId;
                foreach (var producto in request.ProductoLista)
                {
                    var detallesession = new CarritoSessionDetalle() {FechaCreacion = DateTime.Now, CarritoSessionId = id,ProductoSeleccionado = producto};
                    _contexto.CarritoSessionDetalle.Add(detallesession);
                }

                value = await _contexto.SaveChangesAsync();
                if (value >0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el detalle del carrito");
            }
        }

    }
}
