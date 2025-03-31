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
    public class UsuarioService : IUsuario
    {
        private readonly TodoListDBContext context;


        public UsuarioService(TodoListDBContext context)
        {
            this.context = context;
        }

        public async Task<List<Usuario>> Get()
        {
            try
            {
                var usuarios = await context.Usuario.ToListAsync();
                return usuarios;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return new List<Usuario>();
            }
        }

        public async Task<string> Post(Usuario model)
        {
            try
            {
                await context.Usuario.AddAsync(model);
                await context.SaveChangesAsync();
                return "Registro insertado correctamente";
            }
            catch (Exception e)
            {
                return $"Error al insertar el registro: {e.Message}";
            }
        }

        public async Task<string> Put(int id, Usuario model)
        {
            try
            {
                var usuario = await context.Usuario
                    .FirstOrDefaultAsync(user => user.id == id);

                if (usuario == null)
                {
                    return "Usuario no encontrado";
                }

                // Actualizamos los valores del usuario
                usuario.Nombre = model.Nombre;
                usuario.Correo = model.Correo;
                usuario.Contrasenia = model.Contrasenia;

                await context.SaveChangesAsync();
                return "Registro actualizado correctamente";
            }
            catch (Exception e)
            {
                return $"Error al actualizar el registro: {e.Message}";
            }
        }

        public async Task<string> Delete(int id)
        {
            try
            {
                var usuario = await context.Usuario
                    .FirstOrDefaultAsync(u => u.id == id);

                if (usuario == null)
                {
                    return "Usuario no encontrado";
                }

                var tareasBorrar = await context.Tareas
                    .Where(t => t.idUsuario == id)
                    .ToListAsync();

                context.Tareas.RemoveRange(tareasBorrar);
                context.Usuario.Remove(usuario);
                await context.SaveChangesAsync();
                return "Usuario y tareas pertenecientes a este eliminados correctamente";
            }
            catch (Exception e)
            {
                return $"Error al eliminar el usuario: {e.Message}";
            }
        }
    }
}
