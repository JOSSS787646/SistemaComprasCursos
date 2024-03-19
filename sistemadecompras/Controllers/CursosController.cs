using NLog;
using sistemadecompras.Models.Domain;
using sistemadecompras.Services.Categorias;
using sistemadecompras.Services.Cursos;
using sistemadecompras.Services.Infraestructura;
using sistemadecompras.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace sistemadecompras.Controllers
{
    public class CursosController : Controller
    {

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private string pathFullFile = string.Empty;
        private ICursosServicios cursosServicios = null;
        private readonly ICategoriasServicios categoriasServicios = null;

        public CursosController()
        {
            cursosServicios = new CursosServicios();
            categoriasServicios = new CategoriasServicios();
        }

        [HttpGet]
        [Authorize(Roles ="Administrador")]
        public ActionResult Create()
        {
            var categorias = categoriasServicios.AllCategorias();
            ViewBag.Categorias = categorias;

            ClaimsPrincipal principal = this.User as ClaimsPrincipal;
            var rol = principal.Claims.Where(p => p.Type == ClaimTypes.Role).Select(p => p.Value).ToList();
            ViewBag.Role = rol;

            return View();
        }
        [HttpPost]
        public ActionResult Create(CursosViewModel cursosViewModel)
        {
            ClaimsPrincipal principal = this.User as ClaimsPrincipal;
            var rol = principal.Claims.Where(p => p.Type == ClaimTypes.Role).Select(p => p.Value).ToList();
            ViewBag.Role = rol;

            if (cursosViewModel.ImagenFile != null && cursosViewModel.ImagenFile.ContentLength > 0)
            {
                if (SaveFile(cursosViewModel))
                {
                    CursosDomainModel cursosDM = new CursosDomainModel();
                    AutoMapper.Mapper.Map(cursosViewModel, cursosDM);
                    cursosDM.UrlImagenCurso = pathFullFile;
                    cursosDM.FechaInicial = DateTime.Now;
                    cursosDM.FechaActualizacion = DateTime.Now;

                    // Asignar el ID de la categoría seleccionada al curso
                    cursosDM.IdCategoria = cursosViewModel.IdCategoria;

                    if (!cursosServicios.Insert(cursosDM))
                    {
                        Directory.Delete(pathFullFile);
                        ModelState.AddModelError("FileError", "No se pudo guardar: consulta al administrador");
                    }
                    else
                    {
                        return RedirectToAction("SuccessAction", "SuccessController");
                    }
                }
                else
                {
                    ModelState.AddModelError("FileError", "No se pudo guardar el curso: esta repetido");
                }
            }
            var categorias = categoriasServicios.AllCategorias();
            ViewBag.Categorias = categorias;

            return View(cursosViewModel);
        }

        private bool SaveFile(CursosViewModel cursosViewModel)
        {
            try
            {
                string nombre = Guid.NewGuid().ToString() + Path.GetExtension(cursosViewModel.ImagenFile.FileName);
                string pathFileUrl = Server.MapPath("~/Imagenes");
                string curso = cursosViewModel.Nombre.ToLower().Trim();
                string directorio=pathFileUrl+"\\"+curso;

                pathFullFile = Path.Combine(pathFileUrl+"\\"+curso, nombre);
                if (!Directory.Exists(directorio))
                {
                    Directory.CreateDirectory(directorio);
                    cursosViewModel.ImagenFile.SaveAs(pathFullFile);
                }
                else
                {
                    return false;
                }
                return true;

            }
            catch (FileLoadException fex)
            {
                _logger.Error(fex.Message);
                return false;
            }
        }
    }

}
