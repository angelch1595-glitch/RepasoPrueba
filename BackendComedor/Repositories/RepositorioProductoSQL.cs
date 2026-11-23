using BackendComedor.Repositories.Interfaces;
using BackendComedor.Data;
using Microsoft.EntityFrameworkCore;
using BackendComedor.Models;
namespace BackendComedor.Repositories
{
    public class RepositorioProductoSQL:IProducto
    {
        private readonly ComedorDbContext _context;
        public RepositorioProductoSQL(ComedorDbContext context) {
        _context = context;
        }

        public async Task<List<Producto>> ObtenerTodosAsync() { 
            return await _context.Producto.AsNoTracking().ToListAsync();        
        
        }
        public async Task<Producto?> ObtenerProductoPorIdAsync(int id)
        {
            return await _context.Producto.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        }
        public async Task<Producto> AgregarProductoAsync(Producto producto) { 
            _context.Producto.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }
        public async Task<bool> ActualizarProductoAsync(Producto producto)
        {
            var encontrarProducto =await _context.Producto.FindAsync(producto.Id);
            if (encontrarProducto == null) {
                return false;
            }
            encontrarProducto.Nombre = producto.Nombre;
            encontrarProducto.Precio=producto.Precio;
            encontrarProducto.Activo= producto.Activo;
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<bool> EliminarProductoAsync(int id) { 
            var encontrado=await _context.Producto.FindAsync(id);
            if (encontrado == null) {
                return false;    
            }
            _context.Remove(encontrado);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
