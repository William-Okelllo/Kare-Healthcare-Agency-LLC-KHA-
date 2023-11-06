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
    public class PcategoriesController : Controller
    {
        private PCategory_Context db = new PCategory_Context();

        // GET: Pcategories
        public ActionResult Index()
        {
            return View(db.pcategories.ToList());
        }

        // GET: Pcategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pcategory pcategory = db.pcategories.Find(id);
            if (pcategory == null)
            {
                return HttpNotFound();
            }
            return View(pcategory);
        }

        // GET: Pcategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pcategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Category")] Pcategory pcategory)
        {
            if (ModelState.IsValid)
            {
                db.pcategories.Add(pcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pcategory);
        }

        // GET: Pcategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pcategory pcategory = db.pcategories.Find(id);
            if (pcategory == null)
            {
                return HttpNotFound();
            }
            return View(pcategory);
        }

        // POST: Pcategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Category")] Pcategory pcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pcategory);
        }

        // GET: Pcategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pcategory pcategory = db.pcategories.Find(id);
            if (pcategory == null)
            {
                return HttpNotFound();
            }
            return View(pcategory);
        }

        // POST: Pcategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pcategory pcategory = db.pcategories.Find(id);
            db.pcategories.Remove(pcategory);
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
