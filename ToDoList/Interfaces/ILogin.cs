using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface ILogin
    {
        public Task<Usuario> ValidarUsuario(string email, string pass);
        public Task<String> GenerateJwtToken(Usuario usuario);
    }
}
