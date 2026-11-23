using BackendComedor.DTOs;
namespace BackendComedor.Services.Interfaces
{
    public interface IAuthServicio
    {
        Task<(LoginRespuesta? Respuesta, string? Error)> LoginAsync(LoginRequest request);
    }
}
