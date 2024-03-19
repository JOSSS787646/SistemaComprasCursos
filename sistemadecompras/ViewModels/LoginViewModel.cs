using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sistemadecompras.ViewModels
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo es Requerido")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "El campo es Requerido")]
        public string Password { get; set; }
    }
}