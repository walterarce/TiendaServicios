﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class ConsultaFiltro
    {
        public class AutorUnico : IRequest<AutorDto>
        {
            public string AutorGuid { get; set; }

        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDto>
        {
            private readonly ContextoAutor _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoAutor contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }
            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autorencontrado = await _contexto.AutorLibros.Where(x => x.AutorLibroGuid == request.AutorGuid).FirstAsync();

                
                if (autorencontrado == null)
                {throw new Exception("No se encontro el autor");}

                var autorencontradoDTO = _mapper.Map<AutorLibro, AutorDto >(autorencontrado);
                return autorencontradoDTO;
            }
        }
    }
}
