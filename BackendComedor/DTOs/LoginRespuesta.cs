namespace BackendComedor.DTOs;
    public record LoginRespuesta(string Token, DateTime Expira, string Usuario, IList<string> Roles);