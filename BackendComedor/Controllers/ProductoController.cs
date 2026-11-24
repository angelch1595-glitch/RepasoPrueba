using BackendComedor.Services.Interfaces;
using BackendComedor.Models;
using BackendComedor.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
namespace BackendComedor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductoController:ControllerBase
    {
        private readonly IServicioProducto _servicioProducto;
        public ProductoController(IServicioProducto servicioProducto) { 
            _servicioProducto = servicioProducto;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> ObtenerTodos() {
            var productos=await _servicioProducto.ObtenerTodos();
            return Ok(productos);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Producto?>> ObtenerProductoPorId(int id) {
            var productoEncontrado = await _servicioProducto.ObtenerProductoPorId(id);
            if (productoEncontrado == null)
            {
                return NotFound();
            }
            return Ok(productoEncontrado);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Producto>> AgregarProducto([FromBody] ProductoCreado dto) {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Precio = dto.precio,
                Activo = dto.activo
            };
            await _servicioProducto.AgregarProducto(producto);
            return CreatedAtAction(nameof(ObtenerProductoPorId), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<bool>> ActualizarProducto(int id ,[FromBody] ProductoActualizado dto) {
            var producto = new Producto
            {
                Id = id ,
                Nombre = dto.Nombre,
                Precio = dto.precio,
                Activo = dto.activo
            };

            bool actualizado = await _servicioProducto.ActualizarProducto(producto);
            if (!actualizado) {
                return NotFound();
            }
            return NoContent();
            
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> EliminarProducto(int id) {
            bool eliminado = await _servicioProducto.EliminarProducto(id);
            if (!eliminado) {
                return NotFound();
            }
            return NoContent();
        }
    }
}
