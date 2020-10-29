using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.CarritoCompra.Modelo
{
    public class CarritoSessionDetalle
    {

        public int CarritoSessionDetalleId { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public string ProductoSeleccionado { get; set; }

        public int CarritoSessionId { get; set; }

        public CarritoSession CarritoSession { get; set; }
    }
}
