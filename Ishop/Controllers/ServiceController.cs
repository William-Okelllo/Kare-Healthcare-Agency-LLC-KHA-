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
    public class ServiceController : Controller
    {
        private ServiceContext db = new ServiceContext();

        // GET: Service
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null))
            {
                return View(db.Sevs.OrderByDescending(p => p.Id).Where(c => c.Service_Name == search || c.Service_Name.StartsWith(search)).ToList().ToPagedList(page ?? 1, 7));

            }
            else
            {
                return View(db.Sevs.OrderByDescending(p => p.Id).Where(c => c.Service_Name.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 6));
            }

        }

        // GET: Service/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sev sev = db.Sevs.Find(id);
            if (sev == null)
            {
                return HttpNotFound();
            }
            return View(sev);
        }

        // GET: Service/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Service/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Service_Name,CreatedOn,Taxable")] Sev sev)
        {
            if (ModelState.IsValid)
            {
                db.Sevs.Add(sev);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sev);
        }

        // GET: Service/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sev sev = db.Sevs.Find(id);
            if (sev == null)
            {
                return HttpNotFound();
            }
            return View(sev);
        }

        // POST: Service/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Service_Name,CreatedOn,Taxable")] Sev sev)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sev).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sev);
        }

        // GET: Service/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sev sev = db.Sevs.Find(id);
            if (sev == null)
            {
                return HttpNotFound();
            }
            return View(sev);
        }

        // POST: Service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sev sev = db.Sevs.Find(id);
            db.Sevs.Remove(sev);
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
