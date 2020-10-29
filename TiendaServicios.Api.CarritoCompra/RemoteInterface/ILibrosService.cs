using System;
using System.Threading.Tasks;


namespace TiendaServicios.Api.CarritoCompra
{
    public interface ILibrosService
    {
        Task <(bool resultado, LibroRemote libro , string ErrorMessage)> GetLibro(Guid LibroId);

    }
}