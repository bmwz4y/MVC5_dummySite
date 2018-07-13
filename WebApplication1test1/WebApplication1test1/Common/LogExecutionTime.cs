using System;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Mvc;

/*
 * this is the custom action filter for lesson 77
 */

namespace WebApplication1test1.Common
{
    public class LogExecutionTime : ActionFilterAttribute, IExceptionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string message = Environment.NewLine + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName +
                             " -> " + filterContext.ActionDescriptor.ActionName + " -> OnActionExecuting \t- " +
                             DateTime.Now.ToString(CultureInfo.InvariantCulture);
            Log(message);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string message = Environment.NewLine + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName +
                             " -> " + filterContext.ActionDescriptor.ActionName + " -> OnActionExecuted \t- " +
                             DateTime.Now.ToString(CultureInfo.InvariantCulture);
            Log(message);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            string message = Environment.NewLine + filterContext.RouteData.Values["controller"] +
                             " -> " + filterContext.RouteData.Values["action"] + " -> OnResultExecuting \t- " +
                             DateTime.Now.ToString(CultureInfo.InvariantCulture);
            Log(message);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            string message = Environment.NewLine + filterContext.RouteData.Values["controller"] +
                             " -> " + filterContext.RouteData.Values["action"] + " -> OnResultExecuted \t- " +
                             DateTime.Now.ToString(CultureInfo.InvariantCulture);
            Log(message);
            Log(Environment.NewLine + "--------------------------------");
        }

        public void OnException(ExceptionContext filterContext)
        {
            string message = Environment.NewLine + filterContext.RouteData.Values["controller"] +
                             " -> " + filterContext.RouteData.Values["action"] + " -> " + filterContext.Exception.Message + " \t- " +
                             DateTime.Now.ToString(CultureInfo.InvariantCulture);
            Log(message);
            Log(Environment.NewLine + "--------------------------------");
        }

        private void Log(string logString)
        {
            File.AppendAllText(HttpContext.Current.Server.MapPath("~/App_Data/Log.txt"), logString);
        }
    }
}