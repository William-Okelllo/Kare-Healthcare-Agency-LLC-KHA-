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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mail;
using EASendMail;
using SmtpMail = EASendMail.SmtpMail;
using Syncfusion.DocIO.DLS;

namespace Ishop.Controllers
{[Authorize]
    public class leavesController : Controller
    {
        private leaveContext db = new leaveContext();

        // GET: leaves
        public ActionResult Index(string searchBy, string search, int? page)
        {
            lleave_llog ll = new lleave_llog();
            var data10 = ll.leaves_logs.ToList();
            ViewBag.F = data10;


            if (this.User.IsInRole("Leaves_Approval"))
            {

                if (!(search == null))
                {
                    return View(db.leave.OrderByDescending(p => p.Id).Where(c => c.Employee == search).ToList().ToPagedList(page ?? 1, 6));

                }
                else
                {
                    return View(db.leave.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 6));


                }

            }
            else
            {
                if (!(search == null))
                {
                    return View(db.leave.OrderByDescending(p => p.Id).Where(c => c.Employee == User.Identity.Name).ToList().ToPagedList(page ?? 1, 6));

                }
                else
                {
                    return View(db.leave.OrderByDescending(p => p.Id).Where(c => c.Employee == User.Identity.Name).ToList().ToPagedList(page ?? 1, 6));


                }
            }
        }
       

        // GET: leaves/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            leave leave = db.leave.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }
            return View(leave);
        }

        // GET: leaves/Create
        public ActionResult Create()
        {
            string constr = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(constr))
            {

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.Open();
                        cmd.CommandText = "sp_getDrpt";
                        cmd.Parameters.Add("@user", SqlDbType.NChar).Value = User.Identity.Name;


                        ViewBag.Drpt = (string)cmd.ExecuteScalar();

                        conn.Close();
                    }

                    catch
                    {
                        ViewBag.Drpt = "--Acc missing Department--";
                    }
                }
                try
                {
                    using (SqlCommand cmd2 = conn.CreateCommand())
                    {
                        cmd2.Connection = conn;
                        cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                        conn.Open();
                        cmd2.CommandText = "sp_getHodEmail";
                        cmd2.Parameters.Add("@user", SqlDbType.NChar).Value = User.Identity.Name;


                        ViewBag.hod = (string)cmd2.ExecuteScalar();

                        conn.Close();

                    }
                }
                catch
                {
                    ViewBag.hod = "--Acc missing Department--";
                }
            }
            return View();
        }

        // POST: leaves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedOn,From_Date,To_Date,Employee,Return_Date,Status,Department,Message,Type,HR_Email,Emp_Mail")] leave leave)
        {
            if (leave.Department == null)
            {
                TempData["msg"] = "Unable to post leave : No department set under user account ";
                return RedirectToAction("Create");

            }
            TimeSpan timeSpan = leave.From_Date - DateTime.Today;
            int days = timeSpan.Days;
            int dd = Int16.Parse( System.Configuration.ConfigurationManager.AppSettings["Leave_pre_days"]);
            var ddd= Int16.Parse(System.Configuration.ConfigurationManager.AppSettings["Leave_pre_days"]);
            if (days < dd)
            { TempData["msg"] = "Kindly request leave  " + dd + "days from today's date"; }
            else
            {
                if (ModelState.IsValid)
                {
                    var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    var currentUser = manager.FindById(User.Identity.GetUserId());

                    db.leave.Add(leave);

                    leave.Status = "0";
                    leave.Approver_Remarks = "--";
                    leave.Emp_Mail = currentUser.Email;
                    TempData["msg"] = "leave request posted successfully ";
                    db.SaveChanges();

                    try
                    {

                        SmtpMail oMail = new SmtpMail("TryIt");
                        oMail.From = System.Configuration.ConfigurationManager.AppSettings["Email"].ToString();
                        oMail.To = leave.HR_Email;
                        oMail.Subject = "New leave request";
                        oMail.TextBody = "Hello, this a leave notification sent to you are the HOD of the department kindly login to the portal for approvals. "
                        +
                           "\n" + " thank you  :";
                        SmtpServer oServer = new SmtpServer("smtp.gmail.com");

                        oServer.User = System.Configuration.ConfigurationManager.AppSettings["Email"].ToString();
                        oServer.Password = System.Configuration.ConfigurationManager.AppSettings["Password"].ToString();

                        oServer.ConnectType = SmtpConnectType.ConnectTryTLS;
                        oServer.Port = 587;
                        SmtpClient oSmtp = new SmtpClient();
                        SmtpClientAsyncResult oResult = oSmtp.BeginSendMail(oServer, oMail, null, null);
                        return RedirectToAction("Index");
                    }
                    catch
                    {

                    }

                    return RedirectToAction("Index");
                }
            }
            return View(leave);
        }

        // GET: leaves/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            leave leave = db.leave.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }
            return View(leave);
        }

        // POST: leaves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,From_Date,To_Date,Employee,Return_Date,Status,Department,Message,Type,HR_Email,Emp_Mail")] leave leave)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leave).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leave);
        }










        public ActionResult Approve_leave (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            leave leave = db.leave.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }




            lleave_llog ll = new lleave_llog();
            var data10 = ll.leaves_logs.ToList();
            ViewBag.F = data10;
            return View(leave);
        }

        // POST: leaves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve_leave([Bind(Include = "Id,CreatedOn,From_Date,To_Date,Employee,Return_Date,Status,Department,Message,Type,HR_Email,Emp_Mail,Approver_Remarks")] leave leave)
        {
            if (ModelState.IsValid)
            {
                

                db.Entry(leave).State = EntityState.Modified;
                db.SaveChanges();
                try
                {
                    string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
                    SqlConnection sqlCon = new SqlConnection(strcon);
                    SqlCommand sqlcmnd = new SqlCommand("sp_markleave_approved", sqlCon);
                    sqlcmnd.CommandType = CommandType.StoredProcedure;
                    sqlcmnd.Parameters.AddWithValue("@Id", leave.Id);
                    sqlcmnd.Parameters.AddWithValue("@Approver_Remarks", leave.Approver_Remarks);
                    sqlcmnd.Parameters.AddWithValue("@Approver", User.Identity.Name);
                    sqlCon.Open();
                    sqlcmnd.ExecuteNonQuery();
                    sqlCon.Close();

                    TempData["msg"] = "Leave approved successfully ";
                }
                catch
                {
                    TempData["msg"] = "error occured in approving leave request ";
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return View(leave);
        }


















        // GET: leaves/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            leave leave = db.leave.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }
            return View(leave);
        }

        // POST: leaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            leave leave = db.leave.Find(id);
            db.leave.Remove(leave);
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

        public ActionResult Approve(int? id, leave leave)
        {
            


            {
                TempData["msg"] = "✔ Leave request approved employee notified ";
                SmtpMail oMail = new SmtpMail("TryIt");
                oMail.From = System.Configuration.ConfigurationManager.AppSettings["Email"].ToString();
                oMail.To = leave.Emp_Mail;
                oMail.Subject = "leave request approved";
                oMail.TextBody = "Hello ,kindly login to the portal to check your approved requested leave. "
                +
                   "\n" + " thank you  :";
                SmtpServer oServer = new SmtpServer("smtp.gmail.com");

                oServer.User = System.Configuration.ConfigurationManager.AppSettings["Email"].ToString();
                oServer.Password = System.Configuration.ConfigurationManager.AppSettings["Password"].ToString();

                oServer.ConnectType = SmtpConnectType.ConnectTryTLS;
                oServer.Port = 587;
                SmtpClient oSmtp = new SmtpClient();
                SmtpClientAsyncResult oResult = oSmtp.BeginSendMail(oServer, oMail, null, null);
                return RedirectToAction("leaves_Requests");
            }
        }
        public ActionResult Deny(int? id, leave leave)
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
                SqlConnection sqlCon = new SqlConnection(strcon);
                SqlCommand sqlcmnd = new SqlCommand("sp_markleave_denied", sqlCon);
                sqlcmnd.CommandType = CommandType.StoredProcedure;
                sqlcmnd.Parameters.AddWithValue("@Id", leave.Id);
                sqlCon.Open();
                sqlcmnd.ExecuteNonQuery();
                sqlCon.Close();

                
            }
            catch
            {
                TempData["msg"] = "error occured in approving leave request ";
                return RedirectToAction("leaves_Requests");
            }
            SmtpMail oMail = new SmtpMail("TryIt");
            oMail.From = System.Configuration.ConfigurationManager.AppSettings["Email"].ToString();
            oMail.To = leave.Emp_Mail;
            oMail.Subject = "leave request Denied";
            oMail.TextBody = "Hello ,kindly note your leave request has been denied. "
            +
               "\n" + " kindly check on the portal to check the denied leave, :" +
                "\n" + " thank you. :";
            SmtpServer oServer = new SmtpServer("smtp.gmail.com");

            oServer.User = System.Configuration.ConfigurationManager.AppSettings["Email"].ToString();
            oServer.Password = System.Configuration.ConfigurationManager.AppSettings["Password"].ToString();

            oServer.ConnectType = SmtpConnectType.ConnectTryTLS;
            oServer.Port = 587;
            SmtpClient oSmtp = new SmtpClient();
            SmtpClientAsyncResult oResult = oSmtp.BeginSendMail(oServer, oMail, null, null);

            {
                TempData["msg"] = "✔ Leave request approved employee notified ";
                return RedirectToAction("leaves_Requests");
            }
        }
    }
}
