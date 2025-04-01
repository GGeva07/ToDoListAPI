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
        public async Task<Usuario?> ValidarUsuario(string correo, string contrasenia)
        {
            try
            {
                return await context.Usuario
               .FirstOrDefaultAsync(u => u.correo == correo && u.contrasenia == contrasenia);
            }catch(Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return null;
            }
        }
    }
}
