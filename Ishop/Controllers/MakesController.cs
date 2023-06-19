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
    public class MakesController : Controller
    {
        private Makes_context db = new Makes_context();

        // GET: Makes
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null))
            {
                return View(db.makes.OrderByDescending(p => p.Id).Where(c => c.Make == search || c.Make.StartsWith(search)).ToList().ToPagedList(page ?? 1, 7));

            }
            else
            {
                return View(db.makes.OrderByDescending(p => p.Id).Where(c => c.Make.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 6));
            }

        }

        // GET: Makes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makes makes = db.makes.Find(id);
            if (makes == null)
            {
                return HttpNotFound();
            }
            return View(makes);
        }

        // GET: Makes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Makes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Make,Code")] Makes makes)
        {
            if (ModelState.IsValid)
            {
                db.makes.Add(makes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(makes);
        }

        // GET: Makes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makes makes = db.makes.Find(id);
            if (makes == null)
            {
                return HttpNotFound();
            }
            return View(makes);
        }

        // POST: Makes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Make,Code")] Makes makes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(makes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(makes);
        }

        // GET: Makes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Makes makes = db.makes.Find(id);
            if (makes == null)
            {
                return HttpNotFound();
            }
            return View(makes);
        }

        // POST: Makes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Makes makes = db.makes.Find(id);
            db.makes.Remove(makes);
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
