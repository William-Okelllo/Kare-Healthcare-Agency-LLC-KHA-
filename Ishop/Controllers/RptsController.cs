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

namespace Ishop.Controllers
{
    [Authorize]
    public class RptsController : Controller
    {
        private Rpt_context db = new Rpt_context();

        // GET: Rpts
        public ActionResult Index()
        {
            return View(db.rpts.ToList());
        }

        // GET: Rpts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rpt rpt = db.rpts.Find(id);
            if (rpt == null)
            {
                return HttpNotFound();
            }
            return View(rpt);
        }

        // GET: Rpts/Create
        public ActionResult Create(int Id)
        {
            Workplan_context WWW = new Workplan_context();
            var WorkP = WWW.workplans.Find(Id);
            ViewBag.WorkP=WorkP;

            return View();
        }

        // POST: Rpts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Track,Activity,Goal,Report,Plan_Id,AddedOn,Staff")] Rpt rpt)
        {
            if (ModelState.IsValid)
            {
                db.rpts.Add(rpt);
                db.SaveChanges();
                TempData["msg"] = "Report captured   successfully ";
            }

            return RedirectToAction("WorkPlan_Reports", "Workplan", new { Id = rpt.Plan_Id });
        }

        // GET: Rpts/Edit/5
        public ActionResult Edit(int? id)
        {
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rpt rpt = db.rpts.Find(id);
            if (rpt == null)
            {
                return HttpNotFound();
            }


            Workplan_context WWW = new Workplan_context();
            var WorkP = WWW.workplans.Find(rpt.Plan_Id);
            ViewBag.WorkP = WorkP;
            return View(rpt);
        }

        // POST: Rpts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Track,Activity,Goal,Report,Plan_Id,AddedOn,Staff")] Rpt rpt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rpt).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Report updated   successfully ";
              
            }
            return RedirectToAction("WorkPlan_Reports", "Workplan", new { Id = rpt.Plan_Id });
        }

        // GET: Rpts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rpt rpt = db.rpts.Find(id);
            if (rpt == null)
            {
                return HttpNotFound();
            }
            return View(rpt);
        }

        // POST: Rpts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rpt rpt = db.rpts.Find(id);
            db.rpts.Remove(rpt);
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
