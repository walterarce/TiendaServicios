﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class LibroMaterialDTO
    {
        public Guid? LibreriaMaterialId { get; set; }

        public string titulo { get; set; }

        public DateTime? FechaPublicacion { get; set; }

        public Guid? AutorLibro { get; set; }
    }
}
