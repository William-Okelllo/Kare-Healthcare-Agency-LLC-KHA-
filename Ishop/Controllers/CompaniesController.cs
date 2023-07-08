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
    public class CompaniesController : Controller
    {
        private InsuaranceContext db = new InsuaranceContext();

        // GET: Companies
        public ActionResult Index()
        {
            return View(db.insuarances.ToList());
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuarance insuarance = db.insuarances.Find(id);
            if (insuarance == null)
            {
                return HttpNotFound();
            }
            return View(insuarance);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedOn,Company")] Insuarance insuarance)
        {
            if (ModelState.IsValid)
            {
                db.insuarances.Add(insuarance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuarance);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuarance insuarance = db.insuarances.Find(id);
            if (insuarance == null)
            {
                return HttpNotFound();
            }
            return View(insuarance);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Company")] Insuarance insuarance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insuarance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuarance);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuarance insuarance = db.insuarances.Find(id);
            if (insuarance == null)
            {
                return HttpNotFound();
            }
            return View(insuarance);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuarance insuarance = db.insuarances.Find(id);
            db.insuarances.Remove(insuarance);
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
