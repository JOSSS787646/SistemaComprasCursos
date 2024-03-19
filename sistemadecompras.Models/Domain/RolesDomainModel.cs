using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemadecompras.Models.Domain
{
   public class RolesDomainModel
        {
            public int Id { get; set; }

            public string StrValor { get; set; }

            public List<LoginDomainModel> Usuarios { get; set; }
        }
    }