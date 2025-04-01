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

        public async Task<String> Post(int idUsuario, Tarea model)
        {
            try
            {
                var usuario = await context.Usuario
                    .FirstOrDefaultAsync(u => u.id == idUsuario);

                if (usuario == null)
                {
                    return "Usuario no encontrado";
                }

                model.idUsuario = idUsuario;
                await context.Tarea.AddAsync(model);

                await context.SaveChangesAsync();
                return "Tarea creada";


            }
            catch (Exception e)
            {
                return $"Error: {e.Message}";
            }
        }


        public async Task<String> Put(int id, int idUsuario, Tarea model)
        {
            var tarea = await context.Tarea
                .FirstOrDefaultAsync(t => t.id == id && t.idUsuario == idUsuario);

            if (tarea == null)
            {
                return "Tarea no encontrada";
            }

            tarea.Nombre = model.Nombre;
            tarea.Contenido = model.Contenido;
            tarea.Estado = model.Estado;

            await context.SaveChangesAsync();
            return "Tarea editada";
        }

        public async Task<string> Delete(int id, int idUsuario)
        {
            try
            {
                var tareaEliminar = await context.Tarea
                .FirstOrDefaultAsync(t => t.id == id && t.idUsuario == idUsuario);


                if (tareaEliminar == null)
                {
                    return "Tarea a eliminar no encontrada!!";
                }

                context.Tarea.Remove(tareaEliminar);

                await context.SaveChangesAsync();
                return "Tarea eliminada";

            }
            catch (Exception e)
            {
                return $"Error: {e.Message}";
            }
        }
    }
}
