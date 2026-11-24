using BackendComedor.Models;

namespace BackendComedor.Services.Interfaces
{
    public interface IServicioProveedor
    {
        Task<IEnumerable<Proveedor>> ObtenerProveedoresAsync();
        Task<Proveedor?> ObtenerProveedorAsync(int id);
        Task<Proveedor> CrearProveedorAsync(Proveedor proveedor);
    }
}
