using sistemadecompras.Models.Domain;
using sistemadecompras.Services.Categorias;
using sistemadecompras.ViewModels;
using System.Web.Mvc;

namespace sistemadecompras.Controllers
{
    public class CategoriasController : Controller
    {

        private readonly CategoriasServicios _categoriasServicios;

        public CategoriasController()
        {
            _categoriasServicios = new CategoriasServicios();
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create(CategoriasViewModel categoriasViewModel)
        {
            if (ModelState.IsValid)
            {
                var nuevaCategoria = new CategoriasDomainModel { Nombre = categoriasViewModel.Nombre };

                if (_categoriasServicios.Insert(nuevaCategoria))
                {
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Error al guardar la categoría. Por favor, inténtelo de nuevo.");
                }
            }

            return View(categoriasViewModel);
        }
    }
}
