using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IShop.Core;
using Ishop.Infa;
using IShop.Core.Interface;
using System.Data.SqlClient;

namespace Ishop.Controllers
{
    public class PhaseController : Controller
    {
        private Phase_Context db = new Phase_Context();

        // GET: Phase
        public ActionResult Index()
        {
            return View(db.phases.ToList());
        }



       /** public ActionResult Checklimit(int HouseCount, string Project_Name)
        {
            Project_Context PP = new Project_Context();
            var ProjectB  = PP.projects.Where(c => c.Project_Name == Project_Name).Select(d => d.Budget).DefaultIfEmpty(0).Sum();

            var Prjoectid = PP.projects.Where(c => c.Project_Name == Project_Name).FirstOrDefault();

            var PhasesB = db.phases.Where(c => c.Project_id == Prjoectid.Id).Select(d => d.Budget).DefaultIfEmpty(0).Sum();

            bool exists = HouseCount + Houses > HouseCount2.HousesCount;
            return Json(exists, JsonRequestBehavior.AllowGet);
        }

        **/
        // GET: Phase/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phase phase = db.phases.Find(id);
            if (phase == null)
            {
                return HttpNotFound();
            }
            return View(phase);
        }

        // GET: Phase/Create
        public ActionResult Create(int id)
        {

            ViewBag.Projectid = id;

            Direct_Context DC = new Direct_Context();
            var DirectC = DC.directs.ToList();
            ViewBag.Direct = new SelectList(DirectC, "Name", "Name");
            return View();
        }

        // POST: Phase/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Project_id,Start,End,Name,Budget")] Phase phase)
        {
            if (ModelState.IsValid)
            {
                db.phases.Add(phase);
                db.SaveChanges();
                TempData["msg"] = "✔   Phase added successfully ";
                return RedirectToAction("Details", "projects", new { id = phase.Project_id });
            }

            return View(phase);
        }

        // GET: Phase/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phase phase = db.phases.Find(id);
            if (phase == null)
            {
                return HttpNotFound();
            }
            return View(phase);
        }

        // POST: Phase/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Project_id,Start,End,Name,Budget")] Phase phase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phase);
        }

        // GET: Phase/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phase phase = db.phases.Find(id);
            if (phase == null)
            {
                return HttpNotFound();
            }
            return View(phase);
        }

        // POST: Phase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Phase phase = db.phases.Find(id);
            db.phases.Remove(phase);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
