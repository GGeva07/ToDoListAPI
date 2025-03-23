using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface ITarea
    {
        List<Tareas> GetTarea();

        string SetTarea(Tareas model);

        string UpdateTarea(Tareas model);

        string DeleteTarea(int id);
    }
}
