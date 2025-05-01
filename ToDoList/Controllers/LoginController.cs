using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogin service;
        private readonly IUsuario serviceUser;

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

        [HttpPost("Sing-In")]
        public async Task<IActionResult> SingIn([FromBody] Login login)
        {
            var usuario = await service.RegistrarUsuario(login.usuarioNombre, login.Correo, login.Contrasenia);

            if (usuario == null)
                return Unauthorized(new { message = "Credenciales incorrectas o en uso" });

            return Ok(new
            {
                message = "Usuario registrado exitoso",
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
