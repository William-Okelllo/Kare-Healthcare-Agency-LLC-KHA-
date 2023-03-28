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
using Ishop.Models;
using Syncfusion.DocIO.DLS;
using System.Configuration;
using System.Data.SqlClient;
using EASendMail;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace Ishop.Controllers
{
    [Authorize]
    public class TimesheetController : Controller
    {
        private TT_Context db = new TT_Context();

        // GET: Timesheet
        public ActionResult Index(string searchBy, string search, int? page)
        {
            
                if (!(search == null))
                {
                    return View(db.tt.OrderByDescending(p => p.Id).Where(c => c.Employee == User.Identity.Name).ToList().ToPagedList(page ?? 1, 6));

                }
                else
                {
                    return View(db.tt.OrderByDescending(p => p.Id).Where(c => c.Employee == User.Identity.Name).ToList().ToPagedList(page ?? 1, 6));


                
            }

        }
        public JsonResult GetEvents()
        {
            using (calt2 dc = new calt2())
            {
                var events = dc.Gettimesheet2(User.Identity.Name).ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
        public ActionResult list(string searchBy, string search, int? page,TT tT)
        {
            TTresponses dbb = new TTresponses();
            var data10 = dbb.Timesheet_replies.Where(c => c.Sheetid==tT.Id).ToList();
            ViewBag.F = data10;

            if (!(search == null))
            {
                return View(db.tt.OrderByDescending(p => p.Id).Where(c => c.Employee == User.Identity.Name && c.Project.StartsWith(search) ).ToList().ToPagedList(page ?? 1, 6));

            }
            else
            {
                return View(db.tt.OrderByDescending(p => p.Id).Where(c => c.Employee == User.Identity.Name).ToList().ToPagedList(page ?? 1, 6));



            }

        }






        [Authorize(Roles = "DashBoard")]
        public ActionResult Timesheet(string searchBy, string search, string Employees, int? page)
        {
            Employee_Context dbbb = new Employee_Context();
            var Employ = dbbb.Employees.ToList();
            ViewBag.Employees = new SelectList(Employ, "Username", "Username");

            if (!(search == null || search ==""))
            {
                return View(db.tt.OrderByDescending(p => p.Id).Where(c => c.Employee == search).ToList().ToPagedList(page ?? 1, 9));

            }
            else
            {
                return View(db.tt.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 9));

            }


           

         
        }
       

        // GET: Timesheet/Details/5
        public ActionResult Details(int id)
        {

            TTresponses dbb = new TTresponses();
            var data10 = dbb.Timesheet_replies.Where(c => c.Sheetid == id).ToList();
            ViewBag.F = data10;


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TT tt = db.tt.Find(id);
            if (tt == null)
            {
                return HttpNotFound();
            }
            return PartialView("Details", tt);
        }
        // GET: Timesheet/Create
        public ActionResult Create()
        {
            Projects_table1 dbb = new Projects_table1();
            var categories = dbb.Projects.ToList();
            ViewBag.Categories = new SelectList(categories, "Project1", "Project1");



            return View();
        }

        // POST: Timesheet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Employee,CreatedOn,Description,Project,Status")] TT tT)
        {
            if (ModelState.IsValid)
            {
                db.tt.Add(tT);
                db.SaveChanges();


                try
                {


                    SmtpMail oMail = new SmtpMail("TryIt");
                    oMail.From = System.Configuration.ConfigurationManager.AppSettings["Email"].ToString();
                    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    var currentUser = manager.FindById(User.Identity.GetUserId());

                    oMail.To = System.Configuration.ConfigurationManager.AppSettings["Businesssmail"].ToString();
                    oMail.Subject = tT.Employee +" Timesheet posted";
                    
                    oMail.TextBody = "The following are the the timesheet details"
                       
                        +"\n" 
                         + "   Client/Project  :- " + tT.Project
                    + "\n" + "   Duities actioned  " + "\n"
                    + "\n" + tT.Description  ;

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
                    TempData["msg"] = "error sending email notification";
                    
                }
                TempData["msg"] = "Timesheet posted Successfully";
                return RedirectToAction("list");
            }

            return View(tT);
        }

        public ActionResult Reply(int? id)
        {

            TT_Context bd= new TT_Context();
            var Data = bd.tt.ToList().Where(c => c.Id==id );
            ViewBag.Data = Data;



            return View();
        }

        // POST: Timesheet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reply([Bind(Include = "Id,feedback,Project")] TT tT)
        {
           
                try
                {
                    string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
                    SqlConnection sqlCon = new SqlConnection(strcon);
                    SqlCommand sqlcmnd = new SqlCommand("sp_sheet_reply", sqlCon);
                    sqlcmnd.CommandType = CommandType.StoredProcedure;
                    sqlcmnd.Parameters.AddWithValue("@Id", tT.Id);
                    sqlcmnd.Parameters.AddWithValue("@feedback", tT.feedback);
                    sqlcmnd.Parameters.AddWithValue("@User", User.Identity.Name);
                    sqlCon.Open();
                    sqlcmnd.ExecuteNonQuery();
                    sqlCon.Close();



                }
                catch
                {

                }
            try
            {
                string Employee_Email = null;

                string constr = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(constr))
                {

                    using (SqlCommand cmd2 = conn.CreateCommand())
                    {
                        cmd2.Connection = conn;
                        cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.Open();
                        cmd2.CommandText = "Getmail_TT";
                        cmd2.Parameters.Add("@id", SqlDbType.Int).Value = tT.Id;
                        Employee_Email = (string)cmd2.ExecuteScalar();

                        conn.Close();


                    }

                }
                SmtpMail oMail = new SmtpMail("TryIt");
                oMail.From = System.Configuration.ConfigurationManager.AppSettings["Email"].ToString();
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var currentUser = manager.FindById(User.Identity.GetUserId());

                oMail.To = Employee_Email;
                oMail.Subject =  " Timesheet feedback posted";

                oMail.TextBody = "feedback posted on project/Client "+ tT.Project

                    + "\n"
                     
                + "\n" + "   Feedback  " + "\n"
                + "\n" + tT.feedback;

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

            }


            TempData["msg"] = "Reply posted Successfully.";
                return RedirectToAction("Timesheet");
            

            return View(tT);
        }

        // GET: Timesheet/Edit/5
        public ActionResult Edit(int? id)
        {
            Projects_table1 dbb = new Projects_table1();
            var categories = dbb.Projects.ToList();
            ViewBag.Categories = new SelectList(categories, "Project1", "Project1");


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TT tT = db.tt.Find(id);
            if (tT == null)
            {
                return HttpNotFound();
            }
            return View(tT);
        }

        // POST: Timesheet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Employee,CreatedOn,Description,Project,Status")] TT tT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tT).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Timesheet updated Successfully";
                return RedirectToAction("Index");
            }
            return View(tT);
        }

        // GET: Timesheet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TT tT = db.tt.Find(id);
            if (tT == null)
            {
                return HttpNotFound();
            }
            return View(tT);
        }

        // POST: Timesheet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TT tT = db.tt.Find(id);
            db.tt.Remove(tT);
            db.SaveChanges();
            TempData["msg"] = "Timesheet deleted Successfully";
            return RedirectToAction("list");
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
