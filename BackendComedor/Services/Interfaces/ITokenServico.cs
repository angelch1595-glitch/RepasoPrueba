using BackendComedor.Models;

namespace BackendComedor.Services.Interfaces
{
    public interface ITokenServico
    {
        (string Token, DateTime Expira) Generar(ApplicationUser usuario, IList<string> roles);
    }
}
