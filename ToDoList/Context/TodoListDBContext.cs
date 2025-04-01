using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Context
{
    public class TodoListDBContext : DbContext
    {
        public TodoListDBContext(DbContextOptions<TodoListDBContext> db): base(db)
        {}
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Tarea> Tarea { get; set; }
    }
}
