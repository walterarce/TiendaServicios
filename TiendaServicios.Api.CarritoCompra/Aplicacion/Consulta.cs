using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoDto>
        {
            public  int CarritoSessionId { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {

            private readonly ContextoCarrito _contexto;

            private readonly ILibrosService _libroService;
            public Manejador(ContextoCarrito contexto, ILibrosService libroservice)
            {

                _contexto = contexto;
                _libroService = libroservice;
            }
            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritosession = 
                    await _contexto.CarritoSession.FirstOrDefaultAsync(x => x.CarritoSessionId == request.CarritoSessionId);

                var carritosessiondetalle =
                    await _contexto.CarritoSessionDetalle.Where(x => x.CarritoSessionId == request.CarritoSessionId).ToListAsync();

                var listacarritodto = new List<CarritoDetalleDTO>();
                foreach (var libro in carritosessiondetalle)
                {
                  var response = await _libroService.GetLibro(new Guid(libro.ProductoSeleccionado));

                  if (response.resultado)
                  {
                      var objetolibro = response.libro;
                      var carritodetalle = new CarritoDetalleDTO()
                      {
                          TituloLibro = objetolibro.titulo,
                          FechaPublicacion = objetolibro.FechaPublicacion,
                          LibroId =  objetolibro.LibreriaMaterialId
                      };
                      listacarritodto.Add(carritodetalle);
                  }
                }
                var carritoSessionDto = new CarritoDto()
                {
                    CarritoId = carritosession.CarritoSessionId,
                    FechaCreacionSession = carritosession.FechaCreacion,
                    ListaProductos = listacarritodto
                };

                return carritoSessionDto;
            }
        }
    }
}
