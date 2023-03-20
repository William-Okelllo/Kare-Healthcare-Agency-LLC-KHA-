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
using Syncfusion.DocIO.DLS;

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
        [Authorize(Roles = "DashBoard")]
        public ActionResult Timesheet(string User,  string From_Date, string End_Date)
        {



            if (User == null || From_Date == "" || End_Date == "")
            {
                var startDated = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var last = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
               
                User = "All";
                From_Date = startDated.ToString();
                End_Date = last.ToString();

            }
            
                sp_times dbb = new sp_times();
                var categories = dbb.Gettimesheet(User, From_Date, End_Date).ToList();
                ViewBag.l5 = categories;
            

            return View();
        }
        public JsonResult GetEvents()
        {
            using (sp_times dc = new sp_times())
            {
                var events = dc.Gettimesheet(User.Identity.Name,"","").ToList();
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
            TT tt = db.tt.Find(id);
            if (tt == null)
            {
                return HttpNotFound();
            }
            return PartialView("Details", tt);
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
