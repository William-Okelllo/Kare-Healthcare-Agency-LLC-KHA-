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
    public class RecoveriesController : Controller
    {
        private Recovery_context db = new Recovery_context();

        // GET: Recoveries
        public ActionResult Index()
        {
            return View(db.recoveries.ToList());
        }

        // GET: Recoveries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recovery recovery = db.recoveries.Find(id);
            if (recovery == null)
            {
                return HttpNotFound();
            }
            return View(recovery);
        }

        // GET: Recoveries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recoveries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,CreatedOn,Staff,Inv_No,Inv_Amount,Balance")] Recovery recovery)
        {
            if (ModelState.IsValid)
            {
                db.recoveries.Add(recovery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recovery);
        }

        // GET: Recoveries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recovery recovery = db.recoveries.Find(id);
            if (recovery == null)
            {
                return HttpNotFound();
            }
            return View(recovery);
        }

        // POST: Recoveries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,CreatedOn,Staff,Inv_No,Inv_Amount,Balance")] Recovery recovery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recovery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recovery);
        }

        // GET: Recoveries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recovery recovery = db.recoveries.Find(id);
            if (recovery == null)
            {
                return HttpNotFound();
            }
            return View(recovery);
        }

        // POST: Recoveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recovery recovery = db.recoveries.Find(id);
            db.recoveries.Remove(recovery);
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
