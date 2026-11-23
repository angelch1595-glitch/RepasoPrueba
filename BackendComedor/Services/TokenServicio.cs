using BackendComedor.Services.Interfaces;
using BackendComedor.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendComedor.Services
{
    public class TokenServicio:ITokenServico
    {
        private readonly IConfiguration _config;
        public TokenServicio(IConfiguration config          )
        {
            _config = config;
        }

        public (string Token, DateTime Expira) Generar(ApplicationUser usuario, IList<string> roles) {
            var claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub,usuario.Id),
            new Claim(ClaimTypes.Name,usuario.UserName??string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach(var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }
            var clave = _config["Jwt:Key"] ?? throw new InvalidOperationException("Falta configurar Jwt:Key.");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clave));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expira = DateTime.UtcNow.AddHours(1);
            // Construir el token
            var token=new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],

                audience: _config["Jwt:Audience"],
                claims:claims,
                expires:expira,
                signingCredentials:credenciales
                );

            return (new JwtSecurityTokenHandler().WriteToken(token), expira);
        }

    }
}
