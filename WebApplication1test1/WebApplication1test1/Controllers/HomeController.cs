using System.Collections.Generic;
using System.Web.Mvc;

namespace WebApplication1test1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application descr(did i remove)";

            return View();
        }

        public ActionResult Lis()
        {
            ViewBag.mylist = new List<string>() { "L1", "L2", "L3" };
            return View();
        }

        /*
         * testing hardcoding view and model inside the controller (no MVC approach)
         */

        public string Newview(string newtext)
        {
            return "<!DOCTYPE html>\n<html>\n<h1>your text = \"" + newtext + "\"</h1>\n" + "<p>" + Request.QueryString["asdf"] + "</p>\n</html>";
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}