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
    public class Time_catController : Controller
    {
        private Time_cat_context db = new Time_cat_context();

        // GET: Time_cat
        public ActionResult Index(string searchBy, string search, int? page)
        {


            if (!(search == null))
            {
                return View(db.time_Cats.OrderBy(p => p.Id).Where(c => c.Name == search).ToList().ToPagedList(page ?? 1, 12));

            }
            else
            {
                return View(db.time_Cats.OrderBy(p => p.Id).ToList().ToPagedList(page ?? 1, 12));


            }
        }

        // GET: Time_cat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Time_cat time_cat = db.time_Cats.Find(id);
            if (time_cat == null)
            {
                return HttpNotFound();
            }
            return View(time_cat);
        }

        // GET: Time_cat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Time_cat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedOn,Name,Description")] Time_cat time_cat)
        {
            if (ModelState.IsValid)
            {
                db.time_Cats.Add(time_cat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(time_cat);
        }

        // GET: Time_cat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Time_cat time_cat = db.time_Cats.Find(id);
            if (time_cat == null)
            {
                return HttpNotFound();
            }
            return View(time_cat);
        }

        // POST: Time_cat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Name,Description")] Time_cat time_cat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(time_cat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(time_cat);
        }

        // GET: Time_cat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Time_cat time_cat = db.time_Cats.Find(id);
            if (time_cat == null)
            {
                return HttpNotFound();
            }
            return View(time_cat);
        }

        // POST: Time_cat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Time_cat time_cat = db.time_Cats.Find(id);
            db.time_Cats.Remove(time_cat);
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
