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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Ishop.Controllers
{
    public class Direct_costController : Controller
    {
        private Direct_cost_context db = new Direct_cost_context();

        // GET: Direct_cost
        public ActionResult Index()
        {
            return View(db.direct_Costs.ToList());
        }

        // GET: Direct_cost/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direct_cost direct_cost = db.direct_Costs.Find(id);
            if (direct_cost == null)
            {
                return HttpNotFound();
            }
            return View(direct_cost);
        }

        // GET: Direct_cost/Create
        public ActionResult Create(string Track)
        {
            Workplan_context WP = new Workplan_context();
            var WorkPlan = WP.workplans.Where(c => c.Track == Track).FirstOrDefault();
            ViewBag.WorkPlan = WorkPlan.Id;





            return PartialView("Create");
        }

        // POST: Direct_cost/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WorkPlan_Id,Description,Quantity,Unit,Total")] Direct_cost direct_cost)
        {
            if (ModelState.IsValid)
            {
                db.direct_Costs.Add(direct_cost);
                db.SaveChanges();
                
            }

            TempData["msg"] = "Cost added successfully ";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }

        // GET: Direct_cost/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Direct_cost direct_cost = db.direct_Costs.Find(id);
            if (direct_cost == null)
            {
                return HttpNotFound();
            }
            return View(direct_cost);
        }

        // POST: Direct_cost/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WorkPlan_Id,Description,Quantity,Unit,Total")] Direct_cost direct_cost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(direct_cost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(direct_cost);
        }

        // GET: Direct_cost/Delete/5
        public ActionResult Delete(int? id)
        {
           
            Direct_cost direct_cost = db.direct_Costs.Find(id);
            db.direct_Costs.Remove(direct_cost);
            db.SaveChanges();
            TempData["msg"] = "Cost dropped successfully ";
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
