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

namespace Ishop
{
    public class Sheet_commentsController : Controller
    {
        private Sheet_comments_context db = new Sheet_comments_context();
        private Timesheet_Context dbb = new Timesheet_Context();
        // GET: Sheet_comments
        public ActionResult Index()
        {
            return View(db.sheet_Comments.ToList());
        }

        // GET: Sheet_comments/Details/5
        
        // GET: Sheet_comments/Create
        public ActionResult Approvals(int Timesheetid)
        {
            var Timesheet = dbb.timesheets.Where(c => c.Id == Timesheetid).FirstOrDefault();
            ViewBag.Timesheet = Timesheet;
            return View();
        }

        // POST: Sheet_comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approvals([Bind(Include = "Id,Timesheeet_Id,CreatedOn,Comments,Approver")] Sheet_comments sheet_comments, string action)
        {
           

            if (ModelState.IsValid)
            {
                db.sheet_Comments.Add(sheet_comments);
                db.SaveChanges();

                var Timesheet = dbb.timesheets.Where(c => c.Id == sheet_comments.Timesheeet_Id).FirstOrDefault();

                if (action == "Approve_Timesheet")
                {

                   

                    TempData["msg"] = "Timesheet approved  successfully  -   Staff : " + Timesheet.Owner + " From Date " + Timesheet.From_Date.ToString("dd - MMMM - yyyy") + " To Date " + Timesheet.End_Date.ToString("dd - MMMM - yyyy");
                    string returnUrl = Request.UrlReferrer.ToString();
                    return RedirectToAction("ApproveTimesheet", "Timesheet", new { Id = Timesheet.Id, user = Timesheet.Owner, From_Date = Timesheet.From_Date, End_Date = Timesheet.End_Date });
                }
                else if (action == "Decline_Timesheet")
                {
                    TempData["msg"] = "Timesheet  Declined  -   Staff : " + Timesheet.Owner + " From Date " + Timesheet.From_Date.ToString("dd - MMMM - yyyy") + " To Date " + Timesheet.End_Date.ToString("dd - MMMM - yyyy");
                    return RedirectToAction("Decline", "Timesheet", new { Id = Timesheet.Id, user = Timesheet.Owner });
                }
                
            }

            return View(sheet_comments);
        }

        // GET: Sheet_comments/Edit/5
       
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
