    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemadecompras.Models.Domain
{
    public class LoginDomainModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public RolesDomainModel Roles { get; set; }
    }
}
