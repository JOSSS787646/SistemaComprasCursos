using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistemadecompras.Filters
{
    public class ExeptionLogginAttribute : FilterAttribute, IExceptionFilter
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }
            Logger.Error(filterContext.Exception, "No se pudo controlar la excepcion");

            filterContext.Result = new ViewResult
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary
                {
                    { "Exception", filterContext.Exception }
                }
            };
            filterContext.ExceptionHandled = true;
        }
    }
}
           
      