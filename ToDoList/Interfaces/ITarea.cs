using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface ITarea
    {
        public Task<List<Tarea>> Get();
        public Task<List<Tarea>> GetTareasByNombre(string Nombre);
        public Task<List<Tarea>> GetTareasByIdUsuario(int idUsuario);
        public Task<Object> GetTareaById(int id);
        public Task<Object> Post(Tarea tarea);
        public Task<Object> Put(Tarea tarea);
        public Task<Object> Delete(int id, int idUsuario);
    }
}
