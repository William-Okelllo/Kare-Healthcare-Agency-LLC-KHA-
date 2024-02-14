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
    [Authorize]
    public class ReportAccessesController : Controller
    {
        private ReportAccess_context db = new ReportAccess_context();

        // GET: ReportAccesses
        public ActionResult Index()
        {
            return View(db.reportAccesses.ToList());
        }

        // GET: ReportAccesses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportAccess reportAccess = db.reportAccesses.Find(id);
            if (reportAccess == null)
            {
                return HttpNotFound();
            }
            return View(reportAccess);
        }

        // GET: ReportAccesses/Create
        public ActionResult Add(int id ,string Report)
        {
            ViewBag.Report = Report;
            ViewBag.ReportId = id;



            var Accessteam = db.reportAccesses.Where(c=>c.GroupId ==id).ToList();
            var Accessteamm = db.reportAccesses.Where(c => c.GroupId == id && c.Report==Report).FirstOrDefault();
            ViewBag.Accessteam = Accessteam;
            if (Accessteamm != null)
            {
                Employee_Context Ep = new Employee_Context();
                var Employee = Ep.employees.Where(e => e.Username != Accessteamm.Staff).ToList();
                ViewBag.Employee = new SelectList(Employee, "Username", "Username");
            }
            else
            {
                Employee_Context Ep = new Employee_Context();
                var Employee = Ep.employees.ToList();
                ViewBag.Employee = new SelectList(Employee, "Username", "Username");
            }

            return View();
        }

        // POST: ReportAccesses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,GrantedOn,Report,GroupId,Staff")] ReportAccess reportAccess)
        {
            if (ModelState.IsValid)
            {
                db.reportAccesses.Add(reportAccess);
                db.SaveChanges();
                TempData["msg"] = "Staff Granted Acccess successfully ";
                string returnUrl = Request.UrlReferrer.ToString();
                return Redirect(returnUrl);
            }

            return View(reportAccess);
        }

        // GET: ReportAccesses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportAccess reportAccess = db.reportAccesses.Find(id);
            if (reportAccess == null)
            {
                return HttpNotFound();
            }
            return View(reportAccess);
        }

        // POST: ReportAccesses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GrantedOn,Report,GroupId,Staff")] ReportAccess reportAccess)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reportAccess).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reportAccess);
        }

        // GET: ReportAccesses/Delete/5
        public ActionResult Delete(int? id)
        {
            ReportAccess reportAccess = db.reportAccesses.Find(id);
            db.reportAccesses.Remove(reportAccess);
            db.SaveChanges();
            TempData["msg"] = "Staff  Acccess revorked successfully ";
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
