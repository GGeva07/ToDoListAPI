using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface ITareas
    {
        public Task<List<Tareas>> Get();
        public Task<List<Tareas>> GetTareasByNombre(string Nombre);
        public Task<List<Tareas>> GetTareasByIdUsuario(int idUsuario);
        public Task<String> Post(int idUsuario, Tareas tarea);
        public Task<string> Put(int id, int idUsuario, Tareas tarea);
        public Task<string> Delete(int id, int idUsuario);
    }
}
