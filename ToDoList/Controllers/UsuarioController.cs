using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Interfaces;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario service;

        public UsuarioController(IUsuario service)
        {
            this.service = service;
        }

        [HttpGet("Get-Usuarios")]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            try
            {
                var usuarios = await service.Get();
                if (usuarios == null || !usuarios.Any())
                {
                    return NotFound("No se encontraron usuarios");
                }

                return Ok(usuarios);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error interno del servidor: {e.Message}");
            }
        }

        [HttpPost("Post-Usuario")]
        public async Task<ActionResult<string>> PostUsuario([FromBody] Usuario model)
        {
            try
            {
                var result = await service.Post(model);

                if(result == null)
                {
                    return BadRequest("El usuario ya existe ingresa otro usuario");
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error al insertar usuario: {e.Message}");
            }
        }

        [HttpPut("Put-Usuario/{id}")]
        public async Task<ActionResult<string>> PutUsuario(int id, [FromBody] Usuario model)
        {
            try
            {
                var result = await service.Put(id, model);
                if (result.Contains("no encontrado"))
                {
                    return NotFound(result); 
                }
                return Ok(result); 
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error al actualizar usuario: {e.Message}");
            }
        }

        [HttpDelete("Delete-Usuario/{id}")]
        public async Task<ActionResult<string>> DeleteUsuario(int id)
        {
            try
            {
                var result = await service.Delete(id);
                if (result.Contains("no encontrado"))
                {
                    return NotFound(result); 
                }
                return Ok(result); 
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Error al eliminar usuario: {e.Message}"); 
            }
        }

        [HttpGet("GetUsuarioById{id}")]

        public async Task<ActionResult<string>> GetUsuarioById(int id)
        {
            try
            {
                var result = await service.GetUsuarioById(id);
                if(result == null)
                {
                    return NotFound(result);
                }
                return Ok(result);
            }catch(Exception e)
            {
                return StatusCode(500, $"Error al encontrar el usuario por el id: {e.Message}");
            }
        }
    }
}