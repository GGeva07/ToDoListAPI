using ToDoList.Context;
using ToDoList.Interfaces;
using ToDoList.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace ToDoList.Services
{
    public class LoginService : ILogin
    {
        private readonly TodoListDBContext context;
        private readonly IConfiguration configuracion;

        public LoginService(TodoListDBContext context, IConfiguration config)
        {
            this.context = context;
            this.configuracion = config;
        }

        public async Task<Usuario> ValidarUsuario(string email, string pass)
        {
            try
            {
                var usuario = await context.Usuario
                    .FirstOrDefaultAsync(u => u.Correo == email);

                if (usuario == null || !VerificarContrasenia(pass, usuario.Contrasenia))
                    return null;

                return usuario;
               
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return new Usuario { };
            }
        }



         public bool VerificarContrasenia(string contraseniaIngresada, string contrasenia)
         {
            return BCrypt.Net.BCrypt.Verify(contraseniaIngresada, contrasenia);
         }


         public async Task<string> GenerarToken(Usuario usuario)
         {
            var codigo = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuracion["Jwt:Key"]));
            var credenciales = new SigningCredentials(codigo, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
               new Claim(ClaimTypes.NameIdentifier, usuario.id.ToString()),
               new Claim(ClaimTypes.Email, usuario.Correo)
            };

            var token = new JwtSecurityToken(
                configuracion["Jwt:Issuer"],
                configuracion["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credenciales);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
