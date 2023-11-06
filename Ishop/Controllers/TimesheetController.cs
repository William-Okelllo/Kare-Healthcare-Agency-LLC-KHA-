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
using System.Web.UI.WebControls.WebParts;

namespace Ishop
{
    [Authorize]
    public class TimesheetController : Controller
    {
        private Timesheet_Context db = new Timesheet_Context();

        // GET: Timesheet
        public ActionResult Index(string searchBy, string search, int? page)
        {




            if (!(search == null) && (!(search == "")))
            {
                return View(db.timesheets.OrderByDescending(p => p.Id).Where(c => c.Owner.StartsWith(search) || c.Owner == search).ToList().ToPagedList(page ?? 1, 11));

            }
            else if (search == "")
            {
                return View(db.timesheets.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(db.timesheets.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));
            }

        }



        public JsonResult GetEvents()
        {
            using (Timesheet_Context dc = new Timesheet_Context())
            {
                var events = dc.timesheets.ToList().Where(c => c.Owner == User.Identity.Name).ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }








        // GET: Timesheet/Details/5
        public ActionResult Details(int? id)
        {
            Activities_Context AA = new Activities_Context();
            var Activities = AA.activities.Where(a=>a.TimesheetId ==id).ToList();
            ViewBag.Activities = Activities;


            
            var SumHours = AA.activities.Where(c=>c.TimesheetId ==id).Select(d => d.Hours).DefaultIfEmpty(0).Sum();
            ViewBag.SumHours = SumHours;


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }

        // GET: Timesheet/Create
        public ActionResult Create()
        {
            Direct_Context DC = new Direct_Context();
            var DirectC = DC.directs.ToList();
            ViewBag.Direct = new SelectList(DirectC, "Name", "Name");

            Project_Context P = new Project_Context();
            var Project = P.projects.ToList();
            ViewBag.Project = new SelectList(Project, "Project_Name", "Project_Name");


            return View();
        }

        // POST: Timesheet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedOn,Owner")] Timesheet timesheet, string action)
        {
           
            if (ModelState.IsValid)
            {
                db.timesheets.Add(timesheet);
                db.SaveChanges(); 
                TempData["msg"] = "✔   Timesheet successfully captured";
                return RedirectToAction("Details", "Timesheet", new { id = timesheet.Id });
            }

            return View(timesheet);
        }

        // GET: Timesheet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }

        // POST: Timesheet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Project_Name,Name,hours,Comments,Status,Owner")] Timesheet timesheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timesheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timesheet);
        }

        // GET: Timesheet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timesheet timesheet = db.timesheets.Find(id);
            if (timesheet == null)
            {
                return HttpNotFound();
            }
            return View(timesheet);
        }

        // POST: Timesheet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Timesheet timesheet = db.timesheets.Find(id);
            db.timesheets.Remove(timesheet);
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
