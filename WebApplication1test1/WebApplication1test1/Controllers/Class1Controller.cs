using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1test1.Models;

namespace WebApplication1test1.Controllers
{
    [AttributeRouting.RoutePrefix("Class1/E")]
    public class Class1Controller : Controller
    {
        /*
         * started at lesson 54 to test editing of t4 templates
         */

        public ActionResult T4()
        {
            ViewBag.TestText = "this is the Custom Text";
            return View();
        }

        /*
         * started at lesson 52
         */

        public ActionResult EPartial()
        {
            Class1Context class1Context = new Class1Context();
            return View(class1Context.Class1S.ToList());
        }

        /*
         * instead of lesson 24 and edited in lesson 60 and renamed in lesson 67
         */

        [HttpGet]
        [ActionName("Delete")]
        public PartialViewResult _Delete(int id)// was Delete(int id) but now I can use ctrl+m, ctrl+g
        {
            Class1Context class1Context = new Class1Context();
            Class1 class1 = class1Context.Class1S.Single(e => e.SomeId == id);
            return PartialView("_Delete", class1);
        }

        /*
         * instead of lesson 24
         */

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete_Post(int id)
        {
            Class1Context class1Context = new Class1Context();
            Class1 class1 = class1Context.Class1S.Single(e => e.SomeId == id);
            class1Context.Class1S.Remove(class1);
            class1Context.SaveChanges();
            return RedirectToAction("E");
        }

        /*
         * lesson 23 not using an [HttpPost] on Delete ActionResult is a bad idea for security
        /*
         * and at lesson 18 started this
         */
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(int id) // lesson 21 wanted to use ([Bind(exclude="Name")]Class1 class1) but must omit [required] in model which would change expected create behaviour // before lesson 20 (Class1 class1)
        {
            /*
             * lesson 22 isn't needed because IClass1 interface and Bind would still need to omit [required] in model *same as lesson 21
             */

            Class1Context class1Context = new Class1Context();

            /*
             * added at lesson 20
             */
            Class1 class1 = class1Context.Class1S.Single(e => e.SomeId == id);
            UpdateModel(class1, null, null, new[] { "name" });// take note that it is case insensitive

            if (ModelState.IsValid)
            {
                /*
                 * Moved up at lesson 20
                Class1Context class1Context = new Class1Context();
                */

                class1Context.Class1S.AddOrUpdate(class1);
                class1Context.SaveChanges();

                return RedirectToAction("E");
            }

            return View(class1);
        }

        /*
         * and at lesson 17 started this
         */
        [AttributeRouting.Web.Mvc.Route] // added after lesson 100
        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit(int id)
        {
            Class1Context class1Context = new Class1Context();
            Class1 class1 = class1Context.Class1S.Single(e => e.SomeId == id);

            return View(class1);
        }

        /*
         * and at lesson 13 started this and validateInput is for lesson 55 and lesson 56
         */
        [ValidateInput(false)]
        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post(string name)//lesson 15 at start only (Class1 class1)//lesson 14 (string name, string something) //lesson13 (FormCollection formCollection)
        {
            //for lesson 55 and 56 XSS using name property to prevent non-Replaced tags
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(HttpUtility.HtmlEncode(name));
            stringBuilder.Replace("&lt;b&gt;", "<b>");
            stringBuilder.Replace("&lt;/b&gt;", "</b>");
            stringBuilder.Replace("&lt;u&gt;", "<u>");
            stringBuilder.Replace("&lt;/u&gt;", "</u>");

            /*
             * added on lesson 16
             */
            Class1 class1 = new Class1();
            TryUpdateModel(class1);
            /* lesson 15 omitted these
            Class1 class1 = new Class1();
            class1.Name = name; //lesson13 version formCollection["Name"];
            class1.Something = something; //lesson13 version formCollection["Something"];
            */
            /*
             * added on lesson 15
             */
            if (ModelState.IsValid)
            {
                //for lesson 55 and 56 XSS using name property
                class1.Name = stringBuilder.ToString();

                /*
                 * omitted by lesson 16
                Class1 class1 = new Class1();
                UpdateModel(class1);*/

                Class1Context class1Context = new Class1Context();
                class1Context.Class1S.Add(class1);
                class1Context.SaveChanges();

                return RedirectToAction("E");
            }
            return View();
            /*
             * before database this one was used to view on same create page
            foreach (string key in formCollection.AllKeys)
            {
                if (key == "__RequestVerificationToken")
                    continue;
                Response.Write(key + " = ");
                Response.Write(formCollection[key]);
                Response.Write("<br/>");
            }
            return View();
            */
        }

        /*
         * and at lesson 12 started this
         */
        [AttributeRouting.Web.Mvc.Route] // added after lesson 100
        [HttpGet]
        //added in 15 to keep constant naming
        [ActionName("Create")]
        public ActionResult Create()
        {
            return View();
        }

        /*
         * started at lesson 12
         */
        public ActionResult E()
        {
            Class1Context class1Context = new Class1Context();
            List<Class1> class1S = class1Context.Class1S.ToList();
            return View(class1S);
        }

        /*
         * This is a dummy to increase views at lesson 10 instead of repeated nesting and creating tables and data
         * also instead of lesson 11 there should be a connected sentence from dummy project dummy1
         */

        public ActionResult Dummy()
        {
            return View();
        }

        /*
         * started at lesson 9
         */

        public ActionResult Index()
        {
            Class1Context class1Context = new Class1Context();
            List<Class1> class1S = class1Context.Class1S.ToList();
            return View(class1S);
        }

        /*
         * Up to lesson 8
         */

        public ActionResult SomethingDetails(int id)
        {
            Class1Context class1Context = new Class1Context();
            Class1 class1 = class1Context.Class1S.Single(index => index.SomeId == id);
            /*
             *The code previously based on listing data from code without database
             *
            Class1 class1 = new Class1()
            {
                someId = 1, Name = "is name", Something = "dod"
            };
            */
            return View(class1);
        }
    }
}