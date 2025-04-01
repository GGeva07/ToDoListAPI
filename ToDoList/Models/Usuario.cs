using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Usuario
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string usuarioNombre { get; set; }

        [Required]
        public string correo { get; set; }

        [Required]
        public string contrasenia { get; set; }
    }
}
