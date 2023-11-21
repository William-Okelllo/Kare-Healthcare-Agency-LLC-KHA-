using Ishop.Infa;
using IShop.Core;
using PagedList;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ishop.Controllers
{
    public class ReportGroupController : Controller
    {
        private ReportGroupContext db = new ReportGroupContext();

        // GET: ReportGroup
        public ActionResult Index(string searchBy, string search, int? page)
        {


            if (!(search == null))
            {
                return View(db.ReportGroups.ToList().ToPagedList(page ?? 1, 10));

            }
            else
            {
                return View(db.ReportGroups.ToList().ToPagedList(page ?? 1, 10));
            }




        }

        // GET: ReportGroup/Details/5
        public ActionResult Details(int? id)
        {
            ReportContext RRR = new ReportContext();
            var data = RRR.Reports.Where(c => c.Group_id == id).ToList();
            ViewBag.ReportsData = data;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportGroup reportGroup = db.ReportGroups.Find(id);
            if (reportGroup == null)
            {
                return HttpNotFound();
            }
            return View(reportGroup);
        }

        // GET: ReportGroup/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReportGroup/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Description,Type,Menu_order")] ReportGroup reportGroup)
        {
            if (ModelState.IsValid)
            {
                db.ReportGroups.Add(reportGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reportGroup);
        }

        // GET: ReportGroup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportGroup reportGroup = db.ReportGroups.Find(id);
            if (reportGroup == null)
            {
                return HttpNotFound();
            }
            return View(reportGroup);
        }

        // POST: ReportGroup/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Description,Type,Menu_order")] ReportGroup reportGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reportGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reportGroup);
        }

        // GET: ReportGroup/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportGroup reportGroup = db.ReportGroups.Find(id);
            if (reportGroup == null)
            {
                return HttpNotFound();
            }
            return View(reportGroup);
        }

        // POST: ReportGroup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReportGroup reportGroup = db.ReportGroups.Find(id);
            db.ReportGroups.Remove(reportGroup);
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
