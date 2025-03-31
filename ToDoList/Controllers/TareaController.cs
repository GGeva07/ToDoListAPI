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
        private readonly ITareas service;

        public TareaController(ITareas service)
        {
            this.service = service;
        }

        [HttpGet("Get-Tareas")]
        public async Task<IActionResult> GetTareas()
        {
            var tareas = await service.Get();
            return tareas.Any() ? Ok(tareas) : NotFound("No se encontraron tareas");
        }

        [HttpGet("Get-Tareas-by-Usuario/{idUsuario}")]
        public async Task<IActionResult> GetTareasByIdUsuario(int idUsuario)
        {
            var tareas = await service.GetTareasByIdUsuario(idUsuario);
            return tareas.Any() ? Ok(tareas) : NotFound($"No se encontraron tareas para el usuario con id {idUsuario}");
        }

        [HttpGet("Get-TareaByTitle/{Nombre}")]
        public async Task<IActionResult> GetTareaByNombre(string Nombre)
        {
            var tareas = await service.GetTareasByNombre(Nombre);
            return tareas.Any() ? Ok(tareas) : NotFound($"No se encontraron tareas con el titulo: {Nombre}.");
        }

        [HttpPost("Set-Tarea")]
        public async Task<IActionResult> PostTarea(int idUsuario, [FromBody] Tareas model)
        {
            if (model == null)
            {
                return BadRequest("Los datos de la tarea no son validos");
            }

            var resultado = await service.Post(idUsuario, model);
            return resultado == "Tarea creada" ? Ok(resultado) : BadRequest(resultado);
        }

        [HttpPut("Update-Tarea/{id}/{idUsuario}")]
        public async Task<IActionResult> PutTarea(int id, int idUsuario, [FromBody] Tareas model)
        {
            if (model == null)
            {
                return BadRequest("Los datos de la tarea no son validos");
            }

            var resultado = await service.Put(id, idUsuario, model);
            return resultado == "Tarea editada" ? Ok(resultado) : NotFound(resultado);
        }

        [HttpDelete("Delete-Tarea/{id}/{idUsuario}")]
        public async Task<IActionResult> DeleteTarea(int id, int idUsuario)
        {
            var resultado = await service.Delete(id, idUsuario);
            return resultado == "Tarea eliminada" ? Ok(resultado) : NotFound(resultado);
        }
    }

}
