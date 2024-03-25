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
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.Web.UI.WebControls;
using PagedList;


[Authorize]
public class ReportsController : Controller
{
    private ReportContext db = new ReportContext();
    private ReportGroupContext dbb = new ReportGroupContext();


    public ActionResult Visual ()
    {
        return View();
    }

    public ActionResult Index(string searchBy, string search, int? page)
    {

            ReportAccess_context DD = new ReportAccess_context();
            var userDepartments = DD.reportAccesses.Where(D => D.Staff == User.Identity.Name).Select(d => d.GroupId).ToList();


        return View(dbb.ReportGroups.OrderByDescending(p => p.Menu_order).Where(c => userDepartments.Contains(c.id)).ToList().ToPagedList(page ?? 1, 20));
    }
    public ActionResult Create(int id)
    {
        ViewBag.id = id;
        return View();
    }

    // POST: Reports/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "id,Group_id,Report_Name,Report_Path")] Reports reports)
    {
        if (ModelState.IsValid)
        {
            db.Reports.Add(reports);
            db.SaveChanges();
            TempData["msg"] = "Report added successfullly";
            return RedirectToAction("Details", "ReportGroup", new { id = reports.Group_id });
        }

        return View(reports);
    }
    public ActionResult Run(string Path)
    {
        try
        {
            string ssrsUrl = ConfigurationManager.AppSettings["SSRSReportsUrl"].ToString();
            ReportViewer viewer = new ReportViewer();

            // Set SSRS server URL
            viewer.ProcessingMode = ProcessingMode.Remote;
            viewer.ServerReport.ReportServerUrl = new Uri(ssrsUrl);

            // Report settings
            viewer.ServerReport.ReportPath = Path;
            viewer.ZoomMode = ZoomMode.PageWidth;
            viewer.SizeToReportContent = false;

            // Viewer appearance settings
            viewer.Width = Unit.Percentage(100);
            viewer.Height = Unit.Pixel(900);
            viewer.BackColor = System.Drawing.Color.White;
            viewer.BorderColor = System.Drawing.Color.White;
            viewer.DocumentMapWidth = Unit.Percentage(100);
            viewer.SplitterBackColor = System.Drawing.Color.White;

            // Async rendering
            viewer.AsyncRendering = true;

            // Adjust the width of the parameter area (in pixels)
            viewer.ServerReport.ReportServerUrl = new Uri(ssrsUrl);
            viewer.ServerReport.ReportPath = Path;

            viewer.ServerReport.Refresh();

            ViewBag.ReportViewer = viewer;
            return View();
        }
        catch (Exception ex)
        {
            // Handle/report any exceptions
            ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
            return View("Error");
        }
    }


}