using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Threading;
using ToDoList.Context;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class TareaService : ITarea
    {
        private readonly TodoListDBContext context;

        public TareaService(TodoListDBContext context)
        {
            this.context = context;
        }

        public async Task<List<Tarea>> Get()
        {
            try
            {
                var tarea = context.Tarea.ToListAsync();
                return await tarea;
            }catch(Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return new List<Tarea>();
            }
        }

        public async Task<Object> GetTareaById(int id)
        {
            try
            {
                var tarea = await context.Tarea.FirstOrDefaultAsync(t => t.id == id);
                if (tarea == null)
                {
                    return new { message = "Tarea no encontrada" };
                }
                return tarea;
            }
            catch (Exception e)
            {
                return new { error = $"Error: {e.Message}" };
            }
        }

        public async Task<List<Tarea>> GetTareasByNombre(string Nombre)
        {
            try
            {
                var Tareas = context.Tarea.Where(t => t.Nombre == Nombre).ToListAsync();
                return await Tareas;
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return new List<Tarea>();
            }
        }

        public async Task<List<Tarea>> GetTareasByIdUsuario(int id)
        {
            try
            {

                var tarea = context.Tarea.Where(t => t.idUsuario == id).ToListAsync();
                return await tarea;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return new List<Tarea>();
            }
        }

        public async Task<object> Post(Tarea model)
        {
            try
            {
                var usuario = await context.Usuario
                    .FirstOrDefaultAsync(u => u.id == model.idUsuario);

                if (usuario == null)
                {
                    return new { message = "Usuario no encontrado" };
                }

                model.idUsuario = model.idUsuario;
                await context.Tarea.AddAsync(model);
                await context.SaveChangesAsync();

                return new { message = "Tarea creada", tarea = model }; 
            }
            catch (Exception e)
            {
                return new { error = $"Error: {e.Message}" }; 
            }
        }



        public async Task<object> Put(Tarea model)
        {
            var tarea = await context.Tarea
                .FirstOrDefaultAsync(t => t.id == model.id && t.idUsuario == model.idUsuario);

            if (tarea == null)
            {
                return new { message = "Tarea no encontrada" };
            }

            tarea.Nombre = model.Nombre;
            tarea.Contenido = model.Contenido;
            tarea.Estado = model.Estado;

            await context.SaveChangesAsync();
            return new { message = "Tarea editada", tarea };
        }

        public async Task<object> Delete(int id)
        {
            try
            {
                var tareaEliminar = await context.Tarea
                    .FirstOrDefaultAsync(t => t.id == id);
                var tarea = await context.Tarea.FirstOrDefaultAsync(T => T.idUsuario == tareaEliminar.idUsuario);

                if (tareaEliminar == null )
                {
                    return new { message = "Tarea a eliminar no encontrada" };
                }

                context.Tarea.Remove(tarea);
                await context.SaveChangesAsync();

                return new { message = "Tarea eliminada", tarea = tareaEliminar };
            }
            catch (Exception e)
            {
                return new { error = $"Error: {e.Message}" };
            }
        }
    }
}
