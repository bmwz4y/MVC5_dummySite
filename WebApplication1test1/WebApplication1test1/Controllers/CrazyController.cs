using System;
using System.Collections.Generic;
using Microsoft.Ajax.Utilities;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Web.Mvc;
using WebApplication1test1.Models;

/*
 * edits from lesson 27 + 28 + 29
 * 30 + 31 + 32 is informational only
 * 33 small edit in ShowAll.cshtml
 * 34 to 43 rendering of specific html in asp.net way
 * 44 informational about helpers (Display, DisplayFor, DisplayForModel, Editor, EditorFor, EditorForModel...)
 */

namespace WebApplication1test1.Controllers
{
    public class CrazyController : Controller
    {
        private readonly CrazyTableModel _db = new CrazyTableModel();

        /*
         * added to substitute lesson 29
         */

        public ActionResult CrazyByDate()
        {
            /*
             * third trial
             */
            var crazyByDatesQuery = from theCrazy in _db.CrazyTables
                group theCrazy by theCrazy.CrazyDate
                into dateGroup
                orderby dateGroup.Key
                select dateGroup;

            var crazyByDates = new List<CrazyByDate>();

            foreach (var dateGroup in crazyByDatesQuery)
            {
                var crazyByDate = new CrazyByDate {Date = dateGroup.Key, Crazies = ""};

                StringBuilder stringBuilder = new StringBuilder();
                foreach (var theCrazy in dateGroup)
                {
                    stringBuilder.Append(theCrazy.Crazy + ", ");
                }
                stringBuilder.Length = stringBuilder.Length - 2;
                crazyByDate.Crazies = stringBuilder.ToString();

                crazyByDates.Add(crazyByDate);
            }

            return View(crazyByDates.ToList());

            /*
             * second trial
             */
            //var allCrazyByDate = _db.CrazyTables.GroupBy(x => new {x.CrazyDate, x.Crazy},
            //    (key, group) => new {Key1 = key.CrazyDate, Key2 = key.Crazy, Result = group.ToList()});

            //return View(allCrazyByDate);


            /*
             * first trial
             */
            //var allCrazyByDate = _db.CrazyTables.GroupBy(x => new CrazyByDate {
            //    Date = x.CrazyDate, Crazies = x.Crazy
            //}).ToList();

            //ViewBag.Crazies = allCrazyByDate.
            //return View(allCrazyByDate);
        }

        // GET: crazy
        public ActionResult ShowAll()
        {
            return View(_db.CrazyTables.ToList());
        }

        // GET: crazy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrazyTable crazyTable = _db.CrazyTables.Find(id);
            if (crazyTable == null)
            {
                return HttpNotFound();
            }
            return View(crazyTable);
        }

        // GET: crazy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: crazy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,crazy,crazydate")] CrazyTable crazyTable)
        {
            if (crazyTable.Crazy.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("crazy", "The crazy field is required");
            }
            // db.crazyTables.Single(x => x.Id == crazyTable.Id);
            if (ModelState.IsValid)
            {
                _db.CrazyTables.Add(crazyTable);
                _db.SaveChanges();
                return RedirectToAction("ShowAll");
            }

            return View(crazyTable);
        }

        // GET: crazy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrazyTable crazyTable = _db.CrazyTables.Find(id);
            if (crazyTable == null)
            {
                return HttpNotFound();
            }
            return View(crazyTable);
        }

        // POST: crazy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,crazy,crazydate")] CrazyTable crazyIn)
        {
            CrazyTable dbCrazyIn = new CrazyTable
            {
                Id = crazyIn.Id,
                Crazy = (_db.CrazyTables.Single(x => x.Id == crazyIn.Id)).Crazy
            };
            crazyIn.Crazy = dbCrazyIn.Crazy; //for when it reloads the view the value would'nt be gone
            dbCrazyIn.CrazyDate = crazyIn.CrazyDate;
            if (ModelState.IsValid)
            {
                _db.CrazyTables.AddOrUpdate(dbCrazyIn);
                _db.SaveChanges();
                return RedirectToAction("ShowAll");
            }
            return View(crazyIn);
        }

        // GET: crazy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CrazyTable crazyTable = _db.CrazyTables.Find(id);
            if (crazyTable == null)
            {
                return HttpNotFound();
            }
            return View(crazyTable);
        }

        // POST: crazy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CrazyTable crazyTable = _db.CrazyTables.Find(id);
            Debug.Assert(crazyTable != null, "crazyTable != null");
            _db.CrazyTables.Remove(crazyTable);
            _db.SaveChanges();
            return RedirectToAction("ShowAll");
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