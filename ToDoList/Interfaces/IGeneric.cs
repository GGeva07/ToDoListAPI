using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface IGeneric<Entity> where Entity : class
    {
        List<Entity> Get();
        string Set(Entity model);

        string Update(Entity model);

        string Delete(int id);
    }
}
