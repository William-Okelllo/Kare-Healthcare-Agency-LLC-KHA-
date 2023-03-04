using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Ishop.Models;
using Microsoft.Reporting.WebForms;
using Report = Ishop.Models.Report;

namespace Ishop.Controllers
{
    public class ReportsController : Controller
    {
        private Reports_ db = new Reports_();

        // GET: Reports
        public ActionResult Index()
        {
            return View(db.Reports.ToList());
        }

        public ActionResult Run( string Path)
        {
            string ssrsUrl = ConfigurationManager.AppSettings["SSRSReportsUrl"].ToString();
            ReportViewer viewer = new ReportViewer();

            viewer.ProcessingMode = ProcessingMode.Remote;
            viewer.SizeToReportContent = false;
            viewer.AsyncRendering = true;
            viewer.ServerReport.ReportServerUrl = new Uri(ssrsUrl);
            viewer.ZoomMode = ZoomMode.PageWidth;
            viewer.Width = Unit.Percentage(100);
            viewer.Height = Unit.Pixel(900);
            viewer.BackColor = System.Drawing.Color.White;
            viewer.BorderColor = System.Drawing.Color.White;
            viewer.DocumentMapWidth = Unit.Percentage(100);
            viewer.SplitterBackColor = System.Drawing.Color.White;

            viewer.ServerReport.ReportPath = Path;
            ViewBag.ReportViewer = viewer;
            return View();
           
        }









        // GET: Reports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Report_Name,Report_Path")] Report report)
        {
            if (report.Report_Name == null || report.Report_Path == null)
            {
                TempData["msg"] = "Kindly provide the missing field ";
                return RedirectToAction("create");
            }
            if (ModelState.IsValid)
            {
                db.Reports.Add(report);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(report);
        }

        // GET: Reports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

       
        public ActionResult Edit([Bind(Include = "id,Report_Name,Report_Path")] Report report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(report);
        }

        // GET: Reports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Report report = db.Reports.Find(id);
            db.Reports.Remove(report);
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
