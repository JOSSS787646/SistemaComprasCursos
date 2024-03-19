using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sistemadecompras.ViewModels
{
    public class CursosViewModel
    {
        public int Id {  get; set; }

        [Required(ErrorMessage ="Este campo es oblicatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este campo es oblicatorio")]
        public string Contenido { get; set; }
        [Required(ErrorMessage = "Este campo es oblicatorio")]
        public double Precio { get; set; }

        public DateTime FechaInicial { get; set; }

        public DateTime FechaActualizacion { get; set; }

        public string UrlImagenCurso { get; set; }

        [Required(ErrorMessage = "El autor es obligatorio")]
        public AutorViewModel AutorViewModel { get; set; }
        public HttpPostedFileWrapper ImagenFile { get; set; }
        public int IdCategoria { get; set; } // Propuedad para almacenar Id de la categoria
        public string NombreCategoria { get; set; } // Nueva propiedad para almacenar el nombre de la categoría seleccionada
        public List<CategoriasViewModel> Categorias { get; set; } 


    }
}