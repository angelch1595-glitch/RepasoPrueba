
using BackendComedor.Data;
using BackendComedor.DTOs;
using BackendComedor.Models;
using BackendComedor.Repositories.Interfaces;
using BackendComedor.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace BackendComedor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProveedorController:ControllerBase
    {

        private readonly IServicioProveedor _servicio;

        public ProveedorController(IServicioProveedor servicio)
        {
            _servicio = servicio;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> ObtenerProveedor(int id) { 
            var encontrado=await _servicio.ObtenerProveedorAsync(id);
            if (encontrado == null)
            {
                return NotFound("Proveedor no encontrado");
            }
            return Ok(encontrado);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Proveedor>> CrearProveedor([FromBody] CrearProveedor dto)
        {
            var Proveedor = new Proveedor
            {
                Nombre = dto.Nombre,
                Telefono = dto.Telefono,
                Activo = dto.Activo,
            };
           await  _servicio.CrearProveedorAsync(Proveedor);
            return CreatedAtAction(nameof(ObtenerProveedor), new { id = Proveedor.Id }, Proveedor);
        }
    }
}
