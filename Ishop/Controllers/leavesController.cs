using Antlr.Runtime.Misc;
using EASendMail;
using Ishop.Infa;
using Ishop.Models;
using IShop.Core;
using IShop.Core.Interface;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Services.Description;
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

            leaves_Days_trackContext LL = new leaves_Days_trackContext();
            var leavebalances = LL.Leaves_Days_Tracks.Where(c => c.Username == User.Identity.Name).ToList();
            ViewBag.leavebalance = leavebalances;


                if (!(search == null))
                {
                    return View(db.leave.OrderByDescending(p => p.Id).Where(c => c.Employee == User.Identity.Name && c.Status !=0).ToList().ToPagedList(page ?? 1, 6));

                }
                else
                {
                    return View(db.leave.OrderByDescending(p => p.Id).Where(c => c.Employee == User.Identity.Name && c.Status != 0).ToList().ToPagedList(page ?? 1, 6));


                }
            
        }
        public ActionResult All(string searchBy, string search, int? page, string Duration, string Department)
        {
            var Employ = db.leave.Select(t => t.Employee).Distinct().ToList();
            ViewBag.Usernames = Employ;

            var Departments = db.leave.Select(t => t.Department).Distinct().ToList();
            ViewBag.Departments = Departments;

            DateTime currentDate = DateTime.Now;

            // Set Monday to the Monday of the current week
            DateTime Monday = currentDate.Date.AddDays(-(int)currentDate.DayOfWeek + (int)DayOfWeek.Monday);

            // Set Sunday to the next Sunday from the current day
            DateTime Sunday = Monday.AddDays(6);

            // Determine date range based on Duration parameter
            DateTime startRange, endRange;

            switch (Duration)
            {
                case "thisWeek":
                    startRange = Monday;
                    endRange = Sunday;
                    break;

                case "lastWeek":
                    startRange = Monday.AddDays(-7);
                    endRange = Sunday.AddDays(-7);
                    break;

                case "thisMonth":
                    startRange = new DateTime(currentDate.Year, currentDate.Month, 1);
                    endRange = new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));
                    break;

                case "lastMonth":
                    startRange = new DateTime(currentDate.Year, currentDate.Month - 1, 1);
                    endRange = new DateTime(currentDate.Year, currentDate.Month, 1).AddDays(-1);
                    break;

                default:
                    // Use the default date range for other cases
                    startRange = Monday;
                    endRange = Sunday;
                    break;
            }




            if (!(search == null) && (!(search == "")))
            {
                return View(db.leave.OrderByDescending(p => p.Id).Where(c => c.Department.StartsWith(search) && c.Status != 0 && Department == null || Department == "" || c.Department == (Department)).ToList().ToPagedList(page ?? 1, 11));

            }
            else if (search == "")
            {
                return View(db.leave.OrderByDescending(p => p.Id).Where(c=>c.Status != 0 && Department == null || Department == "" || c.Department == (Department) ).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(db.leave.OrderByDescending(p => p.Id).Where(c => c.Status != 0 ).ToList().ToPagedList(page ?? 1, 11));
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
            var Depp = DD.departments.Where(c => c.DprtName == data13.DprtName).FirstOrDefault();
            ViewBag.Department = Depp;


            var data55 = K2.employees.Where(c => c.Username == User.Identity.Name).FirstOrDefault();
            if (data55 == null || data55.DprtName == null)
            {

                TempData["msg"] = "Non-qualified account for leaves applications";
                return RedirectToAction("Index");
            }
            var newLeave = new leave
            { CreatedOn = DateTime.Now,
              Employee = data13.Username,
              Message="",
              HR_Email = Depp.Email_Address,
              Emp_Mail =data13.Email,
              Department = data13.DprtName,
              phone = data13.Contact,
              Designation=data13.DprtName,
              Approver_Remarks = "--",
              Status = 0,
              Days= 0,
               From_Date = DateTime.Now,
                To_Date = DateTime.Now,
                Return_Date = DateTime.Now,
                Type = "____",
                Total_Hours=0,
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
        public ActionResult Add([Bind(Include = "Id,CreatedOn,Employee,Status,Department,Message,HR_Email,Emp_Mail,Approver_Remarks,Phone,Designation,Days,From_Date,To_Date,Return_Date,Type,Total_Hours")] leave leave,string action)
        {
            
            if (ModelState.IsValid)
            {
                
                
                if (action == "Save_Request")
                {
                    leave.Status = 66;
                }
                else if (action == "Submit_Request")
                {
                    leave.Status = 1;
                    var Subject = "leave Request Submitted ";
                    var TextBody = "Hello ,your leave request has been submitted succesfully ,you will be notified upon approvals "
                    + "\n" + " Requested Total Days " + leave.Days +
                     "\n" + " thank you  :";
                    CollectSms(TextBody, leave.phone);
                    CollectEmail(TextBody, leave.Emp_Mail, Subject);
                }
                db.Entry(leave).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leave);
        }






        public ActionResult Approve_leave(int? id)
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

        public ActionResult Approve_Deny(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Use using statement to properly dispose of the context
            using (IDays_leave_context AA = new IDays_leave_context())
            {
                var Phase = AA.days_Leaves.Where(a => a.Leave_Id == id).ToList();
                ViewBag.days = Phase;

                leave leave = db.leave.Find(id);

                if (leave == null)
                {
                    return HttpNotFound();
                }

                return View(leave);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve_Deny([Bind(Include = "Id,CreatedOn,Employee,Status,Department,Message,HR_Email,Emp_Mail,Approver_Remarks,Phone,Designation,Days,From_Date,To_Date,Return_Date,Type,Total_Hours")] leave leave, string action)
        {
            if (ModelState.IsValid)
            {
                
                {
                    var existingLeave = db.leave.Find(leave.Id);

                    if (existingLeave != null)
                    {
                        if (action == "Approve_Request")
                        {
                            db.Entry(existingLeave).State = EntityState.Modified;
                            existingLeave.Status = 2;
                            var Subject = "leave Request Approved ";
                            var TextBody = "Hello ,your leave request has been Approved. "
                            + "\n" + "Reasons "
                            + "\n"
                            + "\n" + leave.Approver_Remarks +
                             "\n" + " thank you  :";
                            CollectSms(TextBody, leave.phone);
                            CollectEmail(TextBody, leave.Emp_Mail, Subject);

                            IDays_leave_context dv = new IDays_leave_context();
                            var Dayss = dv.days_Leaves.Where(l => l.Leave_Id == leave.Id).FirstOrDefault();
                            leaveTypesContext lv = new leaveTypesContext();
                            var leaveType = lv.leaves_Types.Where(l=>l.Type == Dayss.Type).FirstOrDefault();

                            leavesTrack(leaveType.Days, leaveType.Type, leave.Days, leaveType.Days - leave.Days, leave.Employee);

                            Updatetimesheet(Dayss.From_Date, Dayss.To_Date, Dayss.Time, leave.Employee,leave.Id) ;

                        }
                        else if (action == "Reject_Request")
                        {
                            db.Entry(existingLeave).State = EntityState.Modified;
                            existingLeave.Status = 99;
                            var Subject = "leave Request Rejected ";
                            var TextBody = "Hello ,your leave request has been rejected. "
                            + "\n" + "Reasons " 
                            +"\n" 
                            +"\n"  + leave.Approver_Remarks +
                             "\n" + " thank you  :";
                            CollectSms(TextBody, leave.phone);
                            CollectEmail(TextBody, leave.Emp_Mail, Subject);
                        }

                       
                        db.SaveChanges();
                        return RedirectToAction("All");
                    }
                    else
                    {
                        // Handle the case where the entity doesn't exist
                        return HttpNotFound();
                    }
                }
            }

            return View(leave);
        }



        public void leavesTrack(Decimal Total_leaves_per_year, String Type, decimal Requested_leaves, decimal Remaining_leaves, string Username)
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




        public void Updatetimesheet(DateTime startDate, DateTime endDate, int Type, string Employee, int leaveid)
        {
          

            // Adjust GetDatesInRange to exclude weekends
            List<DateTime> datesInRange = GetDatesInRangeWithoutWeekends(startDate, endDate);

            Random random = new Random();
            int HoursOnleave;

            if (Type == 1) { HoursOnleave = 8; } else { HoursOnleave = 4; }

            Timesheet_Context TT = new Timesheet_Context();
            foreach (var date in datesInRange)
            {
                var Sheet = TT.timesheets.Where(i => i.Owner == Employee && date >= i.From_Date && date <= i.End_Date).FirstOrDefault();
                if (Sheet != null)
                {
                    decimal addedHours = Sheet.Leave + HoursOnleave;

                    // Deduct added hours from leavee.Total_Hours
                    

                    Sheet.Leave = addedHours;
                    Sheet.Tt = Sheet.Direct_Hours + Sheet.InDirect_Hours + Sheet.Leave;
                    TT.SaveChanges();

                    var leavee = db.leave.Where(c=>c.Id ==leaveid).FirstOrDefault();
                    if (leavee != null)
                    {
                        leavee.Total_Hours = leavee.Total_Hours - HoursOnleave;
                        db.SaveChanges(); // Save changes for each iteration
                    }
                }
            }
        }

        // New method to get dates in range excluding weekends
        private List<DateTime> GetDatesInRangeWithoutWeekends(DateTime startDate, DateTime endDate)
        {
            List<DateTime> datesInRange = new List<DateTime>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                // Exclude weekends (Saturday and Sunday)
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    datesInRange.Add(date);
                }
            }

            return datesInRange;
        }




























        private int GetCurrentWeekNumber(DateTime date)
    {
        var cal = System.Globalization.CultureInfo.CurrentCulture.Calendar;
        int week = cal.GetWeekOfYear(date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        return week;
    }
    private List<DateTime> GetDatesInRange(DateTime startDate, DateTime endDate)
        {
            List<DateTime> datesInRange = new List<DateTime>();

            DateTime currentDate = startDate;

            while (currentDate <= endDate)
            {
                datesInRange.Add(currentDate);
                currentDate = currentDate.AddDays(1);
            }

            return datesInRange;
        }







        public void CollectSms(string Message, string Recepient)
        {
            Random random = new Random();
            using (var context = new OutgoingsmsContext())
            {
                Outgoingsms outgoingsms = new Outgoingsms

                {

                    Id = random.Next(), // Using Guid for a unique identifier
                    CreatedOn = DateTime.Now,
                    MessageText=Message,
                    RecipientNumber=Recepient,
                    Response="--",
                    Trials=0,
                    IsSent=false,
                };

                context.outgoingsms.Add(outgoingsms);
                context.SaveChanges();
            }
        }
        public void CollectEmail(string Message, string Recepient, string subject)
        {
            Random random = new Random();
            using (var context = new OutgoingEmailsContext())
            {
                OutgoingEmails outgoingEmails = new OutgoingEmails

                {

                    Id = random.Next(), // Using Guid for a unique identifier
                    CreatedOn = DateTime.Now,
                    Body = Message,
                    Subject=subject,
                    Recipient = Recepient,
                    Response = "--",
                    Trials = 0,
                    Status = false,
                };

                context.outgoingEmails.Add(outgoingEmails);
                context.SaveChanges();
            }
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

            IDays_leave_context dd = new IDays_leave_context();
            Days_leave days_Leave = dd.days_Leaves.FirstOrDefault(l => l.Leave_Id == id);
            if (days_Leave != null)
            {
                dd.days_Leaves.Remove(days_Leave);
                dd.SaveChanges();
            }

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













        
        

























    }
}
