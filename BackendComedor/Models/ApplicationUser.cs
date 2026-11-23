using Microsoft.AspNetCore.Identity;
namespace BackendComedor.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string NombreCompleto { get; set; } = string.Empty;
    }
}
