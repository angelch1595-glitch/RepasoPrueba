using BackendComedor.Models;

namespace BackendComedor.Repositories.Interfaces
{
    public interface IProveedor
    {
        Task<IEnumerable<Proveedor>> GetAllAsync();
        Task<Proveedor?> GetByIdAsync(int id);
        Task<Proveedor> AddAsync(Proveedor proveedor);
    }
}
