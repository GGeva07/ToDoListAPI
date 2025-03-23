using Microsoft.AspNetCore.Identity;
using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface IUsuario
    {
        List<Usuario> Getusuarios();

        string SetUsuario(Usuario model);

        string UpdateUsuario(Usuario model);

        string DeleteUsuario(int id);


    }
}
