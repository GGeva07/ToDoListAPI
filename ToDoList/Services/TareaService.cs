using Microsoft.EntityFrameworkCore;
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

        public string DeleteTarea(int id)
        {
            var registro = context.Tareas.Find(id);
            context.Tareas.Remove(registro);
            context.SaveChanges();
            return "Regsitro Eliminado";
        }

        public List<Tareas> GetTarea()
        {

            var data = context.Tareas.ToList();
            Console.WriteLine(data);
            return data;
        }

        public string SetTarea(Tareas model)
        {
            context.Tareas.Add(model);
            context.SaveChanges();
            return "Registro insertado";
        }

        public string UpdateTarea(Tareas model)
        {
            context.Entry(model).State = EntityState.Modified;
            context.SaveChanges();
            return "Registro Actualizados";
        }
    }
}
