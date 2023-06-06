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
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using EASendMail;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using RestSharp;

using Jose;

using Encoding = System.Text.Encoding;

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
                return View(db.applications.OrderByDescending(p => p.Application_Date).Where(c => c.Sector.StartsWith(search) || c.Sector == search && c.Applicant==User.Identity.Name).ToList().ToPagedList(page ?? 1, 11));

            }
            else if (search == "")
            {
                return View(db.applications.OrderByDescending(p => p.Application_Date).Where(c=> c.Applicant == User.Identity.Name).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(db.applications.OrderByDescending(p => p.Application_Date).Where(c => c.Applicant == User.Identity.Name).ToList()
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
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var currentUser = manager.FindById(User.Identity.GetUserId());
                try
                {
                    string message = "Hello , " + currentUser.UserName



                   + "\n" + " your application has been received successfully"

                   + "\n" + " ..."
                   + "\n" + application.Job_Title
                    + "\n" + application.Type
                     + "\n" + application.Sector
                   + "\n" + "thank you regards ";






                    sms_send(currentUser.PhoneNumber, message);
                }


                catch
                {
                    TempData["msg"] = "✔  reninder not sent "; ;

                }
                return RedirectToAction("Index");
            }

            return View(application);
        }
        public void sms_send(string recipient, string message)
        {
            string APIkey1 = System.Configuration.ConfigurationManager.AppSettings["APIkey"].ToString();
            string apiUrl = System.Configuration.ConfigurationManager.AppSettings["APIUrl"].ToString();
            string shortcode1 = System.Configuration.ConfigurationManager.AppSettings["shortcode"].ToString();
            int partnerID1 = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["partnerID"].ToString());

            using (HttpClient client = new HttpClient())
            {
                // Prepare the request body
                var requestBody = new
                {
                    apikey = APIkey1,
                    partnerID = partnerID1,
                    message = message,
                    shortcode = shortcode1,
                    mobile = recipient

                };
                var json = JsonConvert.SerializeObject(requestBody);
                try
                {
                    var response = client.PostAsync(apiUrl, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["msg"] = "SMS sent successfully.";
                    }
                    else
                    {
                        var errorResponse = response.Content.ReadAsStringAsync().Result;
                        TempData["msg"] = "Failed to send SMS : ";
                    }
                }
                catch (Exception ex)
                {
                    TempData["msg"] = "An error occurred while sending the SMS:";
                }
            }
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
            var data10 = dqb.applications.FirstOrDefault(d => d.Id == id);

            var boo9 = AA.profiles.Where(c=>c.app_user==data10.Applicant).ToList();
            ViewBag.l9 = boo9;

            var boo8 = FC.Educations.Where(c => c.App_user == data10.Applicant).ToList();
            ViewBag.l8 = boo8;


            var boo7 = FC.Experiences.Where(c => c.App_user == data10.Applicant).ToList();
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
