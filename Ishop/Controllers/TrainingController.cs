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
    public class TrainingController : Controller
    {
        private Training_context db = new Training_context();

        // GET: Training
        public ActionResult Index()
        {
            return View(db.training.ToList());
        }

        // GET: Training/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = db.training.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            return View(training);
        }

        // GET: Training/Create
        public ActionResult Create(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        // POST: Training/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Start_Date,End_Date,School,Status,Paid,Tuition_Pay_Date,Amount,Certification_Status,Certification_Test_Result,Student_Id")] Training training)
        {
            if (ModelState.IsValid)
            {
                db.training.Add(training);
                db.SaveChanges();
                TempData["msg"] = "Training Added successfully ";
            }


            return RedirectToAction("Profile", "Admission", new { Id = training.Student_Id });
        }

        // GET: Training/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = db.training.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            return View(training);
        }

        // POST: Training/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Start_Date,End_Date,School,Status,Paid,Tuition_Pay_Date,Amount,Certification_Status,Certification_Test_Result,Student_Id")] Training training)
        {
            if (ModelState.IsValid)
            {
                db.Entry(training).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Training Updated successfully ";
            }


            return RedirectToAction("Profile", "Admission", new { Id = training.Student_Id });
        }

        // GET: Training/Delete/5
        public ActionResult Delete(int? id)
        {
           
            Training training = db.training.Find(id);
            db.training.Remove(training);
            db.SaveChanges();
            TempData["msg"] = "Training deleted successfully ";
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
