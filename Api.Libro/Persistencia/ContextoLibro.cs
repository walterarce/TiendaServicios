﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TiendaServicios.Api.Libro.Persistencia
{
    public class ContextoLibro:DbContext
    {
        public ContextoLibro(DbContextOptions<ContextoLibro> options) : base(options)
        {
            
        }
    }
}
