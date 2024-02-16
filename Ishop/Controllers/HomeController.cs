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
           var Depart = DD.hODs.Where(d => d.Staff == User.Identity.Name).ToList();
           ViewBag.Departmentt = Depart;



            Direct_Activities_Context DA = new Direct_Activities_Context();
            Indirect_Activities_Context IDA = new Indirect_Activities_Context();
            DateTime currentDate = DateTime.Now.Date;
            var DirectA = DA.direct_Activities.Where(p => p.Day_Date == currentDate && p.User ==User.Identity.Name).Select(f=>f.Hours).DefaultIfEmpty(0).Sum();
            var InDirectA = IDA.indirect_Activities.Where(p => p.Day_Date == currentDate && p.User == User.Identity.Name).Select(f => f.Hours).DefaultIfEmpty(0).Sum();
            ViewBag.DirectHours = DirectA;
            ViewBag.InDirectHours = InDirectA;
            ViewBag.TotalHours = DirectA + InDirectA;


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