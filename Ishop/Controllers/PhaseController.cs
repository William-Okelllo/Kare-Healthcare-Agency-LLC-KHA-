using Ishop.Infa;
using IShop.Core;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ishop.Controllers
{
    public class PhaseController : Controller
    {
        private Phase_Context db = new Phase_Context();

        // GET: Phase
        public ActionResult Index()
        {
            return View(db.phases.ToList());
        }



        /** public ActionResult Checklimit(int HouseCount, string Project_Name)
         {
             Project_Context PP = new Project_Context();
             var ProjectB  = PP.projects.Where(c => c.Project_Name == Project_Name).Select(d => d.Budget).DefaultIfEmpty(0).Sum();

             var Prjoectid = PP.projects.Where(c => c.Project_Name == Project_Name).FirstOrDefault();

             var PhasesB = db.phases.Where(c => c.Project_id == Prjoectid.Id).Select(d => d.Budget).DefaultIfEmpty(0).Sum();

             bool exists = HouseCount + Houses > HouseCount2.HousesCount;
             return Json(exists, JsonRequestBehavior.AllowGet);
         }

         **/
        // GET: Phase/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phase phase = db.phases.Find(id);
            if (phase == null)
            {
                return HttpNotFound();
            }
            return View(phase);
        }
        private Direct_Context dbb = new Direct_Context();
        public ActionResult GetPhaseId(string Name)
        {
            var data = dbb.directs.FirstOrDefault(d => d.Name == Name);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: Phase/Create
        public ActionResult Create(int id)
        {

            ViewBag.Projectid = id;

            Direct_Context DC = new Direct_Context();
            var DirectC = DC.directs.ToList();
            ViewBag.Direct = new SelectList(DirectC, "Name", "Name");
            return View();
        }

        // POST: Phase/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Project_id,Start_Date,End_Date,Name,Budget,Step")] Phase phase)
        {
            if (ModelState.IsValid)
            {
                db.phases.Add(phase);
                Budgetsum(phase.Project_id, phase.Budget);
                db.SaveChanges();
                TempData["msg"] = "✔   Phase added successfully ";
                return RedirectToAction("Details", "projects", new { id = phase.Project_id });
            }

            return View(phase);
        }

        public void Budgetsum(int Projectid, decimal Budget)
        {
            Project_Context TT = new Project_Context();
            var Sheet = TT.projects.Where(c=>c.Id == Projectid).FirstOrDefault();

            if (Sheet != null)
            {
                Sheet.Budget = Sheet.Budget + Budget;
                TT.SaveChanges();
            }
        }
        public ActionResult Edit(int? id)
        {
            Direct_Context DC = new Direct_Context();
            var DirectC = DC.directs.ToList();
            ViewBag.Direct = new SelectList(DirectC, "Name", "Name");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phase phase = db.phases.Find(id);
            if (phase == null)
            {
                return HttpNotFound();
            }
            return View(phase);
        }

        // POST: Phase/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Budget,Project_id,Name,Start_Date,End_Date,Step")] Phase phase)
        {
           
            if (ModelState.IsValid)
            {
                db.Entry(phase).State = EntityState.Modified;
                Budgetb4update(phase.Project_id, phase.Budget, phase.Id);
                db.SaveChanges();
                TempData["msg"] = "✔   Phase updated successfully ";
                return RedirectToAction("Details", "projects", new { id = phase.Project_id });
            }
            return View(phase);
        }

        public void Budgetb4update(int Projectid, decimal Budget, int Phase)
        {
            Project_Context TT = new Project_Context();
            var Sheet = TT.projects.Where(c => c.Id == Projectid).FirstOrDefault();

            Phase_Context PC = new Phase_Context();
            var Phas = PC.phases.Where(c => c.Id == Phase).FirstOrDefault();

            if (Sheet != null)
            {
                Sheet.Budget = Sheet.Budget - Phas.Budget;
                Sheet.Budget = Sheet.Budget + Budget;
                TT.SaveChanges();
            }
        }
        public ActionResult Delete(int id)
        {
            Phase phase = db.phases.Find(id);
            db.phases.Remove(phase);
            Budgetdrop(phase.Project_id, phase.Budget);
            db.SaveChanges();
            TempData["msg"] = "Phase dropped successfully ";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }

        public void Budgetdrop(int Projectid, decimal Budget)
        {
            Project_Context TT = new Project_Context();
            var Sheet = TT.projects.Where(c => c.Id == Projectid).FirstOrDefault();

            if (Sheet != null)
            {
                Sheet.Budget = Sheet.Budget - Budget;
                TT.SaveChanges();
            }
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
