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


        [HttpGet ("Get-Usuarios")]
        public List<Usuario> GetUsuarios()
        {
            return service.Getusuarios();
        }

        [HttpPost ("Set-Usuario")]
        public string SetUsuario(Usuario model)
        {
            return service.SetUsuario(model);
        }

        [HttpPut ("Update-Usuario")]
        public string UpdateUsuario(Usuario model)
        {
            return service.UpdateUsuario(model);
        }

        [HttpDelete ("Delete-Usuario")]
        public string DeleteUsuario(int id)
        {
            return service.DeleteUsuario(id);
        }
    }
}
