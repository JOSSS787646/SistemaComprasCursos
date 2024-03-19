using sistemadecompras.Models;
using sistemadecompras.Models.Domain;
using sistemadecompras.Services.Infraestructura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemadecompras.Services.Cursos
{
    public class CursosServicios: ICursosServicios
    {
        private readonly SistemasComprasDBEntities entities;
        public CursosServicios() 
        {
            entities= new SistemasComprasDBEntities();   
        }
        public bool Insert(CursosDomainModel cursosDM)
        {
            bool respuesta = false;
            var transaccion = entities.Database.BeginTransaction();
            try
            {
                // Buscar la categoría por su nombre en la base de datos
                var categoria = entities.Categorias.FirstOrDefault(c => c.Id == cursosDM.IdCategoria);

                // Verificar si la categoría existe
                if (categoria != null)
                {
                    Models.Cursos cursos = new Models.Cursos()
                    {
                        Nombre = cursosDM.Nombre.ToLower().Trim(),
                        Contenido = cursosDM.Contenido.ToLower().Trim(),
                        FechaInicial = cursosDM.FechaInicial,
                        FechaActulizacion = cursosDM.FechaActualizacion,
                        Precio = decimal.Parse(cursosDM.Precio.ToString()),
                        UrlImgaenCurso = cursosDM.UrlImagenCurso,
                        // Asignar el ID de la categoría al nuevo curso
                        idCategoria = categoria.Id
                    };

                    entities.Cursos.Add(cursos);
                    entities.SaveChanges();
                    transaccion.Commit();
                    respuesta = true;
                }
                else
                {
                    throw new Exception("La categoría seleccionada no existe.");
                }

                return respuesta;
            }
            catch (Exception)
            {
                transaccion.Rollback();
                return respuesta;
            }
        }

        public bool Delete(int Id)
        {
            bool respuesta = false;
            var transaccion = entities.Database.BeginTransaction();
            try
            {
                Models.Cursos curso = entities.Cursos.Single(p=>p.Id == Id);
                entities.Cursos.Remove(curso);
                entities.SaveChanges();
                respuesta = true;
                return respuesta;
            }
            catch (Exception)
            {
                transaccion.Rollback();
                return respuesta;
               
            }
        }

        public List<CursosDomainModel> AllCursos()
        {
            List<CursosDomainModel> cursos = entities.Cursos.Select(p => new CursosDomainModel
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Contenido = p.Contenido,
                Precio = double.Parse(p.Precio.ToString()),
                FechaInicial = p.FechaInicial.Value,
                FechaActualizacion = p.FechaActulizacion.Value,
                UrlImagenCurso = p.UrlImgaenCurso
            }).ToList();
            return cursos;
        }
       
    }
}
