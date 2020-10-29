using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.CarritoCompra.Modelo;

namespace TiendaServicios.Api.CarritoCompra.Persistencia
{
    public class ContextoCarrito: DbContext
    {
        public ContextoCarrito(DbContextOptions<ContextoCarrito> options) : base(options)
        {
            
        }

        public DbSet<CarritoSession> CarritoSession { get; set; }
        public DbSet<CarritoSessionDetalle> CarritoSessionDetalle { get; set; }
    }
}
