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
        public LoginService(TodoListDBContext context)
        {
            this.context = context;
        }

        public async Task<Usuario> RegistrarUsuario(string usuarioNombre, string correo, string contrasenia)
        {
            try
            {
                var user = await context.Usuario.FirstOrDefaultAsync(u => u.usuarioNombre == usuarioNombre && u.correo == correo);

                if (user != null)
                {
                    Console.WriteLine("Usuario o Correo en uso");
                    return null;
                }

                var usuario = new Usuario
                {
                    usuarioNombre = usuarioNombre,
                    correo = correo,
                    contrasenia = contrasenia
                };

                await context.Usuario.AddAsync(usuario);
                await context.SaveChangesAsync();

                return usuario;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return null;
            }
        }

        public async Task<Usuario?> ValidarUsuario(string correo, string contrasenia)
        {
            try
            {
                var usuario = await context.Usuario.FirstOrDefaultAsync(u => u.correo == correo && u.contrasenia == contrasenia);
                if(usuario == null)
                {
                    Console.WriteLine("Usuario no existente");
                    return null;
                }

                return usuario;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return null;
            }
        }
    }
}
