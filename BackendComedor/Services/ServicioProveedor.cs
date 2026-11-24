
using BackendComedor.Models;
using BackendComedor.Repositories.Interfaces;
using BackendComedor.Services.Interfaces;
namespace BackendComedor.Services
{
    public class ServicioProveedor: IServicioProveedor
    {
        private readonly IProveedor _repo;
        public ServicioProveedor(IProveedor repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<Proveedor>> ObtenerProveedoresAsync() {
            return await _repo.GetAllAsync();
        }
        public async Task<Proveedor?> ObtenerProveedorAsync(int id) { 
            return await _repo.GetByIdAsync(id);
        }


        public async Task<Proveedor> CrearProveedorAsync(Proveedor proveedor) { 
            return await _repo.AddAsync(proveedor);
        }
    }
}
