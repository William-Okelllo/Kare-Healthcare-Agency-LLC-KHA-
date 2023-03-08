using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ishop.Models;
using PagedList;

namespace Ishop.Controllers
{
    public class AirlinesController : Controller
    {
        private tickets_datasetsv2 db = new tickets_datasetsv2();

        // GET: Airlines
        public ActionResult Index(string searchBy, string search, int? page)
        {


            if (!(search == null) && (!(search == "")))
            {
                return View(db.Ticketing_Airlines.OrderByDescending(p => p.id).Where(c => c.Airline == search).ToList().ToPagedList(page ?? 1, 6));

            }

            else if (search == " ")
            {
                return View(db.Ticketing_Airlines.OrderByDescending(p => p.id).ToList().ToPagedList(page ?? 1, 6));

            }
            else
            {
                return View(db.Ticketing_Airlines.OrderByDescending(p => p.id).ToList().ToPagedList(page ?? 1, 6));

            }

        }

                // GET: Airlines/Details/5
                public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticketing_Airlines airline = db.Ticketing_Airlines.Find(id);
            if (airline == null)
            {
                return HttpNotFound();
            }
            return View(airline);
        }

        // GET: Airlines/Create
        public ActionResult Create()
        {
            return View();
        }

        


        // POST: Airlines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Airline,CreatedOn")] Ticketing_Airlines ticketing_Airlines)
        {
            if (ModelState.IsValid)
            {
                db.Ticketing_Airlines.Add(ticketing_Airlines);
                db.SaveChanges();
                TempData["msg"] = "Airline added successfully ";
                return RedirectToAction("Index");
            }

            return View(ticketing_Airlines);
        }

        // GET: Airlines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticketing_Airlines airline = db.Ticketing_Airlines.Find(id);
            if (airline == null)
            {
                return HttpNotFound();
            }
            return View(airline);
        }

        // POST: Airlines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Airline1,CreatedOn")] Ticketing_Airlines airline)
        {
            if (ModelState.IsValid)
            {
                db.Entry(airline).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(airline);
        }

        // GET: Airlines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticketing_Airlines airline = db.Ticketing_Airlines.Find(id);
            if (airline == null)
            {
                return HttpNotFound();
            }
            return View(airline);
        }

        // POST: Airlines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticketing_Airlines airline = db.Ticketing_Airlines.Find(id);
            db.Ticketing_Airlines.Remove(airline);
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
