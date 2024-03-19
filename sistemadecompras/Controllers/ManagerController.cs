using sistemadecompras.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistemadecompras.Controllers
{
    public class ManagerController : Controller
    {

        public ActionResult Principal()
        {
            return View();

        }
        public ActionResult Header()
        {

            return PartialView("_Header");

        }
        public ActionResult Nav()
        {

            return PartialView("_Nav");


        }
        public ActionResult CuadroInicio()
        {

            return PartialView("_CuadroInicio");

        }
        public ActionResult Card()
        {

            return PartialView("_Card");

        }
        public ActionResult Content()
        {
            return PartialView("_Content");
        }
        public ActionResult Footer()
        {
            return PartialView("_Footer");
        }

        [HttpGet]
        public ActionResult Contacto()
        {
            return View("_Contacto");
        }

        [HttpPost]
        public ActionResult Contacto([Bind(Include = "Email,Observaciones")] ContactDM contactDM)
        {
            
            return View("_Contacto");
        }
    }
}

