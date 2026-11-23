using BackendComedor.Models;

namespace BackendComedor.Services.Interfaces
{
    public interface IServicioProducto
    {
        Task<List<Producto>> ObtenerTodos();
        Task<Producto?> ObtenerProductoPorId(int id);
        Task<Producto> AgregarProducto(Producto producto);
        Task<bool> ActualizarProducto(Producto producto);
        Task<bool> EliminarProducto(int id);
    }
}
