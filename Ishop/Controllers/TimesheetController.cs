using Ishop.Infa;
using Ishop.Models;
using IShop.Core;
using IShop.Core.Interface;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Configuration;
using System.Web.Http.Results;
using System.Web.Mvc;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Ishop
{

    public class TimesheetController : Controller
    {
        private Userstable dbb = new Userstable();
        private Timesheet_Context db = new Timesheet_Context();

        [Authorize]
        public ActionResult Index()
        {


            List<A> results = new List<A>();
            string strcon = ConfigurationManager.ConnectionStrings["Planning"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(strcon))
            {
                sqlCon.Open();

                using (SqlCommand command = new SqlCommand("sp_Calendar_Activities", sqlCon))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", User.Identity.Name);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            A result = new A
                            {
                                WeekdayName = reader["WeekdayName"].ToString(),
                                Direct_Hours = Convert.ToInt32(reader["Direct Hours"]),
                                InDirect_Hours = Convert.ToInt32(reader["InDirect Hours"]),
                                Total_Hours = Convert.ToInt32(reader["Total Hours"])
                            };

                            results.Add(result);
                        }
                    }
                }
            }

            ViewBag.Results = results;











            return View();
        }





















        [Authorize]
        public ActionResult Approval( int? page, string option, string startDate, string endDate)
        {
            string strcon = ConfigurationManager.ConnectionStrings["Planning"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(strcon))
            {
                using (SqlCommand cmd = new SqlCommand("Employeeslist", sqlCon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    sqlCon.Open();
                    List<string> usernames = new List<string>();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usernames.Add(reader["Username"].ToString());
                        }
                    }

                    sqlCon.Close();

                    // Assign the usernames to ViewBag
                    ViewBag.Usernames = usernames;

                }

            }

            

            List<CombinedViewModel> timeSheets = new List<CombinedViewModel>(); // Create a list to store time sheet data

            using (SqlConnection sqlCon = new SqlConnection(strcon))
            {
                using (SqlCommand cmd = new SqlCommand("Alltimesheets", sqlCon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@UserName", option);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    sqlCon.Open();

                    // Execute the stored procedure and get the result
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // You can process the result set here or pass it to the view
                        while (reader.Read())
                        {
                            CombinedViewModel combinedViewModel = new CombinedViewModel
                            {
                                WeekNumber = Convert.ToInt32(reader["Week_Number"]),
                                MonthName = reader["Month_Name"].ToString(),
                                User = reader["User"].ToString(),
                                IndirectHours = Convert.ToInt32(reader["Indirect Hours"]),
                                DirectHours = Convert.ToInt32(reader["Direct Hours"]),
                                TotalHours = Convert.ToInt32(reader["Total Hours"]),
                                Status = reader["Status"].ToString()
                            };

                            timeSheets.Add(combinedViewModel);
                        }
                    }

                    sqlCon.Close();
                }
            }

            int pageSize = 11; // Number of items per page
            int pageNumber = page ?? 1; // If no page is specified, default to page 1

            IPagedList<CombinedViewModel> pagedTimeSheets = timeSheets.ToPagedList(pageNumber, pageSize);

            return View(pagedTimeSheets);
        }




        // Helper function to get the week from a date



        private Direct_Activities_Context DA = new Direct_Activities_Context();
        private Indirect_Activities_Context IA = new Indirect_Activities_Context();
        public ActionResult Directtasks(int id, int? page,string user)
        {
            var data =DA.direct_Activities.Where(c => c.User == user && c.WeekNo == id).ToList().ToPagedList(page ?? 1, 11).ToList();
            ViewBag.user = user;
            ViewBag.weeNo = id;
            return PartialView("Directtasks", data);
        }
        public ActionResult InDirecttasks(int id, int? page, string user)
        {
            var data = IA.indirect_Activities.Where(c => c.User == user && c.WeekNo == id).ToList().ToPagedList(page ?? 1, 11).ToList();

            ViewBag.user = user;
            ViewBag.weeNo = id;
            return PartialView("InDirecttasks", data);
        }




        public ActionResult Re_Open(int Id)
        {
            var Owner = db.timesheets.Where(t => t.Id == Id).FirstOrDefault();
            var U = UA.AspNetUsers.Where(t => t.UserName == Owner.Owner).FirstOrDefault();

            string input = Owner.Weekid.ToString(); // Your input string

            int week;
            int month;
            int year;

            if (input.Length == 7)
            {
                week = int.Parse(input.Substring(0, 1));
                month = int.Parse(input.Substring(1, 2));
                year = int.Parse(input.Substring(3));
            }
            else if (input.Length == 6)
            {
                week = int.Parse(input.Substring(0, 1));
                month = int.Parse(input.Substring(1, 2));
                year = int.Parse(input.Substring(3));
            }
            else
            {
                throw new ArgumentException("Invalid input format");
            }

            int adjustedMonth = month % 12;
            string monthName = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(adjustedMonth);

            string formattedDate = $"week {week} of {monthName} {year}";












            string daytime;
            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 12)
            { daytime = "Good Morning"; }
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
            { daytime = "Good Afternoon"; }
            else
            { daytime = "Good Evenning"; }
            string Subject = "Timesheet Re-Opened Week No " + formattedDate;
            string message = daytime + " ," + Owner.Owner
            + "\n" + "Your timesheet has been re-opened by management "
            + "\n" + "kindly login and fill in your timesheet details "
            + "\n" + "Regards , HR-Team ";

            PushEmail(U.Email, Subject, message, DateTime.Now);
            PushSms(U.PhoneNumber, message, DateTime.Now);
            Timesheet check = db.timesheets.Find(Id);
            if (check != null)
            {
                check.Status = 0;
                db.SaveChanges();
            }
            TempData["msg"] = "✔  Timesheet Re-Openned successfully - Employee Notified";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }





        private Userstable UA = new Userstable();
        [Authorize]
        public ActionResult Submit(int Id)
        {
            var Owner = db.timesheets.Where(t => t.Id == Id).FirstOrDefault();
            var U = UA.AspNetUsers.Where(t => t.UserName == Owner.Owner).FirstOrDefault();

            string input = Owner.Weekid.ToString(); // Your input string

            int week;
            int month;
            int year;

            if (input.Length == 7)
            {
                week = int.Parse(input.Substring(0, 1));
                month = int.Parse(input.Substring(1, 2));
                year = int.Parse(input.Substring(3));
            }
            else if (input.Length == 6)
            {
                week = int.Parse(input.Substring(0, 1));
                month = int.Parse(input.Substring(1, 2));
                year = int.Parse(input.Substring(3));
            }
            else
            {
                throw new ArgumentException("Invalid input format");
            }

            int adjustedMonth = month % 12;
            string monthName = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(adjustedMonth);

            string formattedDate = $"week {week} of {monthName} {year}";

            string daytime;
            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 12)
            { daytime = "Good Morning"; }
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
            { daytime = "Good Afternoon"; }
            else
            { daytime = "Good Evenning"; }
            string Subject = "Timesheet Submitted Successfully Week No " + formattedDate;
            string message = daytime + " ," + Owner.Owner
            + "\n" + "Your timesheet has been submitted successfully  "
            + "\n" + "kindly note management team will notify you on the approvals via mail soon."
            + "\n" + "thank you "
            + "\n" + "Regards , HR-Team ";

            PushEmail(U.Email, Subject, message, DateTime.Now);
            PushSms(U.PhoneNumber, message, DateTime.Now);
            Timesheet check = db.timesheets.Find(Id);
            if (check != null)
            {
                check.Status = 1;
                db.SaveChanges();
            }

            TempData["msg"] = "✔  Timesheet Submitted successfully";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }


        public ActionResult Approve(int Id)
        {
            var Owner = db.timesheets.Where(t => t.Id == Id).FirstOrDefault();
            var U = UA.AspNetUsers.Where(t => t.UserName == Owner.Owner).FirstOrDefault();
            string input = Owner.Weekid.ToString(); // Your input string

            int week;
            int month;
            int year;

            if (input.Length == 7)
            {
                week = int.Parse(input.Substring(0, 1));
                month = int.Parse(input.Substring(1, 2));
                year = int.Parse(input.Substring(3));
            }
            else if (input.Length == 6)
            {
                week = int.Parse(input.Substring(0, 1));
                month = int.Parse(input.Substring(1, 2));
                year = int.Parse(input.Substring(3));
            }
            else
            {
                throw new ArgumentException("Invalid input format");
            }

            int adjustedMonth = month % 12;
            string monthName = System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(adjustedMonth);

            string formattedDate = $"week {week} of {monthName} {year}";
            string daytime;
            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 12)
            { daytime = "Good Morning"; }
            else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
            { daytime = "Good Afternoon"; }
            else
            { daytime = "Good Evenning"; }
            string Subject = "Timesheet Approved Successfully Week No " + formattedDate;
            string message = daytime + " ," + Owner.Owner
            + "\n" + "Your timesheet has been Approved successfully  "
            + "\n" + "thank you "
            + "\n" + "Regards , HR-Team ";

            PushEmail(U.Email, Subject, message, DateTime.Now);
            PushSms(U.PhoneNumber, message, DateTime.Now);
            Timesheet check = db.timesheets.Find(Id);
            if (check != null)
            {
                check.Status = 2;
                db.SaveChanges();
            }

            TempData["msg"] = "✔  Timesheet Approved successfully";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }





        [Authorize]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public bool TimesheetExists(int weekNumber, string employeeUsername)
        {
            return db.timesheets.Any(c => c.Weekid == weekNumber && c.Owner == employeeUsername);
        }

       
        public void InsertTimesheet()
        {
            DateTime currentDate = DateTime.Now;

            // Calculate the day of the week the month starts
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            int daysPassed = (int)firstDayOfMonth.DayOfWeek;

            // Adjust daysPassed if the first day of the month is not Sunday (0)
            daysPassed = (daysPassed == 0) ? 0 : 7 - daysPassed;

            // Calculate the week number based on the starting day of the month
            int currentWeekNumber = GetCurrentWeekNumber(DateTime.Today);
            int weekNumber = currentWeekNumber;

            string weekInfo = $"{weekNumber:D2}{currentDate.Month:D2}{currentDate.Year}";
            string joinedStringConcat = string.Concat(weekInfo);
            int.TryParse(joinedStringConcat, out int WeekNo);




            Employee_Context Emp = new Employee_Context();
            List<Employee> employees = Emp.employees.ToList();

            foreach (var employee in employees)
            {
                // Check if the timesheet already exists for the current week and employee
                if (!TimesheetExists(WeekNo, employee.Username))
                {
                    // Create a new Timesheet record
                    Timesheet timesheet = new Timesheet
                    {
                        CreatedOn = DateTime.Now,
                        Owner = employee.Username,
                        Weekid = WeekNo,
                        // Set other day-specific values here
                       
                        Tt = 0,
                        Status = 0
                    };


                    var timesheetsToUpdate = db.timesheets.Where(c => c.Weekid < WeekNo).ToList();

                    foreach (var check in timesheetsToUpdate)
                    {
                        check.Status = 1;
                        db.SaveChanges();
                    }
                    // Insert the record into the "timesheet" table
                    db.timesheets.Add(timesheet);
                    db.SaveChanges();

                }
                else
                {

                }

            }


        }
        private int GetCurrentWeekNumber(DateTime date)
        {
            var cal = System.Globalization.CultureInfo.CurrentCulture.Calendar;
            int week = cal.GetWeekOfYear(date, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return week;
        }
        private string connectionString = ConfigurationManager.ConnectionStrings["Planning"].ConnectionString;
        public void PushEmail(string Recipient, string Subject, string Body, DateTime CreatedOn)
        {
            string query = "INSERT INTO OutgoingEmails (Recipient,Subject,Body,Status,CreatedOn,Trials,Response) VALUES " +
            "                                          (@Recipient,@Subject, @Body,0,@CreatedOn,@Trials,@Response)";
            using (SqlConnection connection = new SqlConnection(connectionString))
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
            using (SqlConnection connection = new SqlConnection(connectionString))
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
    }




}

