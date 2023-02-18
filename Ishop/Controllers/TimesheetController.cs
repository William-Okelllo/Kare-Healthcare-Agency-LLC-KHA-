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
using Ishop.Models;

namespace Ishop.Controllers
{
    [Authorize]
    public class TimesheetController : Controller
    {
        private TT_Context db = new TT_Context();

        // GET: Timesheet
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null))
            {
                return View(db.tt.OrderByDescending(p => p.Id).Where(c => c.Employee == User.Identity.Name).ToList().ToPagedList(page ?? 1, 6));

            }
            else
            {
                return View(db.tt.OrderByDescending(p => p.Id).Where(c => c.Employee == User.Identity.Name).ToList().ToPagedList(page ?? 1, 6));


            }
        }
        public JsonResult GetEvents()
        {
            using (timesheet dc = new timesheet())
            {
                var events = dc.Gettimesheet(User.Identity.Name).ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        // GET: Timesheet/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TT bp = db.tt.Find(id);
            if (bp == null)
            {
                return HttpNotFound();
            }
            return PartialView("Details", bp);
        }
        // GET: Timesheet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Timesheet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Employee,CreatedOn,Description,Project,Status")] TT tT)
        {
            if (ModelState.IsValid)
            {
                db.tt.Add(tT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tT);
        }

        // GET: Timesheet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TT tT = db.tt.Find(id);
            if (tT == null)
            {
                return HttpNotFound();
            }
            return View(tT);
        }

        // POST: Timesheet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Employee,CreatedOn,Description,Project,Status")] TT tT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tT);
        }

        // GET: Timesheet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TT tT = db.tt.Find(id);
            if (tT == null)
            {
                return HttpNotFound();
            }
            return View(tT);
        }

        // POST: Timesheet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TT tT = db.tt.Find(id);
            db.tt.Remove(tT);
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
