using Microsoft.AspNetCore.Identity;
using BackendComedor.Models;
using BackendComedor.DTOs;
using BackendComedor.Services;
using Microsoft.AspNetCore.Mvc;
using BackendComedor.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
namespace BackendComedor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles ="Admin")]
    public class RolController:ControllerBase
    {
        private readonly IRolServicio _rolServicio;

        public RolController(IRolServicio rolServicio) {
            _rolServicio= rolServicio;
        }

        [HttpPost]
        public async Task<ActionResult<IdentityResult>> crearRol([FromBody]CrearRol dto) {
            var resultado = await _rolServicio.CrearRolAsync(dto.nombre);
            if (!resultado.Succeeded) { return BadRequest(new { errors = resultado.Errors.Select(error => error.Description) }); }
            return StatusCode(201, new{ mensaje = "Rol creado correctamente",nombre = dto.nombre.Trim() });  }

        [HttpPost("~/api/usuarios/{id}/roles")]
        public async Task<ActionResult<IdentityResult>> asignarRol(string idUsuario, AsignarRol dto) {
            var resultado = await _rolServicio.AsignarRolAsync(idUsuario, dto.nombre);
            if (!resultado.Succeeded) {
                var respuesta = new { errors = resultado.Errors.Select(error => error.Description) };
                if (resultado.Errors.Any(error => error.Code == "UsuarioNoEncontrado" || error.Code == " Rol no encontrado"))
                {
                    return NotFound(respuesta);
                }
                return BadRequest(respuesta);
            }
            return NoContent();
        }
    }
}
