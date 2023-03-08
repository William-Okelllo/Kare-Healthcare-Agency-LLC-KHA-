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
    public class Ticketing_ClientsController : Controller
    {
        private tickets_datasetsv2 db = new tickets_datasetsv2();

        // GET: Ticketing_Clients
        public ActionResult Index(string searchBy, string search, int? page)
        {


            if (!(search == null) && (!(search == "")))
            {
                return View(db.Ticketing_Clients.OrderByDescending(p => p.id).Where(c => c.Client == search).ToList().ToPagedList(page ?? 1, 10));

            }

            else if (search == " ")
            {
                return View(db.Ticketing_Clients.OrderByDescending(p => p.id).ToList().ToPagedList(page ?? 1, 10));

            }
            else
            {
                return View(db.Ticketing_Clients.OrderByDescending(p => p.id).ToList().ToPagedList(page ?? 1, 10));

            }

        }

        // GET: Ticketing_Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticketing_Clients ticketing_Clients = db.Ticketing_Clients.Find(id);
            if (ticketing_Clients == null)
            {
                return HttpNotFound();
            }
            return View(ticketing_Clients);
        }

        // GET: Ticketing_Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ticketing_Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Client,CreatedOn")] Ticketing_Clients ticketing_Clients)
        {
            if (ModelState.IsValid)
            {
                db.Ticketing_Clients.Add(ticketing_Clients);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticketing_Clients);
        }

        // GET: Ticketing_Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticketing_Clients ticketing_Clients = db.Ticketing_Clients.Find(id);
            if (ticketing_Clients == null)
            {
                return HttpNotFound();
            }
            return View(ticketing_Clients);
        }

        // POST: Ticketing_Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Client,CreatedOn")] Ticketing_Clients ticketing_Clients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketing_Clients).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticketing_Clients);
        }

        // GET: Ticketing_Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticketing_Clients ticketing_Clients = db.Ticketing_Clients.Find(id);
            if (ticketing_Clients == null)
            {
                return HttpNotFound();
            }
            return View(ticketing_Clients);
        }

        // POST: Ticketing_Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticketing_Clients ticketing_Clients = db.Ticketing_Clients.Find(id);
            db.Ticketing_Clients.Remove(ticketing_Clients);
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
