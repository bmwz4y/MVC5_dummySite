using System.Web.Mvc;

namespace WebApplication1test1.Controllers
{
    /*
     * This was added lesson 72
     */

    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult NotFound()
        {
            return View();
        }
    }
}