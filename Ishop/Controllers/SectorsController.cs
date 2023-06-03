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
    public class SectorsController : Controller
    {
        private sectortabb db = new sectortabb();

        // GET: Sectors
        public ActionResult Index(string option, string search, int? page)
        {
            if (!(search == null) && (!(search == "")))
            {
                return View(db.Sectorslists.OrderByDescending(p => p.CreatedOn).Where(c => c.Sector.StartsWith(search) || c.Sector == search).ToList().ToPagedList(page ?? 1, 11));

            }
            else if (search == "")
            {
                return View(db.Sectorslists.OrderByDescending(p => p.CreatedOn).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(db.Sectorslists.OrderByDescending(p => p.CreatedOn).ToList()
                    .ToPagedList(page ?? 1, 11));
            }
        }

        public ActionResult CheckValueExists(string Sector)
        {
            bool exists = db.Sectorslists.Any(c => c.Sector == Sector);
            return Json(exists, JsonRequestBehavior.AllowGet);
        }



        // GET: Sectors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sectorslist sectorslist = db.Sectorslists.Find(id);
            if (sectorslist == null)
            {
                return HttpNotFound();
            }
            return View(sectorslist);
        }

        // GET: Sectors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,CreatedOn,Sector")] Sectorslist sectorslist)
        {
            if (ModelState.IsValid)
            {
                db.Sectorslists.Add(sectorslist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sectorslist);
        }

        // GET: Sectors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sectorslist sectorslist = db.Sectorslists.Find(id);
            if (sectorslist == null)
            {
                return HttpNotFound();
            }
            return View(sectorslist);
        }

        // POST: Sectors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,CreatedOn,Sector")] Sectorslist sectorslist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sectorslist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sectorslist);
        }

        // GET: Sectors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sectorslist sectorslist = db.Sectorslists.Find(id);
            if (sectorslist == null)
            {
                return HttpNotFound();
            }
            return View(sectorslist);
        }

        // POST: Sectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sectorslist sectorslist = db.Sectorslists.Find(id);
            db.Sectorslists.Remove(sectorslist);
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
