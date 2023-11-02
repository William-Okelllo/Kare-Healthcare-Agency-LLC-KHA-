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
    public class InDirectsController : Controller
    {
        private InDirect_Context db = new InDirect_Context();

        // GET: InDirects
        public ActionResult Index()
        {
            return View(db.InDirects.ToList());
        }

        // GET: InDirects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InDirect inDirect = db.InDirects.Find(id);
            if (inDirect == null)
            {
                return HttpNotFound();
            }
            return View(inDirect);
        }

        // GET: InDirects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InDirects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedOn,Name")] InDirect inDirect)
        {
            if (ModelState.IsValid)
            {
                db.InDirects.Add(inDirect);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inDirect);
        }

        // GET: InDirects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InDirect inDirect = db.InDirects.Find(id);
            if (inDirect == null)
            {
                return HttpNotFound();
            }
            return View(inDirect);
        }

        // POST: InDirects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Name")] InDirect inDirect)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inDirect).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inDirect);
        }

        // GET: InDirects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InDirect inDirect = db.InDirects.Find(id);
            if (inDirect == null)
            {
                return HttpNotFound();
            }
            return View(inDirect);
        }

        // POST: InDirects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InDirect inDirect = db.InDirects.Find(id);
            db.InDirects.Remove(inDirect);
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
