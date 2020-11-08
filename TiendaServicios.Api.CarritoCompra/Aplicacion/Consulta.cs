using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarritoCompra.Modelo;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Consulta
    {
        public class ListaCarrito : IRequest<List<CarritoDtoWithoutList>>
        {
            
        }
        public class Manejador : IRequestHandler<ListaCarrito, List<CarritoDtoWithoutList>>
        {
            private readonly ContextoCarrito _contexto;

            private readonly IMapper _mapper;
            public Manejador(ContextoCarrito contexto , IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }
          

            public async Task<List<CarritoDtoWithoutList>> Handle(ListaCarrito request, CancellationToken cancellationToken)
            {
                var carritos = await _contexto.CarritoSession.ToListAsync();
                var carritosDto = _mapper.Map<List<CarritoSession>, List<CarritoDtoWithoutList>>(carritos);

                return carritosDto;
            }
        }
    }
}
