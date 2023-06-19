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
    public class Inspections_partsController : Controller
    {
        private Inspections_partsContext db = new Inspections_partsContext();

        // GET: Inspections_parts
        public ActionResult Index()
        {
            return View(db.inspections_Parts.ToList());
        }

        // GET: Inspections_parts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspections_parts inspections_parts = db.inspections_Parts.Find(id);
            if (inspections_parts == null)
            {
                return HttpNotFound();
            }
            return View(inspections_parts);
        }

        // GET: Inspections_parts/Create

      
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inspections_parts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Booking_Id,Part_Name,VAT,Condition,Estimate_Totals,Amount,Total_Amount")] Inspections_parts inspections_parts)
        {
            if (ModelState.IsValid)
            {
                db.inspections_Parts.Add(inspections_parts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inspections_parts);
        }

        // GET: Inspections_parts/Edit/5
        public ActionResult Edit(int? id ,int id2)
        {
            ViewBag.id2 = id2;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspections_parts inspections_parts = db.inspections_Parts.Find(id);
            if (inspections_parts == null)
            {
                return HttpNotFound();
            }
            return View(inspections_parts);
        }

        // POST: Inspections_parts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Booking_Id,Part_Name,VAT,Condition,Estimate_Totals,Amount,Total_Amount")] Inspections_parts inspections_parts ,int id2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inspections_parts).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Part updated successfully";
                return RedirectToAction("Inspect", "Inspections", new { id = id2 });
            }
            return View(inspections_parts);
        }

        // GET: Inspections_parts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspections_parts inspections_parts = db.inspections_Parts.Find(id);
            if (inspections_parts == null)
            {
                return HttpNotFound();
            }
            return View(inspections_parts);
        }

        // POST: Inspections_parts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inspections_parts inspections_parts = db.inspections_Parts.Find(id);
            db.inspections_Parts.Remove(inspections_parts);
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
