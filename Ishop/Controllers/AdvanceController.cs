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
using Ishop.Models;
using PagedList;
using Rotativa;
using IShop.Core.Interface;

namespace Ishop.Controllers
{
    [Authorize]
    public class AdvanceController : Controller
    {
        private Advance_Context db = new Advance_Context();

        // GET: Advance
        public ActionResult Index(string searchBy, string search, int? page)
        {
            


            if (this.User.IsInRole("Admin"))
            {

                if (!(search == null))
                {
                    return View(db.Advances.OrderByDescending(p => p.id).Where(c => c.Employee == search).ToList().ToPagedList(page ?? 1, 6));

                }
                else
                {
                    return View(db.Advances.OrderByDescending(p => p.id).ToList().ToPagedList(page ?? 1, 6));


                }

            }
            else
            {
                if (!(search == null))
                {
                    return View(db.Advances.OrderByDescending(p => p.id).Where(c => c.Employee == User.Identity.Name).ToList().ToPagedList(page ?? 1, 6));

                }
                else
                {
                    return View(db.Advances.OrderByDescending(p => p.id).Where(c => c.Employee == User.Identity.Name).ToList().ToPagedList(page ?? 1, 6));


                }
            }
        }


        // GET: Advance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advance advance = db.Advances.Find(id);
            if (advance == null)
            {
                return HttpNotFound();
            }
            return View(advance);
        }

        // GET: Advance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Advance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Employee,CreatedOn,Employee_Fullnames,Amount,Amount_Words,Pay_Date,Due_to,Approved_by_Manager,Approved_Date_Manager,Approved_by_Finance,Approved_Date_Finance,Request_status")] Advance advance)
        {
            advance.Request_status = 0;
            advance.Approved_by_Manager = false;
            advance.Approved_Date_Manager = advance.CreatedOn;
            advance.Approved_by_Finance = false;
            advance.Approved_Date_Finance = advance.CreatedOn;

            if (ModelState.IsValid)
            {
                db.Advances.Add(advance);
                db.SaveChanges();
                TempData["msg"] = "Request posted successfully ";
                return RedirectToAction("Index");
            }

            return View(advance);
        }

        // GET: Advance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advance advance = db.Advances.Find(id);
            if (advance == null)
            {
                return HttpNotFound();
            }
            return View(advance);
        }

        // POST: Advance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Employee,CreatedOn,Employee_Fullnames,Amount,Amount_Words,Pay_Date,Due_to,Approved_by_Manager,Approved_Date_Manager,Approved_by_Finance,Approved_Date_Finance")] Advance advance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(advance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(advance);
        }




        // GET: Advance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advance advance = db.Advances.Find(id);
            if (advance == null)
            {
                return HttpNotFound();
            }
            return View(advance);
        }

        // POST: Advance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Advance advance = db.Advances.Find(id);
            db.Advances.Remove(advance);
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

        public ActionResult Print_Form(int id)
        {


            {
                Advance pay = db.Advances.FirstOrDefault(c => c.id == id);

                var report = new PartialViewAsPdf("~/Views/Advance/Print_Form.cshtml", pay);
                return report;
            }
        }

        public ActionResult Approve_Finance(int id)
        {

            return View();

        }
        public ActionResult Approve_Admin(int id)
        {

            return View();

        }
    }
}
