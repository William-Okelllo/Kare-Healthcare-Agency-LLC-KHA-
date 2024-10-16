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

    [Authorize]
    public class WaitlistController : Controller
    {
        private Waitlist_context db = new Waitlist_context();

        // GET: Waitlist
        public ActionResult Index()
        {
            return View(db.waitlists.ToList());
        }

        // GET: Waitlist/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waitlist waitlist = db.waitlists.Find(id);
            if (waitlist == null)
            {
                return HttpNotFound();
            }
            return View(waitlist);
        }

        // GET: Waitlist/Create
        public ActionResult Add(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        // POST: Waitlist/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,Student_Id,Application_Date,Course,Status,Addmision_Date,Comments")] Waitlist waitlist)
        {
            if (ModelState.IsValid)
            {
                db.waitlists.Add(waitlist);
                db.SaveChanges();
                TempData["msg"] = "Waitlist Added successfully ";
            }


            return RedirectToAction("Profile", "Admission", new { Id = waitlist.Student_Id });
        }

        // GET: Waitlist/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waitlist waitlist = db.waitlists.Find(id);
            if (waitlist == null)
            {
                return HttpNotFound();
            }
            return View(waitlist);
        }

        // POST: Waitlist/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Student_Id,Application_Date,Course,Status,Addmision_Date,Comments")] Waitlist waitlist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(waitlist).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Waitlist updated successfully ";
            }
            return RedirectToAction("Profile", "Admission", new { Id = waitlist.Student_Id });
        }

        // GET: Waitlist/Delete/5
        public ActionResult Delete(int? id)
        {
            
            Waitlist waitlist = db.waitlists.Find(id);
            db.waitlists.Remove(waitlist);
            db.SaveChanges();
            TempData["msg"] = "Waitlist deleted successfully ";
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
