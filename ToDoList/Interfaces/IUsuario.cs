   using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface IUsuario
    {
        public Task<List<Usuario>> Get();

        public Task<Usuario> GetUsuarioById(int id);
        public Task<string> Post(Usuario model);
        public Task<string> Put(int id, Usuario model);
        public Task<string> Delete(int id);
    }
}
