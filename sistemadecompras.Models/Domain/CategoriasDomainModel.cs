using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemadecompras.Models.Domain
{
    public class CategoriasDomainModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set;}
        public bool BitEliminado { get; set;}
    }
}
