using BackendComedor.Data;
using BackendComedor.Models;
using BackendComedor.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace BackendComedor.Repositories
{
    public class ProveedorRepository:IProveedor
    {
        private readonly ComedorDbContext _context;
        public ProveedorRepository(ComedorDbContext context )
        {
            _context = context;
        }

        public async Task<IEnumerable<Proveedor>> GetAllAsync() { 
            return await _context.Proveeedor.AsNoTracking().ToListAsync();
        }
        public async Task<Proveedor?> GetByIdAsync(int id) {
            return await _context.Proveeedor.AsNoTracking().FirstOrDefaultAsync(pro => pro.Id ==id);
        }
        public async Task<Proveedor> AddAsync(Proveedor proveedor) { 
            _context.Proveeedor.Add(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }
    }
}
