using EASendMail;
using Ishop.Infa;
using Ishop.Models;
using IShop.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using Rotativa;
using System;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SmtpMail = EASendMail.SmtpMail;

namespace Ishop.Controllers
{
    [Authorize]
    public class leavesController : Controller
    {
        private leaveContext db = new leaveContext();

        // GET: leaves
        public ActionResult Index(string searchBy, string search, int? page)
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


        // GET: leaves/Details/5
        public ActionResult Details(int? id)
        {
            IDays_leave_context AA = new IDays_leave_context();
            var Phase = AA.days_Leaves.Where(a => a.Leave_Id == id).ToList();
            ViewBag.days = Phase;


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
            Employee_Context K2 = new Employee_Context();
            var data13 = K2.employees.Where(c => c.Username == User.Identity.Name).FirstOrDefault();
            ViewBag.EmpInfo = data13;

            DepartmentContext DD = new DepartmentContext();
            var data14 = K2.employees.Where(c => c.Username == User.Identity.Name).FirstOrDefault();
            var Depp = DD.departments.Where(c => c.DprtName == data14.DprtName).FirstOrDefault();
            ViewBag.Department = Depp;


            var data55 = K2.employees.Where(c => c.Username == User.Identity.Name).FirstOrDefault();
            if (data55 == null || data55.DprtName == null)
            {

                TempData["msg"] = "Non-qualified account for leaves applications";
                return RedirectToAction("Index");
            }
            var newLeave = new leave
            { CreatedOn = DateTime.Now,
              Employee= data13.Username,
              Message="",
              HR_Email = Depp.Email_Address,
              Emp_Mail =data13.Email,
              Department =Depp.DprtName,
              phone = data13.Contact,
              Designation=data13.DprtName,
              Approver_Remarks = "--",
              Status = 0

                // Set other properties as needed
            };

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());

            newLeave.Emp_Mail = currentUser.Email;

            db.leave.Add(newLeave);
            db.SaveChanges();

            return RedirectToAction("Add", new { id = newLeave.Id });
        }





        private string connectionString2 = ConfigurationManager.ConnectionStrings["Planning"].ConnectionString;
        public void PushEmail(string Recipient, string Subject, string Body, DateTime CreatedOn)
        {
            string query = "INSERT INTO OutgoingEmails (Recipient,Subject,Body,Status,CreatedOn,Trials,Response) VALUES " +
            "                                          (@Recipient,@Subject, @Body,0,@CreatedOn,@Trials,@Response)";
            using (SqlConnection connection = new SqlConnection(connectionString2))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@Recipient", Recipient);
                    command.Parameters.AddWithValue("@Subject", Subject);
                    command.Parameters.AddWithValue("@Body", Body);
                    command.Parameters.AddWithValue("@CreatedOn", CreatedOn);
                    command.Parameters.AddWithValue("@Trials", 0);
                    command.Parameters.AddWithValue("@Response", "--waiting--");
                    command.ExecuteNonQuery();
                }
            }
        }
        public void PushSms(string Recipient, string Subject, DateTime CreatedOn)
        {
            string query = "INSERT INTO Outgoingsms (MessageText,IsSent,CreatedOn,RecipientNumber,Trials,Response) VALUES " +
            "                                          (@MessageText,@IsSent,@CreatedOn,@RecipientNumber,@Trials,@Response)";
            using (SqlConnection connection = new SqlConnection(connectionString2))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@MessageText", Subject);
                    command.Parameters.AddWithValue("@IsSent", 0);
                    command.Parameters.AddWithValue("@CreatedOn", CreatedOn);
                    command.Parameters.AddWithValue("@RecipientNumber", Recipient);
                    command.Parameters.AddWithValue("@Trials", 0);
                    command.Parameters.AddWithValue("@Response", "--waiting--");
                    command.ExecuteNonQuery();
                }
            }
        }



































        // GET: leaves/Edit/5
        public ActionResult Add(int? id)
        {

            IDays_leave_context AA = new IDays_leave_context();
            var Phase = AA.days_Leaves.Where(a => a.Leave_Id == id).ToList();
            ViewBag.days = Phase;



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
        public ActionResult Add([Bind(Include = "Id,CreatedOn,Employee,Status,Department,Message,HR_Email,Emp_Mail,Approver_Remarks,Phone,Designation,Days")] leave leave)
        {
            if (ModelState.IsValid)
            {
                leave.Status = 1;
                db.Entry(leave).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leave);
        }










        public ActionResult Approve_leave(int? id)
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




            // lleave_llog ll = new lleave_llog();
            // var data10 = ll.leaves_logs.Where(c => c.leave_id == id).ToList();
            //ViewBag.F = data10;
            return View(leave);
        }

     
        private string connectionString = ConfigurationManager.ConnectionStrings["Planning"].ConnectionString;
        public void InsertIntoTable(DateTime Datett, string Approver, string Approver_status, string Approver_remarks, int leave_id)
        {
            string query = "INSERT INTO leaves_logs (createdon,Approver,Approver_status,Approver_remarks,leave_id) VALUES " +
            "                                       (@Datett,@Approver, @Approver_status,@Approver_remarks,@leave_id)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Datett", Datett);
                    command.Parameters.AddWithValue("@Approver", Approver);
                    command.Parameters.AddWithValue("@Approver_status", Approver_status);
                    command.Parameters.AddWithValue("@Approver_remarks", Approver_remarks);
                    command.Parameters.AddWithValue("@leave_id", leave_id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void leavesTrack(int Total_leaves_per_year, String Type, int Requested_leaves, int Remaining_leaves, string Username)
        {
            string query = "INSERT INTO leaves_Days_track (Total_leaves_per_year,Type,Requested_leaves,Remaining_leaves,Username) VALUES " +
            "                                             (@Total_leaves_per_year,@Type,@Requested_leaves,@Remaining_leaves,@Username)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Total_leaves_per_year", Total_leaves_per_year);
                    command.Parameters.AddWithValue("@Type", Type);
                    command.Parameters.AddWithValue("@Requested_leaves", Requested_leaves);
                    command.Parameters.AddWithValue("@Remaining_leaves", Remaining_leaves);
                    command.Parameters.AddWithValue("@Username", Username);
                    command.ExecuteNonQuery();
                }
            }
        }




        public ActionResult Print_Form(int id)
        {
            leaves_Days_trackContext LLL = new leaves_Days_trackContext();
            Employee_Context EEE = new Employee_Context();
            var result = db.leave.Where(i => i.Id == id).FirstOrDefault();
            var Emp = EEE.employees.Where(i => i.Username == result.Employee).FirstOrDefault();
            var dataa = LLL.Leaves_Days_Tracks.Where(c => c.Username == result.Employee).ToList();

            {
                leave pay = db.leave.FirstOrDefault(c => c.Id == id);
                ViewBag.leavedata = dataa;
                ViewBag.Fullname = Emp.Fullname;



                // lleave_llog ll = new lleave_llog();
                // var data10 = ll.leaves_logs.Where(c => c.leave_id == id && c.Approver_status =="1").ToList();
                //var data11 = ll.leaves_logs.Where(c => c.leave_id == id && c.Approver_status == "2").ToList();
                /// ViewBag.F = data10;
                // ViewBag.F2 = data11;









                var report = new PartialViewAsPdf("~/Views/leaves/Print_Form.cshtml", pay);
                return report;
            }
        }










        public ActionResult Delete(int id)
        {
            leave leave = db.leave.Find(id);
            db.leave.Remove(leave);
            db.SaveChanges();
            TempData["msg"] = "Leave request deleted successfully ";
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
