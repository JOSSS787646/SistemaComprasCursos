using System.Web.Mvc;
using System.Web.Routing;

namespace sistemadecompras
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "VerFoto",
                url: "Cursos/VerFoto",
                defaults: new { controller = "Cursos", action = "VerFoto" }
            );
            


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                  name: "Imprimir",
                  url: "ReportePdf/Imprimir",
                  defaults: new { controller = "ReportePdf", action = "pdf" } // Asociamos la ruta a la acción "pdf"
              );
            routes.MapRoute(
    name: "CrearCategoria",
    url: "Categorias/Create",
    defaults: new { controller = "Categorias", action = "Create" }
);


        }
    }
}
