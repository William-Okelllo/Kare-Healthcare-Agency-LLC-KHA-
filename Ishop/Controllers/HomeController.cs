using Ishop.Infa;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Ishop.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {


            HodContext DD = new HodContext();
            var Depart = DD.hODs.Where(d => d.Staff == User.Identity.Name).Select(d => d.DprtName).ToList();
            ViewBag.Departmentt = Depart;


            //Timesheet_Context TC = new Timesheet_Context();
            //var TimmDetails = TC.timesheets.Where(c => Depart.Contains(c.Department) && c.Status == 0 && c.Locked == true).GroupBy(c => c.Department) .Select(group => new {DepartmentName = group.Key,Count = group.Count()}).ToList();

            // ViewBag.TimmDetails = TimmDetails;




            Timesheet_Context TT = new Timesheet_Context();
            var Sheet = TT.timesheets.Where(i => i.Owner == User.Identity.Name && DateTime.Now >= i.From_Date && DateTime.Now <= i.End_Date).FirstOrDefault();
            if(Sheet != null)
            {
                ViewBag.DirectHours = Sheet.Direct_Hours;
                ViewBag.InDirectHours = Sheet.InDirect_Hours;
                ViewBag.leave = Sheet.Leave;
                ViewBag.Total = Sheet.Tt;
            }
            else
            {
                ViewBag.DirectHours = 0;
                ViewBag.InDirectHours = 0;
                ViewBag.leave = 0;
                ViewBag.Total = 0;
            }


            return View();
        }



        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        private string connectionString = ConfigurationManager.ConnectionStrings["Planning"].ConnectionString;
        public void Glitch(string Message, string User)
        {
            Employee_Context emp = new Employee_Context();
            var ee = emp.employees.Where(c => c.Username == User).FirstOrDefault();

            string messagel = "I wanted to bring to your attention that I recently encountered a system glitch or error while using the platform."
            + "\n" + "The issue occurred on the following system url : " + Message
            + "\n" + "thank you ";
            string query = "INSERT INTO OutgoingEmails (Recipient,Subject,Body,Status,CreatedOn,Trials,Response) VALUES " +
            "                                          (@Recipient,@Subject, @Body,0,@CreatedOn,@Trials,@Response)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    var rr = "info@speculate.co.ke,"+ee.Email;
                    var cc = "System Glich";
                    command.Parameters.AddWithValue("@Recipient", rr);
                    command.Parameters.AddWithValue("@Subject", cc);
                    command.Parameters.AddWithValue("@Body", messagel);
                    command.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                    command.Parameters.AddWithValue("@Trials", 0);
                    command.Parameters.AddWithValue("@Response", "--waiting--");
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}