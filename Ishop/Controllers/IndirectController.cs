using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IShop.Core;
using Ishop.Infa;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using IShop.Core.Interface;

namespace Ishop.Controllers
{
    public class IndirectController : Controller
    {
        private Indirect_context db = new Indirect_context();

        // GET: Indirect
        public async Task<ActionResult> Index()
        {
            return View(await db.indirects.ToListAsync());
        }

        // GET: Indirect/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indirect indirect = await db.indirects.FindAsync(id);
            if (indirect == null)
            {
                return HttpNotFound();
            }
            return View(indirect);
        }

        // GET: Indirect/Create
        public ActionResult Create(string Track)
        {
            Workplan_context WP = new Workplan_context();
            var WorkPlan = WP.workplans.Where(c => c.Track == Track).FirstOrDefault();
            ViewBag.WorkPlan = WorkPlan.Id;





            return PartialView("Create");
        }

            // POST: Indirect/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WorkPlan_Id,Description,Rate,Amount")] Indirect indirect)
        {
           
                db.indirects.Add(indirect);
                 db.SaveChanges();
               
            

            TempData["msg"] = "Cost added successfully ";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }

        // GET: Indirect/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Indirect indirect = await db.indirects.FindAsync(id);
            if (indirect == null)
            {
                return HttpNotFound();
            }
            return View(indirect);
        }

        // POST: Indirect/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,WorkPlan_Id,Description,Rate,Amount")] Indirect indirect)
        {
            if (ModelState.IsValid)
            {
                db.Entry(indirect).State = EntityState.Modified;
                await db.SaveChangesAsync();
               
            }
            TempData["msg"] = "Cost updated successfully ";
            Workplan_context WW = new Workplan_context();
            var Wp = WW.workplans.Find(indirect.WorkPlan_Id);
            return RedirectToAction("Budget_view", "Workplan", new { Track = Wp.Track });
        }

        // GET: Indirect/Delete/5
        public ActionResult Delete(int? id)
        {
            
            Indirect indirect =  db.indirects.Find(id);
            db.indirects.Remove(indirect);
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
