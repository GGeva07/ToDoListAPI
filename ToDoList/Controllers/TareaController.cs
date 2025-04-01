using Microsoft.AspNetCore.Mvc;
using ToDoList.Interfaces;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly ITarea service;

        public TareaController(ITarea service)
        {
            this.service = service;
        }

        [HttpGet("Get-Tareas")]
        public async Task<IActionResult> GetTareas()
        {
            try
            {
                var tareas = await service.Get();
                return tareas.Any() ? Ok(tareas) : NotFound("No se encontraron tareas");
            }catch(Exception e)
            {
                return StatusCode(500, $"Error obteniendo la lista de tareas {e.Message}");
            }

        }

        [HttpGet("Get-Tareas-by-Usuario/{idUsuario}")]
        public async Task<IActionResult> GetTareasByIdUsuario(int idUsuario)
        {
            try
            {
                var tareas = await service.GetTareasByIdUsuario(idUsuario);
                return tareas.Any() ? Ok(tareas) : NotFound($"No se encontraron tareas para el usuario con id o el usuario fue eliminado {idUsuario}");
            }catch(Exception e)
            {
                return StatusCode(500, $"Error obteniendo las tareas por el id Usuario {e.Message}");
            }
            
        }

        [HttpGet("Get-TareaByTitle/{Nombre}")]
        public async Task<IActionResult> GetTareaByNombre(string Nombre)
        {
            try
            {
                var tareas = await service.GetTareasByNombre(Nombre);
                return tareas.Any() ? Ok(tareas) : NotFound($"No se encontraron tareas con el titulo: {Nombre}.");
            }catch(Exception e)
            {
                return StatusCode(500, $"Error obteniedo las tareas por su nombre {e.Message}");
            }
        }

        [HttpPost("Set-Tarea")]
        public async Task<IActionResult> PostTarea(int idUsuario, [FromBody] Tarea model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Los datos de la tarea no son validos");
                }

                var resultado = await service.Post(idUsuario, model);
                return resultado == "Tarea creada" ? Ok(resultado) : BadRequest(resultado);
            }catch(Exception e)
            {
                return StatusCode(500, $"Error creando la tarea {e.Message}");
            }

        }

        [HttpPut("Update-Tarea/{id}/{idUsuario}")]
        public async Task<IActionResult> PutTarea(int id, int idUsuario, [FromBody] Tarea model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Los datos de la tarea no son validos");
                }

                var resultado = await service.Put(id, idUsuario, model);
                return resultado == "Tarea editada" ? Ok(resultado) : NotFound(resultado);
            }catch(Exception e)
            {
                return StatusCode(500, $"Error editando la tarea {e.Message}");
            }

        }

        [HttpDelete("Delete-Tarea/{id}/{idUsuario}")]
        public async Task<IActionResult> DeleteTarea(int id, int idUsuario)
        {
            try
            {
                var resultado = await service.Delete(id, idUsuario);
                return resultado == "Tarea eliminada" ? Ok(resultado) : NotFound(resultado);
            }catch (Exception e)
            {
                return StatusCode(500, $"Error borrando la tarea {e.Message}");
            }

        }
    }

}
