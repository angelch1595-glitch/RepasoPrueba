using Microsoft.EntityFrameworkCore;
using BackendComedor.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace BackendComedor.Data
{
    public class ComedorDbContext : IdentityDbContext<ApplicationUser>
    {
        public ComedorDbContext(DbContextOptions<ComedorDbContext> options) : base(options) { } // Para q pueda hablar con la bd
        public DbSet<Producto> Producto => Set<Producto>(); //Se crea la tabla producto
        public DbSet<Proveedor> Proveeedor=> Set<Proveedor>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder); // siempre 1ro
            modelBuilder.Entity<Producto>(
                Entidad => {
                    Entidad.Property(p => p.Nombre).HasMaxLength(100);
                    Entidad.Property(p => p.Precio).HasPrecision(18, 2);
                }
                );
            modelBuilder.Entity<Proveedor>(
                    Entidad => {
                        Entidad.Property(pro => pro.Nombre).HasMaxLength(40);
                        Entidad.Property(pro => pro.Telefono).HasMaxLength(15);
                    }
                );
        }


    }
}
