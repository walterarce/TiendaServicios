using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Persistencia
{
    public class ContextoLibro: DbContext
    {
        public ContextoLibro()
        {
            //se agrega porque tiene el test la necesidad que se agregue
        }
        public ContextoLibro(DbContextOptions<ContextoLibro> options) : base(options)
        {
            
        }

        public virtual DbSet<LibreriaMaterial> LibreriaMaterial { get; set; }
    }
}
