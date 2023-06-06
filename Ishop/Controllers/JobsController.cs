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
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using EASendMail;
using System.Configuration;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ishop.Controllers
{
    public class JobsController : Controller
    {
       
        ApplicationContext dqb = new ApplicationContext();
        ProfileContext AA = new ProfileContext();
        GrptabsEnt FC = new GrptabsEnt();
        
        private Job_context db = new Job_context();

        // GET: Jobs
        public ActionResult Index(string searchBy, string search, int? page)
        {

            if (!(search == null))
            {
                return View(db.Jobs.Where(c => c.Sector == search || c.Sector.StartsWith(search) && c.Posted_By==User.Identity.Name).ToList().ToPagedList(page ?? 1, 7));

            }
            else
            {
                return View(db.Jobs.Where(c => c.Posted_By ==User.Identity.Name ).ToList().ToPagedList(page ?? 1, 6));
            }

        }

        public ActionResult Open_Jobs(string searchBy, string search, int? page)
        {

            if (!(search == null))
            {
                return View(db.Jobs.Where(c => c.Sector == search || c.Sector.StartsWith(search)).ToList().ToPagedList(page ?? 1, 7));

            }
            else
            {
                return View(db.Jobs.ToList().ToPagedList(page ?? 1, 6));
            }

        }



            // GET: Jobs/Details/5
            public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Jobs/Create
        public ActionResult Post_Job()
        {
            sectortabb dbb = new sectortabb();
            var sectors = dbb.Sectorslists.ToList();
            ViewBag.sectors = new SelectList(sectors, "Sector", "Sector");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Post_Job([Bind(Include = "Id,Title,Description,Application_Deadline,Sector,Experience,Qualifications,Type,Application_Type,Salary,link_email,Show_salary,Posted_By,Responsibilites")] Job job)
        {
            if(job.Application_Type=="Internal")
            {
                job.link_email = " --";
            }

            if (ModelState.IsValid)
            {
                db.Jobs.Add(job);
                db.SaveChanges();

                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var currentUser = manager.FindById(User.Identity.GetUserId());
                try
                {
                    SmtpMail oMail = new SmtpMail("TryIt");
                    oMail.From = System.Configuration.ConfigurationManager.AppSettings["Email"].ToString();
                    oMail.To = currentUser.Email;
                    oMail.Subject = "Job Posted Successfullly ";
                    oMail.TextBody = "Hello , " + currentUser.UserName



                   + "\n" + " Ypur job has been posted successsfully"
                   
                   + "\n" + " ..."
                   + "\n" + "thank you regards ";



                    SmtpServer oServer = new SmtpServer(ConfigurationManager.AppSettings["smtp"].ToString());
                    oServer.User = System.Configuration.ConfigurationManager.AppSettings["Email"].ToString();
                    oServer.Password = System.Configuration.ConfigurationManager.AppSettings["Password"].ToString();
                    oServer.ConnectType = SmtpConnectType.ConnectTryTLS;
                    oServer.Port = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["port"].ToString());
                    SmtpClient oSmtp = new SmtpClient();
                    SmtpClientAsyncResult oResult = oSmtp.BeginSendMail(oServer, oMail, null, null);

                    


                    sms_send(currentUser.PhoneNumber, oMail.TextBody);
                }


                catch
                {
                    TempData["msg"] = "✔  reninder not sent "; ;

                }
                return RedirectToAction("Index");
            }

            return View(job);
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

        // GET: Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Application_Deadline,Sector,Experience,Industry,Qualifications,Type,Application_Type,Salary")] Job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(job);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
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
