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
using System.Configuration;
using System.Data.SqlClient;
using EASendMail;
using Ishop.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Web.UI.WebControls;

namespace Ishop.Controllers
{
    public class ConfirmController : Controller
    {
        private ConfirmContext db = new ConfirmContext();

        // GET: Confirm
        public ActionResult Index()
        {
            return View(db.confirms.ToList());
        }
        public ActionResult Present()
        {
            return View();
        }

        // GET: Confirm/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Confirm confirm = db.confirms.Find(id);
            if (confirm == null)
            {
                return HttpNotFound();
            }
            return View(confirm);
        }

        // GET: Confirm/Create
        public ActionResult Accept(int id)
        {
            ViewBag.Event_Id = id;

            EventsContext EEE = new EventsContext();
            var Data = EEE.events.Where(c => c.Id == id);
            ViewBag.Data = Data;
            return View();
        }
        public ActionResult CheckValueExists(string Phone)
        {PeopleContext PPP = new PeopleContext();

            bool exists = PPP.peoples.Any(c => c.Phone == Phone);
            return Json(exists, JsonRequestBehavior.AllowGet);
        }
        // POST: Confirm/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Accept([Bind(Include = "Id,Event_Id,Name,Phone,Email,Spouse,Children,Children_No,Spouse_Name,Spouse_Phone")] Confirm confirm)
        {

            bool exists = db.confirms.Where(c=>c.Event_Id ==confirm.Event_Id).Any(c => c.Phone == confirm.Phone);
            if(exists ==true)
            {
                return RedirectToAction("Present");
            }





            if (ModelState.IsValid)
            {
                db.confirms.Add(confirm);
                db.SaveChanges();

                PeopleContext PPP = new PeopleContext();
                var results = PPP.peoples.Where(c => c.Phone == confirm.Phone && c.Event_Id ==confirm.Event_Id).FirstOrDefault();
                    var Acc = PPP.peoples.Find(results.Id);
                    if (Acc != null)
                    {
                        Acc.Confirmed_Attendance = true;
                        PPP.SaveChanges();
                    }
                    
               if(!(confirm.Spouse_Name == null))
                {
                    InsertIntoTable(confirm.Event_Id, confirm.Spouse_Name, confirm.Spouse_Phone, true);
                }

                EventsContext EEE = new EventsContext();
                var result = EEE.events.Where(e => e.Id == confirm.Event_Id).FirstOrDefault();

               

                try
                {

                    SmtpMail oMail = new SmtpMail("TryIt");
                    oMail.From = System.Configuration.ConfigurationManager.AppSettings["Email"].ToString();

                    oMail.To = confirm.Email;
                    oMail.Subject = "Confirmed Attendance";

                    oMail.TextBody = "Hello , " + confirm.Name


                        + "\n"
                            + "\n" + " Thanky you for your attendance confirmation, "
                   + "\n" + "see the event Details below"
                   + "\n" + "...... ......... ........"
                   + "\n" + "Event :" + result.Name
                   + "\n" + ""
                   + "\n" + "Event Date : " + result.Event_date.ToString("dd-MMMM-yyyy")
                   + "\n" + ""
                   + "\n" + "Event Venue : " + result.Venue
                   + "\n" + ""
                   + "\n" + "See you there"
                   + "\n" + "  -------------------------------- "
                   + "\n" + "  thank you ";

                    SmtpServer oServer = new SmtpServer(ConfigurationManager.AppSettings["smtp"].ToString());
                    oServer.User = System.Configuration.ConfigurationManager.AppSettings["Email"].ToString();
                    oServer.Password = System.Configuration.ConfigurationManager.AppSettings["Password"].ToString();

                    oServer.ConnectType = SmtpConnectType.ConnectTryTLS;
                    oServer.Port = 587;
                    SmtpClient oSmtp = new SmtpClient();
                    SmtpClientAsyncResult oResult = oSmtp.BeginSendMail(oServer, oMail, null, null);
                }


                catch
                {
                    TempData["msg"] = "error  ";

                }
                return RedirectToAction("Index");
            }

            return View(confirm);
        }



        



        private string connectionString = ConfigurationManager.ConnectionStrings["GRS"].ConnectionString;
        public void InsertIntoTable(int Event_Id,string Name,string Phone,bool Confirmed_Attendance)
        {
            string query = "INSERT INTO People (Event_Id,Name,Phone,Confirmed_Attendance) VALUES " +
            "                                   (@Event_Id,@Name,@Phone,@Confirmed_Attendance)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Event_Id", Event_Id);
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@Confirmed_Attendance", Confirmed_Attendance);
                    
                    command.ExecuteNonQuery();
                }
            }
        }
        // GET: Confirm/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Confirm confirm = db.confirms.Find(id);
            if (confirm == null)
            {
                return HttpNotFound();
            }
            return View(confirm);
        }

        // POST: Confirm/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Event_Id,Name,Phone,Email,Spouse,Children,Children_No,Spouse_Name,Spouse_Phone")] Confirm confirm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(confirm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(confirm);
        }

        // GET: Confirm/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Confirm confirm = db.confirms.Find(id);
            if (confirm == null)
            {
                return HttpNotFound();
            }
            return View(confirm);
        }

        // POST: Confirm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Confirm confirm = db.confirms.Find(id);
            db.confirms.Remove(confirm);
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
