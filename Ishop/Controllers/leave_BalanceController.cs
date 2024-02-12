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
    public class leave_BalanceController : Controller
    {
        private leaves_Days_trackContext db = new leaves_Days_trackContext();

        // GET: leave_Balance
        public ActionResult Index()
        {
            return View(db.Leaves_Days_Tracks.ToList());
        }

        // GET: leave_Balance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            leaves_Days_track leaves_Days_track = db.Leaves_Days_Tracks.Find(id);
            if (leaves_Days_track == null)
            {
                return HttpNotFound();
            }
            return View(leaves_Days_track);
        }

        // GET: leave_Balance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: leave_Balance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Total_leaves_per_year,Requested_leaves,Type,Remaining_leaves,Username")] leaves_Days_track leaves_Days_track)
        {
            if (ModelState.IsValid)
            {
                db.Leaves_Days_Tracks.Add(leaves_Days_track);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(leaves_Days_track);
        }

        // GET: leave_Balance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            leaves_Days_track leaves_Days_track = db.Leaves_Days_Tracks.Find(id);
            if (leaves_Days_track == null)
            {
                return HttpNotFound();
            }
            return View(leaves_Days_track);
        }

        // POST: leave_Balance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Total_leaves_per_year,Requested_leaves,Type,Remaining_leaves,Username")] leaves_Days_track leaves_Days_track)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leaves_Days_track).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leaves_Days_track);
        }

        // GET: leave_Balance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            leaves_Days_track leaves_Days_track = db.Leaves_Days_Tracks.Find(id);
            if (leaves_Days_track == null)
            {
                return HttpNotFound();
            }
            return View(leaves_Days_track);
        }

        // POST: leave_Balance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            leaves_Days_track leaves_Days_track = db.Leaves_Days_Tracks.Find(id);
            db.Leaves_Days_Tracks.Remove(leaves_Days_track);
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
