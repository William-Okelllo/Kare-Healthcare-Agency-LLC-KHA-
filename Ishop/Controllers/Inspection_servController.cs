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
    [Authorize]
    public class Inspection_servController : Controller
    {
        private Inspec_serv_context db = new Inspec_serv_context();

        // GET: Inspection_serv
        public ActionResult Index()
        {
            return View(db.Inspection_Servs.ToList());
        }

        // GET: Inspection_serv/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspection_serv inspection_serv = db.Inspection_Servs.Find(id);
            if (inspection_serv == null)
            {
                return HttpNotFound();
            }
            return View(inspection_serv);
        }

        // GET: Inspection_serv/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inspection_serv/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Booking_Id,Service_Name,Estimate_Totals,Amount,Total_Amount,VAT")] Inspection_serv inspection_serv)
        {
            if (ModelState.IsValid)
            {
                db.Inspection_Servs.Add(inspection_serv);
                db.SaveChanges();
                TempData["msg"] = "Service updated successfully";
                return RedirectToAction("Index");
            }

            return View(inspection_serv);
        }

        // GET: Inspection_serv/Edit/5
        public ActionResult Edit(int? id, int id2)
        {
            ViewBag.id2 = id2;


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspection_serv inspection_serv = db.Inspection_Servs.Find(id);
            if (inspection_serv == null)
            {
                return HttpNotFound();
            }
            return View(inspection_serv);
        }

        // POST: Inspection_serv/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Booking_Id,Service_Name,Estimate_Totals,Amount,Total_Amount,VAT")] Inspection_serv inspection_serv, int id2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inspection_serv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Inspect", "Inspections", new { id = id2 });
            }
            return View(inspection_serv);
        }

        // GET: Inspection_serv/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspection_serv inspection_serv = db.Inspection_Servs.Find(id);
            if (inspection_serv == null)
            {
                return HttpNotFound();
            }
            return View(inspection_serv);
        }

        // POST: Inspection_serv/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inspection_serv inspection_serv = db.Inspection_Servs.Find(id);
            db.Inspection_Servs.Remove(inspection_serv);
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
