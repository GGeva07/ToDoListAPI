using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface ITareas : IGeneric<Tareas>
    {
        public Tareas? GetTareasByNombre(string Nombre);
    }
}
