﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    public class Tareas
    {
        [Key]
        public int id { get; set; }
        public string Nombre { get; set; }
        public string  Contenido { get; set; }

        [Required]
        public string Estado { get; set; }

        [ForeignKey("Usuario")]
        public int idUsuario { get; set; }
        public Usuario? Usuario { get; set;  }
    }
}
