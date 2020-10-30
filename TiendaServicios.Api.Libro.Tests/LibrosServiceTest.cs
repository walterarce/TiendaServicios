using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider)
                .Returns(new AsyncQueryProvider<LibreriaMaterial>(DataPrueba.Provider)); //necesario pra hacer filtros por parametro
            var contexto = new Mock<ContextoLibro>();

            contexto.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);

            return contexto;
        }

        [Fact]
        public async void GetLibroPorId()
        {
            //Arrange
            var mockContexto = CrearContexto();
            var mapConfig = new MapperConfiguration(cfg=>cfg.AddProfile(new MappingTest()));
            var mapper = mapConfig.CreateMapper();
            var request = new ConsultaFiltro.LibroUnico();
            request.LibroId = Guid.Empty;

            //Act
            var manejador = new ConsultaFiltro.Manejador(mockContexto.Object, mapper);

            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(libro);
            Assert.True(libro.LibreriaMaterialId==Guid.Empty);
        }
        [Fact]
        public async void GetLibros()
        {
            //Arrange
          
            //1 que metodo dentro de la microservice se encarga de realizar la consulta de libros
            //Emular instancia de entity framework core
            //para emular acciones y eventos de un objeto en un ambiente test
            //utilizamos objetos del tipo mock (representa a cualquier elemento de codigo)
            var mockContexto = CrearContexto(); // new Mock<ContextoLibro>();
            //2 se necesita emular al mapping Imapper

            var mapConfig =new MapperConfiguration(cfg => cfg.AddProfile(new MappingTest())); //new Mock<IMapper>();

            var mapper = mapConfig.CreateMapper();

            //3 se debe instanciar la clase manejador y ponerle como parametro los mock creados
            //ACT
            Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object, mapper);
            Consulta.Ejecuta request = new Consulta.Ejecuta();
            var lista =await manejador.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.True(lista.Any());

        }
        [Fact]
        public async void GuardarLibro()
        {
            System.Diagnostics.Debugger.Launch();
            //Arrange
            var options = new DbContextOptionsBuilder<ContextoLibro>()
                .UseInMemoryDatabase(databaseName:"BaseDatosLibro")
                .Options;

            var contexto=new ContextoLibro(options);
            var request = new Nuevo.Ejecuta();
            request.Titulo = "Libro de Microservice";
            request.AutorLibro= Guid.Empty;
            request.FechaPublicacion = DateTime.Now;

            //Act
            var manejador = new Nuevo.Manejador(contexto);
            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());

            //Assert
           Assert.True(libro!=null);
        }
    }
}
