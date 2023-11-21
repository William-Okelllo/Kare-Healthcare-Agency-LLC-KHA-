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
            //  lleave_llog ll = new lleave_llog();
            //  var data10 = ll.leaves_logs.ToList();
            // ViewBag.F = data10;



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

            leaveTypesContext dbb = new leaveTypesContext();
            var categories = dbb.leaves_Types.ToList();
            ViewBag.Categories = new SelectList(categories, "Type", "Type");

            //leaves_t l2 = new leaves_t();
            // var data12 = l2.leaves_Days_track.Where(c => c.Username == User.Identity.Name).ToList();
            // ViewBag.F2 = data12;

            Employee_Context K2 = new Employee_Context();
            var data13 = K2.employees.Where(c => c.Username == User.Identity.Name).ToList();
            ViewBag.EmpInfo = data13;

            DepartmentContext DD = new DepartmentContext();
            var data14 = K2.employees.Where(c => c.Username == User.Identity.Name).FirstOrDefault();
            var Depp = DD.departments.Where(c => c.DprtName == data14.DprtName);
            ViewBag.Department = Depp;


            var data55 = K2.employees.Where(c => c.Username == User.Identity.Name).FirstOrDefault();
            if (data55 == null || data55.DprtName == null)
            {

                TempData["msg"] = "Non-qualified account for leaves applications";
                return RedirectToAction("Index");
            }
            return View();
        }







        // POST: leaves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedOn,From_Date,To_Date,Employee,Return_Date,Status,Department,Message,Type,HR_Email,Emp_Mail,Requested_Days,Phone,PF_NO,Designation")] leave leave)
        {

            leave.Approver_Remarks = "--";
            leave.Status = "0";

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());

            int totalDays = (int)(leave.To_Date - leave.From_Date).TotalDays + 1; // Total number of days between from date and to date, including weekends
            int weekendDays = 0;
            for (DateTime date = leave.From_Date; date <= leave.To_Date; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    weekendDays++;
                }
            }



            TimeSpan timeSpan = leave.From_Date - DateTime.Today;
            int days = timeSpan.Days;
            int dd = Int16.Parse(System.Configuration.ConfigurationManager.AppSettings["Leave_pre_days"]);
            var ddd = Int16.Parse(System.Configuration.ConfigurationManager.AppSettings["Leave_pre_days"]);
            if (days < dd)
            { TempData["msg"] = "Kindly request leave  " + dd + "days from today's date"; }
            else
            {

                bool recordExists = leaveDays.Leaves_Days_Tracks.Any(c => c.Username == leave.Employee && c.Type == leave.Type);
                if (recordExists)
                {  //update leave numbers

                    var leaveDaysId = leaveDays.Leaves_Days_Tracks.FirstOrDefault(c => c.Username == leave.Employee && c.Type == leave.Type);

                    var Ticko = leaveDays.Leaves_Days_Tracks.Find(leaveDaysId.Id);

                    if (Ticko.Remaining_leaves < leave.Requested_Days)
                    {
                        TempData["msg"] = "Requested leaves greater than balance ";
                        return RedirectToAction("Index");
                    }



                }

                else

                {
                    leaveTypesContext LLS = new leaveTypesContext();
                    var leaveNN = LLS.leaves_Types.FirstOrDefault(c => c.Type == leave.Type);

                    if (leaveNN.Days < weekendDays)
                    {
                        TempData["msg"] = "Requested leaves greater than leave days ";
                        return RedirectToAction("Index");
                    }
                }


                if (ModelState.IsValid)
                {


                    int weekdays = totalDays - weekendDays; // Number of weekdays between from date and to date, excluding weekends


                    db.leave.Add(leave);


                    leave.Requested_Days = weekdays;

                    leave.Emp_Mail = currentUser.Email;
                    TempData["msg"] = "leave request posted successfully ";
                    db.SaveChanges();

                    try
                    {


                        var To = leave.HR_Email;
                        var Subject = "New leave request";
                        var Subject2 = "leave Request Submitted";
                        var TextBody = "Employee :" + leave.Employee
                        + "\n"
                        + "\n" + " leave Details  :"
                        + "\n" + "Requested Days : " + leave.Requested_Days + " days"
                        + "\n" + "From Date : " + leave.From_Date.ToString("dd-MMM-yyyy")
                        + "\n" + "To Date : " + leave.To_Date.ToString("dd-MMM-yyyy")
                        + "\n"
                        + "\n" + "Employee Additional Remarks"
                        + "\n"
                        + "\n" + leave.Message;


                        PushEmail(To, Subject, TextBody, DateTime.Now);
                        PushEmail(currentUser.Email, Subject2, TextBody, DateTime.Now);
                        PushSms(currentUser.PhoneNumber, TextBody, DateTime.Now);
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






        private string connectionString2 = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
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
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,From_Date,To_Date,Employee,Return_Date,Status,Department,Message,Type,HR_Email,Emp_Mail,Designation")] leave leave)
        {
            if (ModelState.IsValid)
            {
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

        private leaves_Days_trackContext leaveDays = new leaves_Days_trackContext();
        leaveTypesContext LLL = new leaveTypesContext();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve_leave([Bind(Include = "Id,CreatedOn,From_Date,To_Date,Employee,Return_Date,Status,Department,Message,Type,HR_Email,Emp_Mail,Approver_Remarks,Requested_Days,Phone,PF_NO,Designation")] leave leave)
        {
            if (ModelState.IsValid)
            {
                if (leave.Status == "1")
                {

                    db.Entry(leave).State = EntityState.Modified;
                    db.SaveChanges();
                    InsertIntoTable(DateTime.Now, User.Identity.Name, leave.Status, leave.Approver_Remarks, leave.Id);




                    return RedirectToAction("Index");
                }
                else if (leave.Status == "2")
                {


                    db.Entry(leave).State = EntityState.Modified;
                    db.SaveChanges();
                    InsertIntoTable(DateTime.Now, User.Identity.Name, leave.Status, leave.Approver_Remarks, leave.Id);


                    bool recordExists = leaveDays.Leaves_Days_Tracks.Any(c => c.Username == leave.Employee && c.Type == leave.Type);

                    if (recordExists)
                    {  //update leave numbers

                        var leaveDaysId = leaveDays.Leaves_Days_Tracks.FirstOrDefault(c => c.Username == leave.Employee && c.Type == leave.Type);

                        var Ticko = leaveDays.Leaves_Days_Tracks.Find(leaveDaysId.Id);

                        if (Ticko != null)
                        {
                            var TT = Ticko;

                            var Requested = Ticko.Requested_leaves + leave.Requested_Days;
                            var Balance = Ticko.Remaining_leaves - Ticko.Requested_leaves;


                            Ticko.Requested_leaves = Requested;
                            Ticko.Remaining_leaves = Balance;
                            leaveDays.SaveChanges();


                        }

                    }

                    else
                    {
                        //insert
                        leaveTypesContext LLL = new leaveTypesContext();
                        var leaveTT = LLL.leaves_Types.Where(c => c.Type == leave.Type).FirstOrDefault();


                        leavesTrack(leaveTT.Days, leaveTT.Type, leave.Requested_Days, leaveTT.Days - leave.Requested_Days, leave.Employee);
                        try
                        {
                            Employee_Context EE = new Employee_Context();
                            var Employee = EE.employees.Where(e => e.Username == leave.Employee).FirstOrDefault();


                            var To = leave.HR_Email;
                            var Subject = "leave Approved Successfully";

                            var TextBody = "Employee :" + leave.Employee
                            + "\n"
                            + "\n" + " leave Details  :"
                            + "\n" + "Requested Days : " + leave.Requested_Days + " days"
                            + "\n" + "From Date : " + leave.From_Date.ToString("dd-MMM-yyyy")
                            + "\n" + "To Date : " + leave.To_Date.ToString("dd-MMM-yyyy")
                            + "\n"
                            + "\n" + "Approver Remarks"
                            + "\n"
                            + "\n" + leave.Approver_Remarks;


                            PushEmail(To, Subject, TextBody, DateTime.Now);
                            PushEmail(Employee.Email, Subject, TextBody, DateTime.Now);
                            PushSms(Employee.Contact, TextBody, DateTime.Now);

                            return RedirectToAction("Index");
                        }

                        catch (Exception ex) { }
                    }

                }

                else if (leave.Status == "99")
                {
                    try
                    {
                        Employee_Context EE = new Employee_Context();
                        var Employee = EE.employees.Where(e => e.Username == leave.Employee).FirstOrDefault();


                        var To = leave.HR_Email;
                        var Subject = "leave Request Denied ";

                        var TextBody = "Employee :" + leave.Employee
                        + "\n"
                        + "\n" + " leave Details  :"
                        + "\n" + "Requested Days : " + leave.Requested_Days + " days"
                        + "\n" + "From Date : " + leave.From_Date.ToString("dd-MMM-yyyy")
                        + "\n" + "To Date : " + leave.To_Date.ToString("dd-MMM-yyyy")
                        + "\n"
                        + "\n" + "Approver Remarks"
                        + "\n"
                        + "\n" + leave.Approver_Remarks;


                        var leav = db.leave.Find(leave.Id);
                        if (leav != null)
                        {
                            leav.Status = "99";
                            db.SaveChanges();
                        }

                        PushEmail(To, Subject, TextBody, DateTime.Now);
                        PushEmail(Employee.Email, Subject, TextBody, DateTime.Now);
                        PushSms(Employee.Contact, TextBody, DateTime.Now);

                        return RedirectToAction("Index");
                    }

                    catch (Exception ex) { }
                }









            }
            return RedirectToAction("Index");
        }


        private string connectionString = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
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
