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
using System.Configuration;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using Application = IShop.Core.Application;
using Ishop.Models;

namespace Ishop.Controllers
{
    [Authorize]

    public class ApplicationsController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        // GET: Applications
        public ActionResult Index(string option, string search, int? page)
        {
            if (!(search == null) && (!(search == "")))
            {
                return View(db.applications.OrderByDescending(p => p.Application_Date).Where(c => c.Sector.StartsWith(search) || c.Sector == search).ToList().ToPagedList(page ?? 1, 11));

            }
            else if (search == "")
            {
                return View(db.applications.OrderByDescending(p => p.Application_Date).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(db.applications.OrderByDescending(p => p.Application_Date).ToList()
                    .ToPagedList(page ?? 1, 11));
            }
        }

        public ActionResult Employer(string option, string search, int? page)
        {
            
                return View(db.applications.OrderByDescending(p => p.Application_Date).Where(c => c.Posted_By == User.Identity.Name).ToList().ToPagedList(page ?? 1, 11));

            
            
        }





        // GET: Applications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Applications/Create
        public ActionResult Create(int Id)
        {
            Job_context dbbb = new Job_context();
            var data10 = dbbb.Jobs.OrderByDescending(p => p.Id).Where(c => c.Id == Id).ToList();
            ViewBag.F = data10;


           

            return View();
        }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Job_id,Job_Title,Application_Date,Applicant,Application_Deadline,Sector,Experience,Qualifications,Type,Application_Type,Salary,link_email,Posted_By")] Application application)
        {

            Job_context dbbb = new Job_context();
            var data10 = dbbb.Jobs.FirstOrDefault(d => d.Id ==application.Job_id );

            application.Application_Deadline = data10.Application_Deadline;
            application.Sector = data10.Sector;
            application.Experience = data10.Experience;
            application.Qualifications = data10.Qualifications;
            application.Type = data10.Type;
            application.Salary = data10.Salary;
            application.Application_Type = data10.Application_Type;
            application.link_email = data10.link_email;
            application.Show_salary = data10.Show_salary;
            application.Posted_By=data10.Posted_By;

            if (ModelState.IsValid)
            {
                db.applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(application);
        }

        // GET: Applications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Job_id,Job_Title,Application_Date,Applicant")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(application);
        }

        // GET: Applications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.applications.Find(id);
            db.applications.Remove(application);
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

        public ActionResult Cancel_Application(int? id)
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["Job_Villa"].ConnectionString;
                SqlConnection sqlCon = new SqlConnection(strcon);
                SqlCommand sqlcmnd = new SqlCommand("sp_cancel_application", sqlCon);
                sqlcmnd.CommandType = CommandType.StoredProcedure;
                sqlcmnd.Parameters.AddWithValue("@Id", id);
                sqlCon.Open();
                sqlcmnd.ExecuteNonQuery();
                sqlCon.Close();
                TempData["msg"] = "Application cancelled successfully ";
                string returnUrl = Request.UrlReferrer.ToString();
                return Redirect(returnUrl);


            }
            catch
            {
                TempData["msg"] = "error occured on cancelling application ";
                string returnUrl = Request.UrlReferrer.ToString();
                return Redirect(returnUrl);
            }


        }

        public ActionResult Applicant_details(int? id)

        {

            ViewBag.MyId = id;

            Job_context dbbb = new Job_context();
            ApplicationContext dqb = new ApplicationContext();
            ProfileContext AA = new ProfileContext();
            GrptabsEnt FC =new GrptabsEnt();

            
            var boo9 = AA.profiles.ToList();
            ViewBag.l9 = boo9;

            var boo8 = FC.Educations.ToList();
            ViewBag.l8 = boo8;


            var boo7 = FC.Experiences.ToList();
            ViewBag.l7 = boo7;

            var boo6 = dbbb.Jobs.ToList();
            ViewBag.l6 = boo6;


            filetab dbb = new filetab();
            var boo5 = dbb.Files.ToList();
            ViewBag.l5 = boo5;
            return View();
        }

    }
}
