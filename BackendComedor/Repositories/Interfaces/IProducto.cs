using BackendComedor.Models;

namespace BackendComedor.Repositories.Interfaces
{
    public interface IProducto
    {
        Task<List<Producto>> ObtenerTodosAsync();
        Task<Producto?> ObtenerProductoPorIdAsync(int id);
        Task<Producto> AgregarProductoAsync(Producto producto);
        Task<bool> ActualizarProductoAsync(Producto producto);
        Task<bool> EliminarProductoAsync(int id);
    }
}
