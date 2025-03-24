using ToDoList.Context;
using ToDoList.Interfaces;
using ToDoList.Models;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Services
{
    public class UsuarioService : IUsuario
    {
        private readonly TodoListDBContext context;

        public UsuarioService(TodoListDBContext context)
        {
            this.context = context;
        }

        public string Delete(int id)
        {
            var registro = context.Usuario.Find(id);
            context.Usuario.Remove(registro);
            context.SaveChanges();
            return "Registro Eliminado";
        }

        public List<Usuario> Get()
        {
            return context.Usuario.ToList();
        }

        public string Set(Usuario model)
        {
            context.Usuario.Add(model);
            context.SaveChanges();
            return "Regsitro insertado";
        }

        public string Update(Usuario model)
        {
            context.Entry(model).State = EntityState.Modified;
            context.SaveChanges();
            return "Registro Actualizado";
        }
    }
}
