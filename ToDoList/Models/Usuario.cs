using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Usuario
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string Correo { get; set; }


        [Required]
        public string Contrasenia { get; set; }

        public List<Tareas>? ListadeTareas { get; set; }

    }
}
