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
using IShop.Core.Interface;
using System.Reflection;
using Ishop.Models;
using System.EnterpriseServices.CompensatingResourceManager;

namespace Ishop.Controllers
{
    public class ActivitiesController : Controller
    {
        private Activities_Context db = new Activities_Context();

        // GET: Activities
        public ActionResult Index()
        {
            return View(db.activities.ToList());
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {



            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activities activities = db.activities.Find(id);
            if (activities == null)
            {
                return HttpNotFound();
            }
            return View(activities);
        }

        // GET: Activities/Create
        public ActionResult Add(int Id,string id2)
        {
            ViewBag.Timesheetid = Id;
            ViewBag.Weekday = id2;

            Direct_Context DC = new Direct_Context();
            var DirectC = DC.directs.ToList();
            ViewBag.Direct = new SelectList(DirectC, "Name", "Name");


            InDirect_Context ID = new InDirect_Context();
            var InDirect = ID.InDirects.ToList();
            ViewBag.InDirect = new SelectList(InDirect, "Name", "Name");

            Project_Context P = new Project_Context();
            var Project = P.projects.ToList();
            ViewBag.Project = new SelectList(Project, "Project_Name", "Project_Name");


            
            var Activities = db.activities.Where(a => a.TimesheetId == Id && a.Day ==id2).ToList();
            ViewBag.Activities = Activities;



            var SumHours = db.activities.Where(c => c.TimesheetId == Id && c.Day == id2).Select(d => d.Hours).DefaultIfEmpty(0).Sum();
            ViewBag.SumHours = SumHours;



            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,CreatedOn,Project_Name,User,Name,TimesheetId,Hours,Comments,Day,Charge,Indirect,Indirect_Hours")] Activities activities)
        {
            if(activities.Indirect == null) { activities.Indirect = ""; }

            Employee_Context EE = new Employee_Context();
            var Emp = EE.employees.Where(c => c.Username == activities.User).FirstOrDefault();

            Decimal Charge = Emp.Rate * activities.Hours;
            activities.Charge= Charge;

            var SumHours = db.activities.Where(c => c.TimesheetId == activities.TimesheetId && c.Day == activities.Day).Select(d => d.Hours).DefaultIfEmpty(0).Sum();
            

            if (ModelState.IsValid)
            {
                db.activities.Add(activities);
                db.SaveChanges();
                Timesheet_Context Tc = new Timesheet_Context();
                Timesheet check = Tc.timesheets.Find(activities.TimesheetId);
                if (check != null)
                {
                    // Use reflection to update the specified day's column
                    var property = check.GetType().GetProperty(activities.Day, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    if (property != null)
                    {
                        int indirectHours = activities.Indirect_Hours != null ? activities.Indirect_Hours : 0;
                        property.SetValue(check, activities.Hours + indirectHours + SumHours);

                        // Save the changes to the database
                        check.Tt = check.Sun + check.Mon + check.Tue + check.Wen + check.Thur + check.Fri + check.Sat;
                        Tc.SaveChanges();
                    }
                    else
                    {
                        // Handle the case when the specified day doesn't exist as a column
                        // You can throw an exception, log an error, or handle it according to your needs.
                    }
                }

                    return RedirectToAction("Index", "Timesheet");



            }

            return View(activities);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activities activities = db.activities.Find(id);
            if (activities == null)
            {
                return HttpNotFound();
            }
            return View(activities);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Project_Name,User,Name,TimesheetId,Hours,Comments")] Activities activities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activities).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activities);
        }

        // GET: Activities/Delete/5
       
       
        public ActionResult Delete(int id,string Day)
        {
            Activities activities = db.activities.Find(id);

            var Act = db.activities.Where(c => c.Id ==id).Select(x => x.Hours);
            var TimesheetId = db.activities.Where(c => c.Id == id).FirstOrDefault();
            Timesheet_Context TC = new Timesheet_Context();




            if (TC != null)
            {
                // Use reflection to update the specified day's column
                var property = TC.GetType().GetProperty(Day, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property != null)
                {
                    property.SetValue(TC, Act);

                    // Save the changes to the database
                    TC.SaveChanges();
                }
                else
                {
                    // Handle the case when the specified day doesn't exist as a column
                    // You can throw an exception, log an error, or handle it according to your needs.
                }
            }
            db.activities.Remove(activities);
            db.SaveChanges();
            string returnUrl = Request.UrlReferrer.ToString();
            TempData["msg"] = "✔   Activity dropped successfully ";
            return Redirect(returnUrl);
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
