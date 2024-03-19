using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sistemadecompras.Models
{
    public class ContactDM
    {
  
            public int Id { get; set; }
            [Required(ErrorMessage = "Este campo es obligatorio")]
            [MaxLength(250, ErrorMessage = "Este campo no debe de exeder mas de 250 caracteres")]
            [MinLength(10, ErrorMessage = "Este campo debe tener almenos 10 ccaracteres")]
            [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?$", ErrorMessage = "Error el email no tiene el formato correcto")]
            public string Email { get; set; }

            public string Observaciones { get; set; }
        }
    }
