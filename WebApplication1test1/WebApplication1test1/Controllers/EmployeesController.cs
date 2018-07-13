using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

//added on lesson 74
using WebApplication1test1.Common;
using WebApplication1test1.Models;

namespace WebApplication1test1.Controllers
{
    /*
     * added lesson 62
     */

    public class EmployeesController : Controller
    {
        private readonly EmployeeModel _db = new EmployeeModel();

        /*
         * added on lesson 97
         */

        public JsonResult GetEmployees(string term) // term is jquery specified
        {
            List<string> employeeNames =
                _db.Employees.Where(emp => emp.Name.StartsWith(term)).Select(x => x.Name).ToList();
            return Json(employeeNames, JsonRequestBehavior.AllowGet);
        }

        /*
                 * added on lesson 92
                 */

        public PartialViewResult NeitherFlipNorFlop()
        {
            ViewBag.FlipFlop = "NEither";

            return PartialView("_FlipFlop");// can send model
        }

        /*
         * added on lesson 92
         */

        public PartialViewResult Flip()
        {
            //added on lesson 95 to test loading element
            System.Threading.Thread.Sleep(3000);

            ViewBag.FlipFlop = "Flip is warning";

            return PartialView("_FlipFlop");// can send model
        }

        /*
         * added on lesson 92
         */

        public PartialViewResult Flop()
        {
            ViewBag.FlipFlop = "Flop is success";

            return PartialView("_FlipFlop");// can send model
        }

        /*
         * added on lesson 89
         */

        public JsonResult IsEmailAvailable(string email)
        {
            return Json(!_db.Employees.Any(employee => employee.Email == email), JsonRequestBehavior.AllowGet);
        }

        /*
         * to test lesson lesson 77
         */

        [LogExecutionTime]
        public ActionResult TestLogging(string exception)
        {
            if (exception == "ex")
                throw new Exception("Exception Occured!");

            return View();
        }

        /*
         * to test lesson 71 child only then on lesson 73 output cache
         */

        [ChildActionOnly, ChildActionOutputCache("MainCacheProfile")] //this custom attribute in Common and it was before lesson 74: OutputCache(CacheProfile = "MainCacheProfile")]
        public PartialViewResult _Search(string moneyString)
        {
            ViewBag.MoneyString = "[$^ TotalSeconds to test cache: " + DateTime.Now.Second + moneyString;//a small edit to make sure of things going on
            return PartialView("_Search");
        }

        /*
         * to test lesson 68
         */

        [NonAction]
        public string NonActionString()
        {
            return "This is TheNonActionString";
        }

        // GET: Employees  & SearchEmpBy created on lesson 62 and edited on lesson 63, 64
        public ActionResult Index(string searchEmpBy, string searchExpression, int? page, string sortBy)
        {
            //added after lesson 100 to fix empty searchExpression page 2 and beyond empty paged list problem
            if (searchExpression == "")
            {
                return RedirectToAction("Index");
            }

            // for lesson 68
            ViewBag.TheNonActionString = NonActionString();

            //these are for lesson 64
            ViewBag.SortByNameParameter = string.IsNullOrEmpty(sortBy) ? "name desc" : "";
            ViewBag.SortByGenderParameter = sortBy == "gender" ? "gender desc" : "gender";
            ViewBag.SortByEmailParameter = sortBy == "email" ? "email desc" : "email";

            var employees = _db.Employees.AsQueryable();

            if (searchEmpBy == "Name")
            {
                employees = employees.Where(emp => emp.Name.Contains(searchExpression));//was  return view and ends with .ToList().ToPagedList(page ?? 1, 2) like below
            }
            else if (searchEmpBy == "Email")
            {
                employees = employees.Where(emp => emp.Email.Contains(searchExpression));
                // before lesson 64: return View(db.Employees.Where(emp => emp.Email.Contains(SearchExpression)).ToList().ToPagedList(page ?? 1, 2));
            }
            /*
             * before lesson 64:
            else
            {
                return View(db.Employees.ToList().ToPagedList(page ?? 1, 2));
            }
            */

            //added on lesson 64
            switch (sortBy)
            {
                case "name desc":
                    employees = employees.OrderByDescending(emp => emp.Name);
                    break;

                case "gender":
                    employees = employees.OrderBy(emp => emp.Gender);
                    break;

                case "gender desc":
                    employees = employees.OrderByDescending(emp => emp.Gender);
                    break;

                case "email":
                    employees = employees.OrderBy(emp => emp.Email);
                    break;

                case "email desc":
                    employees = employees.OrderByDescending(emp => emp.Email);
                    break;

                default:
                    employees = employees.OrderBy(emp => emp.Name);
                    break;
            }

            return View(employees.ToPagedList(page ?? 1, 7));
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Gender,Email,EmailConfirm")] Employee employee)// lesson 85 had to add EmailConfirm
        {
            //added on lesson 90 but need to disble js and also check that edit doesn't have these lines
            //removed on lesson 91
            //if (db.Employees.Any(emp => emp.Email == employee.Email))
            //{
            //    ModelState.AddModelError("Email", "Email already registered!!");
            //}

            if (ModelState.IsValid)
            {
                _db.Employees.Add(employee);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Gender,Email,EmailConfirm")] Employee employee)// lesson 85 had to add EmailConfirm
        {
            if (ModelState.IsValid)
            {
                _db.Entry(employee).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        //added lesson 65
        //[HttpPost]
        //// GET: Employees/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employee employee = db.Employees.Find(id);
        //    return View(employee);
        //}

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(IEnumerable<int> idsToDelete)//was DeleteConfirmed(int? id)
        {
            if (idsToDelete != null)
            {
                _db.Employees.RemoveRange(_db.Employees.Where(emp => idsToDelete.Contains(emp.Id)).ToList());
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}