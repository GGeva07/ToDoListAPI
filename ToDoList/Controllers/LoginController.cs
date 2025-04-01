using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogin service;

        public LoginController(ILogin service)
        {
            this.service = service;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var usuario = await service.ValidarUsuario(login.Correo , login.Contrasenia);
            
            if(usuario == null)
                return Unauthorized(new { message = "Credenciales incorrectas" });

            return  Ok(new
            {
                message = "Login exitoso",
                Usuario = new
                {
                    id = usuario.id,
                    usuarioNombre = usuario.usuarioNombre,
                    correo = usuario.correo
                }
            });
        }

        

    }
}
