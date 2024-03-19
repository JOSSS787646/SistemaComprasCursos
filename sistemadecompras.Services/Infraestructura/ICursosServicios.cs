using sistemadecompras.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemadecompras.Services.Infraestructura
{
    public interface ICursosServicios
    {
        List<CursosDomainModel> AllCursos();
        bool Delete(int Id);
        bool Insert(CursosDomainModel cursosDM);
    }
}
