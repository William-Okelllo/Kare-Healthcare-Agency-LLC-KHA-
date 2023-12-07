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
    public class Days_leaveController : Controller
    {
        private IDays_leave_context db = new IDays_leave_context();

        // GET: Days_leave
        public ActionResult Index()
        {
            return View(db.days_Leaves.ToList());
        }

        // GET: Days_leave/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Days_leave days_leave = db.days_Leaves.Find(id);
            if (days_leave == null)
            {
                return HttpNotFound();
            }
            return View(days_leave);
        }

        public JsonResult CalculateDays(DateTime fromDate, DateTime toDate)
        {
            var timeDiff = toDate - fromDate;
            var daysDiff = (int)Math.Ceiling(timeDiff.TotalDays);

            return Json(new { days = daysDiff });
        }





        public ActionResult Create(int id)
        {
            leaveTypesContext dbb = new leaveTypesContext();
            var categories = dbb.leaves_Types.ToList();
            ViewBag.Categories = new SelectList(categories, "Type", "Type");
            ViewBag.leaveid = id;

            return View();
        }

        // POST: Days_leave/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Leave_Id,Approved,Type,From_Date,To_Date,Days,Time")] Days_leave days_leave)
        {
            if (ModelState.IsValid)
            {
                db.days_Leaves.Add(days_leave);
                db.SaveChanges();
                TempData["msg"] = "✔   Leave Day added successfully ";
                return RedirectToAction("Add", "leaves", new { id = days_leave.Leave_Id });
            }

            return View(days_leave);
        }

        // GET: Days_leave/Edit/5
        public ActionResult Add(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Days_leave days_leave = db.days_Leaves.Find(id);
            if (days_leave == null)
            {
                return HttpNotFound();
            }
            return View(days_leave);
        }

        // POST: Days_leave/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,Leave_Id,Approved,Type,Leave_Day,Time")] Days_leave days_leave)
        {
            if (ModelState.IsValid)
            {
                db.Entry(days_leave).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(days_leave);
        }

        // GET: Days_leave/Delete/5
        
        public ActionResult Delete(int id)
        {
            Days_leave days_leave = db.days_Leaves.Find(id);
            db.days_Leaves.Remove(days_leave);
            db.SaveChanges();
            TempData["msg"] = "✔  Days dropped successfully";
            string returnUrl = Request.UrlReferrer.ToString();
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
