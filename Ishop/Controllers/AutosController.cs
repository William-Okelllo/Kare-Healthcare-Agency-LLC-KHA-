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
using PagedList;

namespace Ishop.Controllers
{
    public class AutosController : Controller
    {
        private AutoContext db = new AutoContext();

        // GET: Autos
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null))
            {
                return View(db.autoparts.OrderByDescending(p => p.Id).Where(c => c.Part_Name == search || c.Part_Name.StartsWith(search)).ToList().ToPagedList(page ?? 1, 7));

            }
            else
            {
                return View(db.autoparts.OrderByDescending(p => p.Id).Where(c => c.Part_Name.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 6));
            }

        }

        // GET: Autos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autoparts autoparts = db.autoparts.Find(id);
            if (autoparts == null)
            {
                return HttpNotFound();
            }
            return View(autoparts);
        }

        // GET: Autos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Autos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Part_Name,Category,CreatedOn")] Autoparts autoparts)
        {
            if (ModelState.IsValid)
            {
                db.autoparts.Add(autoparts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(autoparts);
        }

        // GET: Autos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autoparts autoparts = db.autoparts.Find(id);
            if (autoparts == null)
            {
                return HttpNotFound();
            }
            return View(autoparts);
        }

        // POST: Autos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Part_Name,Category,CreatedOn")] Autoparts autoparts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autoparts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autoparts);
        }

        // GET: Autos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autoparts autoparts = db.autoparts.Find(id);
            if (autoparts == null)
            {
                return HttpNotFound();
            }
            return View(autoparts);
        }

        // POST: Autos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Autoparts autoparts = db.autoparts.Find(id);
            db.autoparts.Remove(autoparts);
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
