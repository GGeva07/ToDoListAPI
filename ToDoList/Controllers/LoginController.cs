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
            var usuario = await service.ValidarUsuario(login.Email , login.Pass);
            
            if(usuario == null)
                return Unauthorized(new { message = "Credenciales incorrectas" });

            var token = service.GenerateJwtToken(usuario);
            return Ok(new { token });
        }
    }
}
