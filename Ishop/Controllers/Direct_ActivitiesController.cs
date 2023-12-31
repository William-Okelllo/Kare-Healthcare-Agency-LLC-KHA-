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
    public class Direct_ActivitiesController : Controller
    {
        private Direct_Activities_Context db = new Direct_Activities_Context();

        // GET: Direct_Activities
        public ActionResult Index()
        {
            return View(db.direct_Activities.ToList());
        }

        // GET: Direct_Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direct_Activities direct_Activities = db.direct_Activities.Find(id);
            if (direct_Activities == null)
            {
                return HttpNotFound();
            }
            return View(direct_Activities);
        }

        // GET: Direct_Activities/Create
        public ActionResult Add(DateTime Id)
        {
            ViewBag.Date = Id;
            Direct_Context DC = new Direct_Context();
            var DirectC = DC.directs.ToList();
            ViewBag.Direct = new SelectList(DirectC, "Name", "Name");


            InDirect_Context ID = new InDirect_Context();
            var InDirect = ID.InDirects.ToList();
            ViewBag.InDirect = new SelectList(InDirect, "Name", "Name");

            Team_Context P = new Team_Context();
            var Project = P.teams.ToList();
            ViewBag.Project = new SelectList(Project, "Project_Name", "Project_Name");


            DateTime currentDate = Id;

            // Calculate the week number based on the current date
            int currentWeekNumber = GetCurrentWeekNumber(currentDate); // Make sure to provide the correct date here

            // Assign the week number directly
            int WeekNo = currentWeekNumber;

            ViewBag.SelectedDate = currentDate;
            ViewBag.Weekid = WeekNo;

            ViewBag.Weekid = WeekNo;
            return View();
        }
        private int GetCurrentWeekNumber(DateTime date)
        {
            var cal = System.Globalization.CultureInfo.CurrentCulture.Calendar;
            int week = cal.GetWeekOfYear(date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return week;
        }
        // POST: Direct_Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,Hours,Day_Date,CreatedOn,Project_Name,User,Comments,Name,Charge,Approved,WeekNo")] Direct_Activities direct_Activities)
        {
            
                db.direct_Activities.Add(direct_Activities);
                db.SaveChanges();
                TempData["msg"] = "Activity added successfully ";
                return RedirectToAction("Index", "Timesheet");
            

            return View(direct_Activities);
        }




        public ActionResult GetCharge(int id)
        {
            Employee_Context EE = new Employee_Context();
            var EmployeeRate = EE.employees.Where(e => e.Username == User.Identity.Name).FirstOrDefault();

            var data = EmployeeRate.Rate * id;

            return Json(data, JsonRequestBehavior.AllowGet);
        }





        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direct_Activities direct_Activities = db.direct_Activities.Find(id);
            if (direct_Activities == null)
            {
                return HttpNotFound();
            }
            return View(direct_Activities);
        }

        // POST: Direct_Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WeekId,Hours,Day_Date,CreatedOn,Project_Name,User,Comments,Name,Charge")] Direct_Activities direct_Activities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(direct_Activities).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(direct_Activities);
        }

        // GET: Direct_Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direct_Activities direct_Activities = db.direct_Activities.Find(id);
            if (direct_Activities == null)
            {
                return HttpNotFound();
            }
            return View(direct_Activities);
        }

        // POST: Direct_Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Direct_Activities direct_Activities = db.direct_Activities.Find(id);
            db.direct_Activities.Remove(direct_Activities);
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
