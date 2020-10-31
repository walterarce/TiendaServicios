﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.Api.Libro.Aplicacion;

namespace TiendaServicios.Api.Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroMaterialController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LibroMaterialController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {//me mando un comentario en vano
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<LibroMaterialDTO>>> GetLibros()
        {
            return await _mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroMaterialDTO>> GetLibroUnico(Guid id)
        {
            return await _mediator.Send(new ConsultaFiltro.LibroUnico() {LibroId = id});
        }

    }
}
