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
    public class Service_providersController : Controller
    {
        private tickets_datasetsv2 db = new tickets_datasetsv2();

        // GET: Service_providers
        public ActionResult Index(string searchBy, string search, int? page)
        {


            if (!(search == null) && (!(search == "")))
            {
                return View(db.Ticketing_service_providers.OrderByDescending(p => p.id).Where(c => c.Service_Provider == search).ToList().ToPagedList(page ?? 1, 6));

            }

            else if (search == " ")
            {
                return View(db.Ticketing_service_providers.OrderByDescending(p => p.id).ToList().ToPagedList(page ?? 1, 50));

            }
            else
            {
                return View(db.Ticketing_service_providers.OrderByDescending(p => p.id).ToList().ToPagedList(page ?? 1, 50));

            }

        }

        // GET: Service_providers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticketing_service_providers ticketing_service_providers = db.Ticketing_service_providers.Find(id);
            if (ticketing_service_providers == null)
            {
                return HttpNotFound();
            }
            return View(ticketing_service_providers);
        }

        // GET: Service_providers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Service_providers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Service_Provider,CreatedOn")] Ticketing_service_providers ticketing_service_providers)
        {
            if (ModelState.IsValid)
            {
                db.Ticketing_service_providers.Add(ticketing_service_providers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticketing_service_providers);
        }

        // GET: Service_providers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticketing_service_providers ticketing_service_providers = db.Ticketing_service_providers.Find(id);
            if (ticketing_service_providers == null)
            {
                return HttpNotFound();
            }
            return View(ticketing_service_providers);
        }

        // POST: Service_providers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Service_Provider,CreatedOn")] Ticketing_service_providers ticketing_service_providers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketing_service_providers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticketing_service_providers);
        }

        // GET: Service_providers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticketing_service_providers ticketing_service_providers = db.Ticketing_service_providers.Find(id);
            if (ticketing_service_providers == null)
            {
                return HttpNotFound();
            }
            return View(ticketing_service_providers);
        }

        // POST: Service_providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticketing_service_providers ticketing_service_providers = db.Ticketing_service_providers.Find(id);
            db.Ticketing_service_providers.Remove(ticketing_service_providers);
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
