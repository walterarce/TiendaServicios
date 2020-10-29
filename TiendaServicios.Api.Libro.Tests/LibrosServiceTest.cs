using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Moq;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Persistencia;
using GenFu;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Libro.Modelo;
using Xunit;
namespace TiendaServicios.Api.Libro.Tests
{
    public class LibrosServiceTest
    {
        private IEnumerable<LibreriaMaterial> ObtenerDataPrueba()
        {
            A.Configure<LibreriaMaterial>()
                .Fill(x => x.titulo).AsArticleTitle()
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<LibreriaMaterial>(30);

            lista[0].LibreriaMaterialId = Guid.Empty;

            return lista;
        }

        private Mock<ContextoLibro> CrearContexto()
        {
            var DataPrueba = ObtenerDataPrueba().AsQueryable();

            var dbSet = new Mock<DbSet<LibreriaMaterial>>();

            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(DataPrueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(DataPrueba.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(DataPrueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(DataPrueba.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>()
                .Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<LibreriaMaterial>(DataPrueba.GetEnumerator()));
            var contexto = new Mock<ContextoLibro>();

            contexto.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);

            return contexto;
        }
        [Fact]
        public async void GetLibros()
        {
            //1 que metodo dentro de la microservice se encarga de realizar la consulta de libros
            //Emular instancia de entity framework core
            //para emular acciones y eventos de un objeto en un ambiente test
            //utilizamos objetos del tipo mock (representa a cualquier elemento de codigo)
            var mockContexto = CrearContexto(); // new Mock<ContextoLibro>();

            //2 se necesita emular al mapping Imapper

            var mapConfig =new MapperConfiguration(cfg => cfg.AddProfile(new MappingTest())); //new Mock<IMapper>();

            var mapper = mapConfig.CreateMapper();

            //3 se debe instanciar la clase manejador y ponerle como parametro los mock creados

            Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object, mapper);

            Consulta.Ejecuta request = new Consulta.Ejecuta();

            var lista =await manejador.Handle(request, new System.Threading.CancellationToken());
        }

    }
}
