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
using PagedList;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Ishop.Controllers
{
    [Authorize]
    public class AdmissionController : Controller
    {
        private Admission_context db = new Admission_context();
        private static readonly HttpClient client = new HttpClient();
        // GET: Admission
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null) && (!(search == "")))
            {
                return View(db.admissions.OrderByDescending(p => p.Id).Where(c => c.Admin_No.StartsWith(search) || c.Name == search || c.Name.Contains(search)).ToList().ToPagedList(page ?? 1, 11));

            }
            else if (search == "")
            {
                return View(db.admissions.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(db.admissions.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));
            }

        }
        // GET: Admission/Details/5
        public ActionResult Profile(int? id)
        {
            Waitlist_context WC = new Waitlist_context();
            var Waitlistdata =WC.waitlists.Where(c => c.Student_Id == id);
            ViewBag.Waitlistdata = Waitlistdata;


            Training_context TC = new Training_context();
            var Trains = TC.training.Where(c => c.Student_Id == id);
            ViewBag.Trains = Trains;


            Emp_context EEM = new Emp_context();
            var Empp =EEM.emps.Where(c => c.Student_Id == id);
            ViewBag.Empp = Empp;    

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admission admission =  db.admissions.Find(id);
            if (admission == null)
            {
                return HttpNotFound();
            }
            return View(admission);
        }

        // GET: Admission/Create
        public async Task<ActionResult> Add()
        {
           
            return View();
        }

        // POST: Admission/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([Bind(Include = "Id,Name,Contacts,Email,Gender,Date,Admin_No,Address,Level_Education,Employement_Status,Em_Name,Em_Phone,Em_Mail,Comments,Text")] Admission admission)
        {






            if (ModelState.IsValid)
            {
                db.admissions.Add(admission);
                await db.SaveChangesAsync();
                TempData["msg"] = "Student Admitted successfully ";
                return RedirectToAction("Index");
            }

            return View(admission);
        }













        // GET: Admission/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admission admission = await db.admissions.FindAsync(id);
            if (admission == null)
            {
                return HttpNotFound();
            }
            return View(admission);
        }

        // POST: Admission/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Contacts,Email,Gender,Date,Admin_No,Address,Level_Education,Employement_Status,Em_Name,Em_Phone,Em_Mail,Comments,Text")] Admission admission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admission).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["msg"] = "Admission updated successfully ";
                return RedirectToAction("Index");
            }
            return View(admission);
        }

        // GET: Admission/Delete/5
        public ActionResult Delete(int? id)
        {
           
            Admission admission =  db.admissions.Find(id);
            db.admissions.Remove(admission);
             db.SaveChanges();
            TempData["msg"] = "Admission deleted successfully ";
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
