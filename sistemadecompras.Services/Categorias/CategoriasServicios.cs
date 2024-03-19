using sistemadecompras.Models;
using sistemadecompras.Models.Domain;
using sistemadecompras.Services.Infraestructura;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sistemadecompras.Services.Categorias
{
    public class CategoriasServicios : ICategoriasServicios
    {
        private readonly SistemasComprasDBEntities entities;
        public CategoriasServicios()
        {
            entities = new SistemasComprasDBEntities();
        }
        public bool Insert(CategoriasDomainModel categoriasDM)
        {
            bool respuesta = false;
            var transaccion = entities.Database.BeginTransaction();
            try
            {
                Models.Categorias categorias = new Models.Categorias()
                
                
                {
                    Nombre = categoriasDM.Nombre.ToLower().Trim(),
                    FechaCreacion = categoriasDM.FechaCreacion,
                };
                entities.Categorias.Add(categorias);
                entities.SaveChanges();
                transaccion.Commit();
                respuesta = true;
            }
            catch (Exception)
            {
                transaccion.Rollback();
            }
            return respuesta;
        }
        public List<CategoriasDomainModel> AllCategorias()
        {
            List<CategoriasDomainModel> categorias = entities.Categorias.Select(p => new CategoriasDomainModel
            {
                Id = p.Id,
                Nombre = p.Nombre,
                FechaCreacion = p.FechaCreacion.Value,
                
            }).ToList();
            return categorias;
        }

    }
}
