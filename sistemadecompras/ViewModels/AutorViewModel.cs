using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sistemadecompras.ViewModels
{
    public class AutorViewModel
    {
        public int Id { get; set; }

      
        public string Nombre { get; set; }
       
       public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string Experiencia { get; set; }

        public string DatosProfesionales { get; set; }

        public DateTime FechaInicio { get; set; }


    }
}