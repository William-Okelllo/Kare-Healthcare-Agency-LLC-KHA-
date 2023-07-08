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
    public class CoverController : Controller
    {
        private CoverContext db = new CoverContext();

        // GET: Cover
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null))
            {
                return View(db.covers.OrderByDescending(p => p.Id).Where(c => c.Insuarance == search || c.Insuarance.StartsWith(search)).ToList().ToPagedList(page ?? 1, 7));

            }
            else
            {
                return View(db.covers.OrderByDescending(p => p.Id).Where(c => c.Insuarance.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 6));
            }

        }

        // GET: Cover/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cover cover = db.covers.Find(id);
            if (cover == null)
            {
                return HttpNotFound();
            }
            return View(cover);
        }

        // GET: Cover/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cover/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedOn,Insuarance")] Cover cover)
        {
            if (ModelState.IsValid)
            {
                db.covers.Add(cover);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cover);
        }

        // GET: Cover/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cover cover = db.covers.Find(id);
            if (cover == null)
            {
                return HttpNotFound();
            }
            return View(cover);
        }

        // POST: Cover/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Insuarance")] Cover cover)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cover).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cover);
        }

        // GET: Cover/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cover cover = db.covers.Find(id);
            if (cover == null)
            {
                return HttpNotFound();
            }
            return View(cover);
        }

        // POST: Cover/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cover cover = db.covers.Find(id);
            db.covers.Remove(cover);
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
