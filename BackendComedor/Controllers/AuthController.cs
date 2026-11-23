using BackendComedor.Models;
using BackendComedor.Services.Interfaces;
using BackendComedor.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace BackendComedor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServicio _authservicio;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(IAuthServicio authservicio, UserManager<ApplicationUser> userManager)
        {
            _authservicio = authservicio;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var (respuesta, error) = await _authservicio.LoginAsync(request);
            if (respuesta != null)
            {
                return Ok(respuesta);
            }
            return Unauthorized(new { mensaje = error });
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Usuario,
                Email = request.Email,
                NombreCompleto = request.NombreCompleto
            };

            var resultado = await _userManager.CreateAsync(user, request.Password);
            if (!resultado.Succeeded)
            {
                return BadRequest(new
                {
                    errores = resultado.Errors.Select(e => e.Description)
                });
            }
            // El rol lo decide el servidor, nunca el cliente 
            await _userManager.AddToRoleAsync(user, "Usuario");
            return Ok(new
            {
                mensaje = "Usuario registrado correctamente",
                usuario = user.UserName
            });

        }
    }
}
