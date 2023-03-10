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
    public class RoutesController : Controller
    {
        private tickets_datasetsv2 db = new tickets_datasetsv2();

        // GET: Routes
        public ActionResult Index(string searchBy, string search, int? page)
        {


            if (!(search == null) && (!(search == "")))
            {
                return View(db.Ticketing_Routes.OrderByDescending(p => p.id).Where(c => c.Routing == search).ToList().ToPagedList(page ?? 1, 6));

            }

            else if (search == " ")
            {
                return View(db.Ticketing_Routes.OrderByDescending(p => p.id).ToList().ToPagedList(page ?? 1, 6));

            }
            else
            {
                return View(db.Ticketing_Routes.OrderByDescending(p => p.id).ToList().ToPagedList(page ?? 1, 6));

            }

        }

        // GET: Routes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticketing_Routes ticketing_Routes = db.Ticketing_Routes.Find(id);
            if (ticketing_Routes == null)
            {
                return HttpNotFound();
            }
            return View(ticketing_Routes);
        }

        // GET: Routes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Routes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Routing,CreatedOn")] Ticketing_Routes ticketing_Routes)
        {
            if (ModelState.IsValid)
            {
                db.Ticketing_Routes.Add(ticketing_Routes);
                db.SaveChanges();
                TempData["msg"] = "Route added successfully ";
                return RedirectToAction("Index");
            }

            return View(ticketing_Routes);
        }

        // GET: Routes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticketing_Routes ticketing_Routes = db.Ticketing_Routes.Find(id);
            if (ticketing_Routes == null)
            {
                return HttpNotFound();
            }
            return View(ticketing_Routes);
        }

        // POST: Routes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Routing,CreatedOn")] Ticketing_Routes ticketing_Routes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketing_Routes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticketing_Routes);
        }

        // GET: Routes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticketing_Routes ticketing_Routes = db.Ticketing_Routes.Find(id);
            if (ticketing_Routes == null)
            {
                return HttpNotFound();
            }
            return View(ticketing_Routes);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticketing_Routes ticketing_Routes = db.Ticketing_Routes.Find(id);
            db.Ticketing_Routes.Remove(ticketing_Routes);
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
