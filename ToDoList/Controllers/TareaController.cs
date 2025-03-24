using Microsoft.AspNetCore.Mvc;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController
    {
        private readonly ITareas service;

        public TareaController(ITareas service)
        {
            this.service = service;
        }
        [HttpGet("Get-Tareas")]
        public List<Tareas> GetTareas()
        {
            return service.Get();
        }

        [HttpGet("Get-TareaByTitle{Nombre}")]
        public Tareas? GetTareaByNombre(string Nombre)
        {
            return service.GetTareasByNombre(Nombre);
        }

        [HttpPost("Set-Tarea")]
        public string SetTarea(Tareas model)
        {
            return service.Set(model);
        }

        [HttpPut("Update-Tarea")]
        public string UpdateTareas(Tareas model)
        {
            return service.Update(model);
        }

        [HttpDelete("{id}")]
        public string DeleteTarea(int id)
        {
            return service.Delete(id);
        }
    }
}
