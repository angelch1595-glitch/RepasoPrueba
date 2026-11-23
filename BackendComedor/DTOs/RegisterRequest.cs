namespace BackendComedor.DTOs
{
    public record RegisterRequest(string Usuario, string Email, string Password, string NombreCompleto);
}
