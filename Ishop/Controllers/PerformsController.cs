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

namespace Ishop.Controllers
{
    public class PerformsController : Controller
    {
        private Perform_context db = new Perform_context();

        // GET: Performs
        public async Task<ActionResult> Index()
        {
            return View(await db.performs.ToListAsync());
        }

        // GET: Performs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perform perform = await db.performs.FindAsync(id);
            if (perform == null)
            {
                return HttpNotFound();
            }
            return View(perform);
        }


        public ActionResult Option()
        {
            ViewBag.Data = db.performs.OrderByDescending(c => c.Id).ToList();
            return View();
        }


        


        // GET: Performs/Create
        public ActionResult Report(int Option)
        {

            Admission_context AA = new Admission_context();
            var Admm = AA.admissions.ToList();
            ViewBag.Admm = new SelectList(Admm, "Name", "Name");

            
            ViewBag.Option= Option;


            return View();
        }

        // POST: Performs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Report([Bind(Include = "Id,Stage,Student,Date,Remarks,Percentage,Subjects,Pass")] Perform perform)
        {
            if (ModelState.IsValid)
            {
                db.performs.Add(perform);
                await db.SaveChangesAsync();
                TempData["msg"] = "Report saved successfully ";
                return RedirectToAction("Option");
            }

            return View(perform);
        }

        // GET: Performs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perform perform = await db.performs.FindAsync(id);
            if (perform == null)
            {
                return HttpNotFound();
            }
            return View(perform);
        }

        // POST: Performs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Stage,Student,Date,Remarks,Percentage,Subjects,Pass")] Perform perform)
        {
            if (ModelState.IsValid)
            {
                db.Entry(perform).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(perform);
        }

        // GET: Performs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perform perform = await db.performs.FindAsync(id);
            if (perform == null)
            {
                return HttpNotFound();
            }
            return View(perform);
        }

        // POST: Performs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Perform perform = await db.performs.FindAsync(id);
            db.performs.Remove(perform);
            await db.SaveChangesAsync();
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
