using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ishop.Infa;
using Ishop.Models;

namespace Ishop.Controllers
{
    public class InterviewsController : Controller
    {
        private sp_dashprocs db = new sp_dashprocs();

        // GET: Interviews
        public ActionResult Index()
        {
            return View(db.Interviewlists.ToList());
        }

        // GET: Interviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interviewlist interviewlist = db.Interviewlists.Find(id);
            if (interviewlist == null)
            {
                return HttpNotFound();
            }
            return View(interviewlist);
        }

        // GET: Interviews/Create
        public ActionResult Create(int? id)

        {


            ProfileContext AA = new ProfileContext();
            ApplicationContext dqb = new ApplicationContext();
            var data10 = dqb.applications.OrderByDescending(p => p.Id).Where(c => c.Id == id).ToList();
            ViewBag.F = data10;




            return View();
        }

        // POST: Interviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,CreatedOn,Status,Job_Name,Sector,Type,Posted_by,Applicant,Location,Phone")] Interviewlist interviewlist)
        {
            if (ModelState.IsValid)
            {
                db.Interviewlists.Add(interviewlist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(interviewlist);
        }

        // GET: Interviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interviewlist interviewlist = db.Interviewlists.Find(id);
            if (interviewlist == null)
            {
                return HttpNotFound();
            }
            return View(interviewlist);
        }

        // POST: Interviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,CreatedOn,Status,Job_Name,Sector,Type,Posted_by,Applicant,Location,Phone")] Interviewlist interviewlist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interviewlist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(interviewlist);
        }

        // GET: Interviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Interviewlist interviewlist = db.Interviewlists.Find(id);
            if (interviewlist == null)
            {
                return HttpNotFound();
            }
            return View(interviewlist);
        }

        // POST: Interviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Interviewlist interviewlist = db.Interviewlists.Find(id);
            db.Interviewlists.Remove(interviewlist);
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
