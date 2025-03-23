using Microsoft.AspNetCore.Mvc;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController
    {
        private readonly ITarea service;

        public TareaController(ITarea service)
        {
            this.service = service;
        }
        [HttpGet("Get-Tareas")]
        public List<Tareas> GetTareas()
        {
            return service.GetTarea();
        }

        [HttpPost("Set-Tarea")]
        public string SetTarea(Tareas model)
        {
            return service.SetTarea(model);
        }

        [HttpPut("Update-Tarea")]
        public string UpdateUsuario(Tareas model)
        {
            return service.UpdateTarea(model);
        }

        [HttpDelete("Delete-Tarea")]
        public string DeleteTarea(int id)
        {
            return service.DeleteTarea(id);
        }
    }
}
