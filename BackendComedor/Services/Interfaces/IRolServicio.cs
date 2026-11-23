
using Microsoft.AspNetCore.Identity;
    namespace BackendComedor.Services.Interfaces
{
    public interface IRolServicio
    {
        Task<IdentityResult> CrearRolAsync(string nombreRol);
        Task<IdentityResult> AsignarRolAsync(string idUsuario,string nombreRol);
    }
}
