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

        public async Task<Usuario> GetUsuarioById(int id)
        {
            try
            {
                var usuario = await context.Usuario.FirstOrDefaultAsync(u => u.id == id);

                if (usuario == null)
                    return null;

                return usuario;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return null;
            }
        }


        public async Task<string> Post(Usuario model)
        {
            try
            {     
                var usuario = context.Usuario.FirstOrDefault(u => u.correo == model.correo);

                if (usuario != null)
                {
                    return("El usuario ya existe.");
                }

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

                usuario.usuarioNombre = model.usuarioNombre;
                usuario.correo = model.correo;
                usuario.contrasenia = model.contrasenia;

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

                var tareasBorrar = await context.Tarea.Where(t => t.idUsuario == id).ToListAsync();

                if (tareasBorrar != null)
                {
                    context.Tarea.RemoveRange(tareasBorrar);
                }
                else
                {
                    return "El usuario no tiene tareas registradas para borrar";
                }

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
