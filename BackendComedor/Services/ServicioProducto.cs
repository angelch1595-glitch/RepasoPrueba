using BackendComedor.Models;
using BackendComedor.Repositories.Interfaces;
using BackendComedor.Services.Interfaces;
namespace BackendComedor.Services.Interfaces
{
    public class ServicioProducto:IServicioProducto
    {
        private readonly IProducto _repo;
        public ServicioProducto(IProducto repo  )
        {
            _repo = repo;
        }
        public  Task<List<Producto>> ObtenerTodos() {
            return _repo.ObtenerTodosAsync();
        }
        public Task<Producto?> ObtenerProductoPorId(int id) {
            return  _repo.ObtenerProductoPorIdAsync(id);   
        }
        public  Task<Producto> AgregarProducto(Producto producto)
        {
            return _repo.AgregarProductoAsync(producto);
        }
        public  Task<bool> ActualizarProducto(Producto producto) {

            return  _repo.ActualizarProductoAsync(producto);
        }
        public  Task<bool> EliminarProducto(int id)
        {
            return  _repo.EliminarProductoAsync(id);
        }
    }
}
