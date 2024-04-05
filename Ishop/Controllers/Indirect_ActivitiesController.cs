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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System.Configuration;
using System.Data.SqlClient;

namespace Ishop
{
    [Authorize]
    public class Indirect_ActivitiesController : Controller
    {
        private Indirect_Activities_Context db = new Indirect_Activities_Context();

        // GET: Indirect_Activities
        public ActionResult Index()
        {
            return View(db.indirect_Activities.ToList());
        }

        // GET: Indirect_Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indirect_Activities indirect_Activities = db.indirect_Activities.Find(id);
            if (indirect_Activities == null)
            {
                return HttpNotFound();
            }
            return View(indirect_Activities);
        }

        // GET: Indirect_Activities/Create
        public ActionResult Add(DateTime Id)
        {

            Timesheet_Context TT = new Timesheet_Context();
            var Sheet = TT.timesheets.Where(i => i.Owner == User.Identity.Name && Id >= i.From_Date && Id <= i.End_Date).FirstOrDefault();
            if (Sheet == null)
            {
                TempData["msg"] = "Opps : Note you have no timesheet generated for the current week ";
                return RedirectToAction("Index", "Timesheet");
            }



            ViewBag.Date = Id;
            Direct_Context DC = new Direct_Context();
            var DirectC = DC.directs.ToList();
            ViewBag.Direct = new SelectList(DirectC, "Name", "Name");


            InDirect_Context ID = new InDirect_Context();
            var InDirect = ID.InDirects.ToList();
            ViewBag.InDirect = new SelectList(InDirect, "Name", "Name");

            Project_Context P = new Project_Context();
            var Project = P.projects.ToList();
            ViewBag.Project = new SelectList(Project, "Project_Name", "Project_Name");

            DateTime currentDate = Id;
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
        // POST: Indirect_Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,Hours,Day_Date,CreatedOn,User,Comments,Name,WeekNo")] Indirect_Activities indirect_Activities)
        {
           
            db.indirect_Activities.Add(indirect_Activities);
            CaptureRecord(indirect_Activities.Hours, indirect_Activities.User, indirect_Activities.Day_Date);
            db.SaveChanges();
               
            TempData["msg"] = "Activity added successfully ";
            return RedirectToAction("Index", "Timesheet");
            

           
        }

        public ActionResult Add2(DateTime Id)
        {
            ViewBag.Date = Id;
            Direct_Context DC = new Direct_Context();
            var DirectC = DC.directs.ToList();
            ViewBag.Direct = new SelectList(DirectC, "Name", "Name");


            InDirect_Context ID = new InDirect_Context();
            var InDirect = ID.InDirects.ToList();
            ViewBag.InDirect = new SelectList(InDirect, "Name", "Name");

            Project_Context P = new Project_Context();
            var Project = P.projects.ToList();
            ViewBag.Project = new SelectList(Project, "Project_Name", "Project_Name");

            DateTime currentDate = Id;
            int currentWeekNumber = GetCurrentWeekNumber2(currentDate); // Make sure to provide the correct date here

            // Assign the week number directly
            int WeekNo = currentWeekNumber;

            ViewBag.SelectedDate = currentDate;
            ViewBag.Weekid = WeekNo;

            ViewBag.Weekid = WeekNo;
            return View();
        }
        private int GetCurrentWeekNumber2(DateTime date)
        {
            var cal = System.Globalization.CultureInfo.CurrentCulture.Calendar;
            int week = cal.GetWeekOfYear(date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return week;
        }
        // POST: Indirect_Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add2([Bind(Include = "Id,Hours,Day_Date,CreatedOn,User,Comments,Name,WeekNo")] Indirect_Activities indirect_Activities)
        {

            db.indirect_Activities.Add(indirect_Activities);
            CaptureRecord(indirect_Activities.Hours,  indirect_Activities.User, indirect_Activities.Day_Date);
            db.SaveChanges();

            TempData["msg"] = "Activity added successfully ";
            return RedirectToAction("DataView", "Timesheet", new { selectedDate = indirect_Activities.Day_Date.ToString("yyyy-MM-dd"), User = indirect_Activities.User });


        }

        public void CaptureRecord(Decimal Hours,  string Staff, DateTime datecheck)
        {


            Timesheet_Context TT = new Timesheet_Context();
            var Sheet = TT.timesheets.Where(i => i.Owner == Staff && datecheck >= i.From_Date && datecheck <= i.End_Date).FirstOrDefault();

            Indirect_Activities_Context IDC = new Indirect_Activities_Context();
            Direct_Activities_Context DA = new Direct_Activities_Context();
            Decimal Indirecttime = IDC.indirect_Activities.Where(i => i.User == Staff && i.Day_Date >= Sheet.From_Date && i.Day_Date <= Sheet.End_Date).Select(i => i.Hours).DefaultIfEmpty(0).Sum() + Hours;
            Decimal Directtime = DA.direct_Activities.Where(i => i.User == Staff && i.Day_Date >= Sheet.From_Date && i.Day_Date <= Sheet.End_Date).Select(i => i.Hours).DefaultIfEmpty(0).Sum();
            Decimal LeaveTime = TT.timesheets.Where(c => c.Id == Sheet.Id).Select(i => i.Leave).DefaultIfEmpty(0).Sum();
            Decimal Totaltime = Indirecttime + Directtime + LeaveTime;
            if (Sheet != null)
            {
                Sheet.Tt = Totaltime ;
                Sheet.Direct_Hours = Directtime;
                Sheet.InDirect_Hours = Indirecttime;
                TT.SaveChanges();
            }
        }







        // GET: Indirect_Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            Direct_Context DC = new Direct_Context();
            var DirectC = DC.directs.ToList();
            ViewBag.Direct = new SelectList(DirectC, "Name", "Name");


            InDirect_Context ID = new InDirect_Context();
            var InDirect = ID.InDirects.ToList();
            ViewBag.InDirect = new SelectList(InDirect, "Name", "Name");

            Project_Context P = new Project_Context();
            var Project = P.projects.ToList();
            ViewBag.Project = new SelectList(Project, "Project_Name", "Project_Name");

            




            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indirect_Activities indirect_Activities = db.indirect_Activities.Find(id);
            if (indirect_Activities == null)
            {
                return HttpNotFound();
            }
            return View(indirect_Activities);
        }

        // POST: Indirect_Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WeekId,Hours,Day_Date,CreatedOn,Project_Name,User,Comments,Name")] Indirect_Activities indirect_Activities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(indirect_Activities).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Activity updated successfully ";
                
            }
            return RedirectToAction("Index", "Timesheet");
        }



        public ActionResult Edit2(int? id)
        {
            Direct_Context DC = new Direct_Context();
            var DirectC = DC.directs.ToList();
            ViewBag.Direct = new SelectList(DirectC, "Name", "Name");


            InDirect_Context ID = new InDirect_Context();
            var InDirect = ID.InDirects.ToList();
            ViewBag.InDirect = new SelectList(InDirect, "Name", "Name");

            Project_Context P = new Project_Context();
            var Project = P.projects.ToList();
            ViewBag.Project = new SelectList(Project, "Project_Name", "Project_Name");






            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indirect_Activities indirect_Activities = db.indirect_Activities.Find(id);
            if (indirect_Activities == null)
            {
                return HttpNotFound();
            }
            return View(indirect_Activities);
        }

        // POST: Indirect_Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2([Bind(Include = "Id,WeekId,Hours,Day_Date,CreatedOn,Project_Name,User,Comments,Name")] Indirect_Activities indirect_Activities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(indirect_Activities).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Activity updated successfully ";

            }
            return RedirectToAction("DataView", "Timesheet", new { selectedDate = indirect_Activities.Day_Date.ToString("yyyy-MM-dd"), User = indirect_Activities.User });
        }



































        // GET: Indirect_Activities/Delete/5

        public ActionResult Delete(int id)
        {
            Indirect_Activities indirect_Activities = db.indirect_Activities.Find(id);
            db.indirect_Activities.Remove(indirect_Activities);
            RemoveCaptureRecord(indirect_Activities.Hours, indirect_Activities.User, indirect_Activities.Day_Date);
            db.SaveChanges();
            TempData["msg"] = "Activity dropped successfully ";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }


        public void RemoveCaptureRecord(Decimal time, string Staff, DateTime datecheck)
        {
            Timesheet_Context TT = new Timesheet_Context();
            var Sheet = TT.timesheets.Where(i => i.Owner == Staff && datecheck >= i.From_Date && datecheck <= i.End_Date).FirstOrDefault();

            if (Sheet != null)
            {
                Sheet.Tt = Sheet.Tt -  time;
                Sheet.InDirect_Hours = Sheet.InDirect_Hours- time;
                TT.SaveChanges();
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult InDirecttasksview(DateTime Weekday, int? page)
        {
            try
            {



                DateTime currentDate = Weekday;
                DayOfWeek dayOfWeek = currentDate.DayOfWeek;

                var data = db.indirect_Activities.Where(p => p.User == User.Identity.Name && p.Day_Date == currentDate).ToList().ToPagedList(page ?? 1, 11).ToList();


                // Handle or return data as needed
                return PartialView("InDirecttasksview", data);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error in controller action: {ex.Message}");
                var data = ex.Message;
                return PartialView("InDirecttasksview", data);
            }
        }
    }
}
