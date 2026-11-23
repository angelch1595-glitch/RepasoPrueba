using Microsoft.AspNetCore.Identity;
using BackendComedor.Services.Interfaces;
using BackendComedor.Models;
namespace BackendComedor.Services
{
    public class RolServicio : IRolServicio
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolServicio(
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IdentityResult> CrearRolAsync(string nombre)
        {
            var rol = new IdentityRole(nombre.Trim());
            return await _roleManager.CreateAsync(rol);
        }
        public async Task<IdentityResult> AsignarRolAsync(string usuarioId, string nombreRol)
        {
            var usuario = await _userManager.FindByIdAsync(usuarioId);

            if (usuario == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UsuarioNoEncontrado",
                    Description = "El usuario no existe."
                });
            }

            var rol = await _roleManager.FindByNameAsync(nombreRol.Trim());

            if (rol?.Name == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "RolNoEncontrado",
                    Description = "El rol no existe."
                });
            }

            return await _userManager.AddToRoleAsync(usuario, rol.Name);
        }
        }
    }

