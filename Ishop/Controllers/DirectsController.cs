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
    public class DirectsController : Controller
    {
        private Direct_Context db = new Direct_Context();

        // GET: Directs
        public ActionResult Index()
        {
            return View(db.directs.ToList());
        }

        // GET: Directs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direct direct = db.directs.Find(id);
            if (direct == null)
            {
                return HttpNotFound();
            }
            return View(direct);
        }

        // GET: Directs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Directs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedOn,Name")] Direct direct)
        {
            if (ModelState.IsValid)
            {
                db.directs.Add(direct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(direct);
        }

        // GET: Directs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direct direct = db.directs.Find(id);
            if (direct == null)
            {
                return HttpNotFound();
            }
            return View(direct);
        }

        // POST: Directs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Name")] Direct direct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(direct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(direct);
        }

        // GET: Directs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direct direct = db.directs.Find(id);
            if (direct == null)
            {
                return HttpNotFound();
            }
            return View(direct);
        }

        // POST: Directs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Direct direct = db.directs.Find(id);
            db.directs.Remove(direct);
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
