using Microsoft.Ajax.Utilities;
using sistemadecompras.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace sistemadecompras.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            #region Roles del usuario
            ClaimsPrincipal principal = this.User as ClaimsPrincipal;
            var rol = principal.Claims.Where(p => p.Type == ClaimTypes.Role).Select(p => p.Value).ToList();
            ViewBag.Role = rol;
            #endregion
            return View();
        }
        [Authorize(Roles = "Invitado")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            if (Session["userRegister"] != null)
            {
                RegisterAccountDm accountDM = (RegisterAccountDm)Session["userRegister"];
                ViewBag.Usuario = accountDM.UserName + " " + accountDM.Password;
            }
            else
            {
                ViewBag.Usuario = "No hay Nada";
            }
            return View();


        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Mensaje()
        {
            if (TempData["algo2"] != null)
            {
                if (TempData["algo2"].ToString() != string.Empty)
                {
                    ViewBag.Nombre = TempData["algo2".ToString()];
                    return View();
                }

            }
            ViewBag.Nombre = "Wey TE FALTA ALGO AQUI!!!!!!";
            return View();


        }

        [HttpGet]
        public ActionResult consulta()
        {
            return View();
        }
        [HttpPost]

        public ActionResult consulta(Persona persona)
        {
            var algo = persona.Nombre;
            ViewBag.algo = algo;
            TempData["algo2"] = algo;

            return RedirectToAction("Mensaje", "Home");
        }

        [HttpGet]
        public ActionResult Principal()
        {
            return View();
        }

    }
}