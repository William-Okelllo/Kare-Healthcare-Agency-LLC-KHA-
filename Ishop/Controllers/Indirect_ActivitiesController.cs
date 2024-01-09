﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IShop.Core;
using Ishop.Infa;

namespace Ishop
{
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
                db.SaveChanges();
                TempData["msg"] = "Activity added successfully ";
                return RedirectToAction("Index", "Timesheet");
            

            return View(indirect_Activities);
        }

        // GET: Indirect_Activities/Edit/5
        public ActionResult Edit(int? id)
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
                return RedirectToAction("Index");
            }
            return View(indirect_Activities);
        }

        // GET: Indirect_Activities/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Indirect_Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Indirect_Activities indirect_Activities = db.indirect_Activities.Find(id);
            db.indirect_Activities.Remove(indirect_Activities);
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