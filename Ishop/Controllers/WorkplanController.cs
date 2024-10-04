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
using PagedList;
using System.Windows.Forms;

namespace Ishop.Controllers
{
    public class WorkplanController : Controller
    {
        private Workplan_context db = new Workplan_context();

        // GET: Workplan
        public ActionResult Index(string searchBy, string search, int? page)
        {


            if (!(search == null))
            {
                return View(db.workplans.OrderByDescending(p => p.Id).Where(c => c.Track == search).ToList().ToPagedList(page ?? 1, 6));

            }
            else
            {
                return View(db.workplans.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 6));


            }
        }

        public ActionResult Budget(int? page)
        {
            // Grouping by Track and projecting distinct Workplans
            var Workplans = db.workplans
                .GroupBy(c => c.Track)
                .Select(g => g.FirstOrDefault()) // Get the first workplan from each group
                .ToList()
                .ToPagedList(page ?? 1, 6); // Paginate the results

            ViewBag.Workplans = Workplans;
            return View();
        }

        public ActionResult Budget_view(string Track)
        {
            var WorkPlan = db.workplans.Where(c => c.Track == Track).FirstOrDefault();

            ViewBag.WorkPlan=WorkPlan;

            Direct_cost_context DC =new Direct_cost_context();
            var DirectCost =DC.direct_Costs.Where(c=>c.WorkPlan_Id == WorkPlan.Id).ToList();
            ViewBag.DirectCost = DirectCost;
            ViewBag.DirectCostsum = DC.direct_Costs.Where(c => c.WorkPlan_Id == WorkPlan.Id).Select(c => c.Total).DefaultIfEmpty(0).Sum();

            Indirect_context ID = new Indirect_context();
            var Indirectt = ID.indirects.Where(c => c.WorkPlan_Id == WorkPlan.Id).ToList();
            ViewBag.Indirectt = Indirectt;
            ViewBag.IndirectCostsum = ID.indirects.Where(c => c.WorkPlan_Id == WorkPlan.Id).Select(c => c.Amount).DefaultIfEmpty(0).Sum();

            ViewBag.Allcost = (DC.direct_Costs.Where(c => c.WorkPlan_Id == WorkPlan.Id).Select(c => c.Total).DefaultIfEmpty(0).Sum()) + (ID.indirects.Where(c => c.WorkPlan_Id == WorkPlan.Id).Select(c => c.Amount).DefaultIfEmpty(0).Sum());

            return View();
        }



            // GET: Workplan/Details/5
            public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workplan workplan = db.workplans.Find(id);
            if (workplan == null)
            {
                return HttpNotFound();
            }
            return View(workplan);
        }

        // GET: Workplan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Workplan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Track,Goal,Activities,Guide,Respondent")] Workplan workplan)
        {
            if (ModelState.IsValid)
            {
                db.workplans.Add(workplan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workplan);
        }

        // GET: Workplan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workplan workplan = db.workplans.Find(id);
            if (workplan == null)
            {
                return HttpNotFound();
            }
            return View(workplan);
        }

        // POST: Workplan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Track,Goal,Activities,Guide,Respondent")] Workplan workplan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workplan).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Data updated  successfully ";
                return RedirectToAction("Index");
            }
            return View(workplan);
        }

        // GET: Workplan/Delete/5
        public ActionResult Delete(int? id)
        {
            
            Workplan workplan = db.workplans.Find(id);
            db.workplans.Remove(workplan);
            db.SaveChanges();
            TempData["msg"] = "Data delete successfully ";
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
