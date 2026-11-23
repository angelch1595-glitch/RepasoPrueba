using BackendComedor.DTOs;
using BackendComedor.Models;
using BackendComedor.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
namespace BackendComedor.Services
{
    public class AuthServicio:IAuthServicio
    {
        private readonly ITokenServico _tokenServico;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthServicio(ITokenServico tokenServico , UserManager<ApplicationUser> userManager) { 
            _tokenServico= tokenServico;
            _userManager= userManager;
        }

        public async Task<(LoginRespuesta? Respuesta, string? Error)> LoginAsync(LoginRequest request) {
            var usuario = await _userManager.FindByNameAsync(request.Usuario) ?? await _userManager.FindByEmailAsync(request.Usuario);
            if (usuario == null)
            {
                return (null, "Usuario o contraseña incorrectos");
            }
            if (await _userManager.IsLockedOutAsync(usuario))
            {
                return (null, "Cuenta bloqueada temporalmente. Intenta más tarde.");
            }

            if (!await _userManager.CheckPasswordAsync(usuario, request.Password))
            {
                await _userManager.AccessFailedAsync(usuario);
                return (null, "Usuario o contraseña incorrectos");
            }

            await _userManager.ResetAccessFailedCountAsync(usuario);
            var roles=await _userManager.GetRolesAsync(usuario);
            var (token, expira) = _tokenServico.Generar(usuario, roles);
            var respuesta = new LoginRespuesta(
                token,
                expira,
                usuario.UserName ?? string.Empty,
                roles
                );
            return (respuesta, null);
        }

    }
}
