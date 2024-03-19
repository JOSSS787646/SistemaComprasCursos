using sistemadecompras.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemadecompras.Services.Infraestructura
{
    public interface ICategoriasServicios
    {
        List<CategoriasDomainModel> AllCategorias();
        bool Insert(CategoriasDomainModel categoriasDM);
    }
}
