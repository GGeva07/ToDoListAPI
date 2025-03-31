using ToDoList.Context;
using ToDoList.Interfaces;
using ToDoList.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
                    .FirstOrDefaultAsync(u => u.Correo == email && u.Contrasenia == pass);

                if (usuario != null)
                {
                    return new Usuario { Nombre = "", Apellido = "", Correo = email, Contrasenia = pass };
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return new Usuario { };
            }
        }

        public string GenerateJwtToken(Usuario usuario)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuracion["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

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
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
