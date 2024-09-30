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

namespace Ishop.Controllers
{
    public class WorkplanController : Controller
    {
        private Workplan_context db = new Workplan_context();

        // GET: Workplan
        public ActionResult Index()
        {
            return View(db.workplans.ToList());
        }

        // GET: Workplan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workplan workplan = db.workplans.Find(id);
            if (workplan == null)
            {
                return HttpNotFound();
            }
            return View(workplan);
        }

        // GET: Workplan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Workplan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Track,Goal,Activities")] Workplan workplan)
        {
            if (ModelState.IsValid)
            {
                db.workplans.Add(workplan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workplan);
        }

        // GET: Workplan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workplan workplan = db.workplans.Find(id);
            if (workplan == null)
            {
                return HttpNotFound();
            }
            return View(workplan);
        }

        // POST: Workplan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Track,Goal,Activities")] Workplan workplan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workplan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workplan);
        }

        // GET: Workplan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workplan workplan = db.workplans.Find(id);
            if (workplan == null)
            {
                return HttpNotFound();
            }
            return View(workplan);
        }

        // POST: Workplan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Workplan workplan = db.workplans.Find(id);
            db.workplans.Remove(workplan);
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
