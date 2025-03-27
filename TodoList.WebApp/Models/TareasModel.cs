using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TodoList.WebApp.Models
{
    public class TareasModel
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public string Contenido { get; set; }
        public string Estado { get; set; }
    }
}