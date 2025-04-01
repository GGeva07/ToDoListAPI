using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface ILogin
    {
        public Task<Usuario> ValidarUsuario(string email, string pass);
    }
}
