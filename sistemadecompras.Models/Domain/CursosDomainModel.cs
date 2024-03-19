using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemadecompras.Models.Domain
{
    public class CursosDomainModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es oblicatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este campo es oblicatorio")]
        public string Contenido { get; set; }
        [Required(ErrorMessage = "Este campo es oblicatorio")]
        public double Precio { get; set; }

        public DateTime FechaInicial { get; set; }

        public DateTime FechaActualizacion { get; set; }

        public string UrlImagenCurso { get; set; }

        public int IdCategoria {  get; set; }
        public bool BitEliminado { get; set; }

    }
}
