using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Tests
{
    public class MappingTest:Profile
    {
        public MappingTest()
        {
            CreateMap<LibreriaMaterial, LibroMaterialDTO>();
        }
    }
}
