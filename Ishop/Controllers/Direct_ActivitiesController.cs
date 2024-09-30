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
using Microsoft.Owin;
using IShop.Core.Interface;
using System.Configuration;
using System.Data.SqlClient;

namespace Ishop.Controllers
{
    [Authorize]
    public class Direct_ActivitiesController : Controller
    {
        private Direct_Activities_Context db = new Direct_Activities_Context();

        // GET: Direct_Activities
        public ActionResult Index()
        {
            return View(db.direct_Activities.ToList());
        }

        

        public ActionResult Directtasksview(DateTime Weekday, int? page)
        {
            try
            {
                DateTime currentDate = Weekday;
                DayOfWeek dayOfWeek = currentDate.DayOfWeek;

                var data = db.direct_Activities.Where(p => p.User == User.Identity.Name && p.Day_Date ==currentDate ).ToList().ToPagedList(page ?? 1, 11).ToList();
                 
                return PartialView("Directtasksview", data);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error in controller action: {ex.Message}");
                var data = ex.Message;
                return PartialView("Directtasksview", data);
            }
        }


       


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
        private Time_cat_context TC = new Time_cat_context();
        public ActionResult Getinfo(string Part)
        {


            var data = TC.time_Cats.FirstOrDefault(d => d.Name == Part);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
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

            Time_cat_context TC = new Time_cat_context();
            var Timecategory = TC.time_Cats.ToList();
            ViewBag.Timec = new SelectList(Timecategory, "Name", "Name");

         


          


            DateTime currentDate = Id;

            // Calculate the week number based on the current date
            int currentWeekNumber = GetCurrentWeekNumber(currentDate); // Make sure to provide the correct date here

            // Assign the week number directly
            int WeekNo = currentWeekNumber;

            ViewBag.SelectedDate = currentDate;
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
        public ActionResult Add([Bind(Include = "Id,Hours,Day_Date,CreatedOn,Project_Name,User,Comments,Name,Charge,Approved,WeekNo,Category")] Direct_Activities direct_Activities)
        {
            Employee_Context EE = new Employee_Context();
            var EmployeeRate = EE.employees.Where(e => e.Username == direct_Activities.User).FirstOrDefault();
            direct_Activities.Charge = direct_Activities.Hours * EmployeeRate.Rate;


              db.direct_Activities.Add(direct_Activities);
              db.SaveChanges();
              CaptureRecord(direct_Activities.User, direct_Activities.Day_Date);
              TempData["msg"] = "Activity added successfully ";
              return RedirectToAction("Index", "Timesheet");
            

           
        }



        public ActionResult Add2(DateTime Id)
        {




            ViewBag.Date = Id;

            Time_cat_context TC = new Time_cat_context();
            var Timecategory = TC.time_Cats.ToList();
            ViewBag.Timec = new SelectList(Timecategory, "Name", "Name");




            DateTime currentDate = Id;

            // Calculate the week number based on the current date
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
        // POST: Direct_Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add2([Bind(Include = "Id,Hours,Day_Date,CreatedOn,Project_Name,User,Comments,Name,Charge,Approved,WeekNo,Category")] Direct_Activities direct_Activities)
        {
            Employee_Context EE = new Employee_Context();
            var EmployeeRate = EE.employees.Where(e => e.Username == direct_Activities.User).FirstOrDefault();
            direct_Activities.Charge = direct_Activities.Hours * EmployeeRate.Rate;
            db.direct_Activities.Add(direct_Activities);
            db.SaveChanges();
            CaptureRecord(direct_Activities.User, direct_Activities.Day_Date);
            TempData["msg"] = "Activity added successfully ";
            return RedirectToAction("DataView", "Timesheet", new { selectedDate = direct_Activities.Day_Date.ToString("yyyy-MM-dd") ,User= direct_Activities.User });


            
        }










        public void CaptureRecord( string Staff, DateTime datecheck)
        {


            Timesheet_Context TT = new Timesheet_Context();
            var Sheet = TT.timesheets.Where(i => i.Owner == Staff && datecheck >= i.From_Date && datecheck <= i.End_Date).FirstOrDefault();

            Indirect_Activities_Context IDC = new Indirect_Activities_Context();
            Direct_Activities_Context DA = new Direct_Activities_Context();
            Decimal Indirecttime = IDC.indirect_Activities.Where(i => i.User == Staff && i.Day_Date >= Sheet.From_Date && i.Day_Date <= Sheet.End_Date).Select(i => i.Hours).DefaultIfEmpty(0).Sum();
            Decimal Directtime = DA.direct_Activities.Where(i => i.User == Staff && i.Day_Date >= Sheet.From_Date && i.Day_Date <= Sheet.End_Date).Select(i => i.Hours).DefaultIfEmpty(0).Sum();
            Decimal LeaveTime = TT.timesheets.Where(c => c.Id == Sheet.Id).Select(i => i.Leave).DefaultIfEmpty(0).Sum();
            Decimal Totaltime = Indirecttime + Directtime + LeaveTime;
            if (Sheet != null)
            {
                Sheet.Tt = Totaltime;
                Sheet.Direct_Hours = Directtime;
                Sheet.InDirect_Hours = Indirecttime;
                TT.SaveChanges();
            }
        }





        public ActionResult GetCharge(Decimal id)
        {
           Employee_Context EE = new Employee_Context();
            var EmployeeRate = EE.employees.Where(e => e.Username == User.Identity.Name).FirstOrDefault();

            var data = EmployeeRate.Rate * id;

            return Json(data, JsonRequestBehavior.AllowGet);
        }





        public ActionResult Edit(int? id)
        {
            Direct_Context DC = new Direct_Context();
            var DirectC = DC.directs.ToList();
            ViewBag.Direct = new SelectList(DirectC, "Name", "Name");


            InDirect_Context ID = new InDirect_Context();
            var InDirect = ID.InDirects.ToList();
            ViewBag.InDirect = new SelectList(InDirect, "Name", "Name");

           
           

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
                TempData["msg"] = "Activity updated successfully ";
                return RedirectToAction("Index", "Timesheet");
            }
            return View(direct_Activities);
        }


        public ActionResult Edit2(int? id)
        {
            Direct_Context DC = new Direct_Context();
            var DirectC = DC.directs.ToList();
            ViewBag.Direct = new SelectList(DirectC, "Name", "Name");


            InDirect_Context ID = new InDirect_Context();
            var InDirect = ID.InDirects.ToList();
            ViewBag.InDirect = new SelectList(InDirect, "Name", "Name");


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
        public ActionResult Edit2([Bind(Include = "Id,WeekId,Hours,Day_Date,CreatedOn,Project_Name,User,Comments,Name,Charge")] Direct_Activities direct_Activities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(direct_Activities).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Activity updated successfully ";
                return RedirectToAction("DataView", "Timesheet", new { selectedDate = direct_Activities.Day_Date.ToString("yyyy-MM-dd"), User = direct_Activities.User });
            }
            return View(direct_Activities);
        }


        public ActionResult Delete(int id)
        {
            Direct_Activities direct_Activities = db.direct_Activities.Find(id);
            db.direct_Activities.Remove(direct_Activities);
            RemoveCaptureRecord(direct_Activities.Hours, direct_Activities.User, direct_Activities.Day_Date);
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
                Sheet.Tt = Sheet.Tt - time;
                Sheet.InDirect_Hours = Sheet.InDirect_Hours - time;
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
    }
}
