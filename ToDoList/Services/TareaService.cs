using Microsoft.EntityFrameworkCore;
using ToDoList.Context;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class TareaService : ITareas
    {
        private readonly TodoListDBContext context;

        public TareaService(TodoListDBContext context)
        {
            this.context = context;
        }

        public string Delete(int id)
        {
            var registro = context.Tareas.Find(id);
            context.Tareas.Remove(registro);
            context.SaveChanges();
            return "Regsitro Eliminado";
        }

        public List<Tareas> Get()
        {

            var data = context.Tareas.ToList();
            return data;
        }

        public Tareas? GetTareasByNombre(string Nombre)
        {
            var tarea1 = Get();
            var tarea = tarea1.Find(tarea => tarea.Nombre == Nombre);
            return tarea;
        }

        public string Set(Tareas model)
        {
            context.Tareas.Add(model);
            context.SaveChanges();
            return "Registro insertado";
        }

        public string Update(Tareas model)
        {
            context.Entry(model).State = EntityState.Modified;
            context.SaveChanges();
            return "Registro Actualizados";
        }
    }
}
