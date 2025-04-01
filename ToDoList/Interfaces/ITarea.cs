using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface ITarea
    {
        public Task<List<Tarea>> Get();
        public Task<List<Tarea>> GetTareasByNombre(string Nombre);
        public Task<List<Tarea>> GetTareasByIdUsuario(int idUsuario);
        public Task<String> Post(int idUsuario, Tarea tarea);
        public Task<string> Put(int id, int idUsuario, Tarea tarea);
        public Task<string> Delete(int id, int idUsuario);
    }
}
