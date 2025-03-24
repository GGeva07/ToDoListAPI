using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Web.Models.Tarea
{
    public class TareaModel
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public string Contenido { get; set; }
        public string Estado { get; set; }
        public int idUsuario { get; set; }
    }
}
