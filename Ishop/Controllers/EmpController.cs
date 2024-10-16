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

namespace Ishop.Controllers
{
    public class EmpController : Controller
    {
        private Emp_context db = new Emp_context();

        // GET: Emp
        public ActionResult Index()
        {
            return View(db.emps.ToList());
        }

        // GET: Emp/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emp emp = db.emps.Find(id);
            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }

        // GET: Emp/Create
        public ActionResult Create(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        // POST: Emp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Student_Id,Start_Date,End_Date,Name,Address,Status,Type,Contact,Report_Date")] Emp emp)
        {
            if (ModelState.IsValid)
            {
                db.emps.Add(emp);
                db.SaveChanges();
                TempData["msg"] = "Employement Added successfully ";
            }


            return RedirectToAction("Profile", "Admission", new { Id = emp.Student_Id });
        }

        // GET: Emp/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emp emp = db.emps.Find(id);
            if (emp == null)
            {
                return HttpNotFound();
            }
            return View(emp);
        }

        // POST: Emp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Student_Id,Start_Date,End_Date,Name,Address,Status,Type,Contact,Report_Date")] Emp emp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emp).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Employement updated successfully ";
            }


            return RedirectToAction("Profile", "Admission", new { Id = emp.Student_Id });
        }

        // GET: Emp/Delete/5
        public ActionResult Delete(int? id)
        {
           
            Emp emp = db.emps.Find(id);
            db.emps.Remove(emp);
            db.SaveChanges();
            TempData["msg"] = "Employement deleted successfully ";
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
