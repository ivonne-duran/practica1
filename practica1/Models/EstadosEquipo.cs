using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace practica1.Models
{
    [Table("estados_equipo")]
    public class EstadosEquipo
    {
        [Key]
        public int id_estados_equipo { get; set; }

        public string descripcion { get; set; }

        public string estado { get; set; }
    }
}
