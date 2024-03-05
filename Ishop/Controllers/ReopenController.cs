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
using IShop.Core.Interface;
using PagedList;
using System.Configuration;
using System.Data.SqlClient;

namespace Ishop
{
    [Authorize]
    public class ReopenController : Controller
    {
        private Timesheet_Context ddb = new Timesheet_Context();
        private Re_open_context db = new Re_open_context();

        // GET: Reopen
        public ActionResult Index(string searchBy, string search, int? page)
        {

            return View(db.reopens.OrderByDescending(p => p.CreatedOn).Where(c => c.Staff == User.Identity.Name).ToList().ToPagedList(page ?? 1, 11));

        }
        public ActionResult Requests(string searchBy, string search, int? page)
        {
            HodContext DD = new HodContext();
            var userDepartments = DD.hODs.Where(D => D.Staff == User.Identity.Name).Select(d => d.DprtName).ToList();

            // Assuming 'Department' is the property in 'reopens' that holds the department information
            var filteredRequests = db.reopens
                .OrderByDescending(p => p.CreatedOn)
                .Where(c => userDepartments.Contains(c.Department))
                .ToList();

            return View(filteredRequests.ToPagedList(page ?? 1, 11));
        }











        public ActionResult Re_open(int id, int Requestid)
        {

            var Sheet = ddb.timesheets.Find(id);

            Employee_Context EM = new Employee_Context();
            var Empl = EM.employees.Where(c => c.Username == Sheet.Owner).FirstOrDefault();

            var Request = db.reopens.Find(Requestid);
            if (Sheet != null)
            {
                Sheet.Locked = false;
                Sheet.Status = 0;
                ddb.SaveChanges();
            }

            if (Request != null)
            {
                Request.Status = 1;
                db.SaveChanges();
            }



            string Subject = "Timesheets Re-Opened  ";
            string message = "Hello " + Sheet.Owner
                + "\n" + "Kindly note your head of department has re-opened your timesheet. "
                + "\n" + "Timesheet Details "
                + "\n" + "  _   _   _  _ "
                + "\n" + "From Date " + Sheet.From_Date.ToString("dd - MMMM - yyyy") + " To Date " + Sheet.End_Date.ToString("dd - MMMM - yyyy") + " "
                + "\n" + "Project Time " + Sheet.Direct_Hours
                + "\n" + "Non Project Time " + Sheet.InDirect_Hours
                + "\n" + "Total Time  " + Sheet.Tt
                 + "\n" + "Automated Mail Notification - No Action Required";

            Employee_Context EmM = new Employee_Context();
            var Empsl = EmM.employees.Where(c => c.Username == Sheet.Owner).FirstOrDefault();
            PushEmail(Empsl.Email, Subject, message, DateTime.Now);
            TempData["msg"] = "Timesheet Re-open successfully  -   Staff : " + Sheet.Owner + " From Date " + Sheet.From_Date.ToString("dd - MMMM - yyyy") + " To Date " + Sheet.End_Date.ToString("dd - MMMM - yyyy");

            return RedirectToAction("Requests");
        }
        public ActionResult Deny(int id, int Requestid)
        {


            var Sheet = ddb.timesheets.Find(id);
            var Request = db.reopens.Find(Requestid);
            
            if (Request != null)
            {
                Request.Status = 2;
                db.SaveChanges();
            }



            string Subject = "Timesheets Re-Opened Request Denied  ";
            string message = "Hello " + Sheet.Owner
                + "\n" + "Kindly your request on re-openning requested timesheet has been denied by your head of department. "
                + "\n" + "Timesheet Details "
                + "\n" + "  _   _   _  _ "
                + "\n" + "From Date " + Sheet.From_Date.ToString("dd - MMMM - yyyy") + " To Date " + Sheet.End_Date.ToString("dd - MMMM - yyyy") + " "
                + "\n" + "Project Time " + Sheet.Direct_Hours
                + "\n" + "Non Project Time " + Sheet.InDirect_Hours
                + "\n" + "Total Time  " + Sheet.Tt
                 + "\n" + "Automated Mail Notification - No Action Required";

            Employee_Context EM = new Employee_Context();
            var Empl = EM.employees.Where(c => c.Username == Sheet.Owner).FirstOrDefault();
            PushEmail(Empl.Email, Subject, message, DateTime.Now);
            TempData["msg"] = "Timesheet Re-open request marked as denied  -   Staff : " + Sheet.Owner + " From Date " + Sheet.From_Date.ToString("dd - MMMM - yyyy") + " To Date " + Sheet.End_Date.ToString("dd - MMMM - yyyy");

            return RedirectToAction("Requests");
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











        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reopen reopen = db.reopens.Find(id);
            if (reopen == null)
            {
                return HttpNotFound();
            }
            return View(reopen);
        }
        private Timesheet_Context AA = new Timesheet_Context();
        public ActionResult Setid(int TimesheetId)
        {

            var data = AA.timesheets.FirstOrDefault(d => d.Id == TimesheetId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        // GET: Reopen/Create
        public ActionResult Re_quest()
        {
            Timesheet_Context TC = new Timesheet_Context();
            var Tiemsheets =TC.timesheets.Where(c=>c.Owner ==User.Identity.Name && c.Locked ==true).ToList();
            ViewBag.Tiemsheets = Tiemsheets;
            return View();
        }

        // POST: Reopen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Re_quest([Bind(Include = "Id,Timesheet_id,Reason,Department,Staff,CreatedOn,Status")] Reopen reopen)
        {
            if(reopen.Timesheet_id ==null)
            {
                TempData["msg"] = "✔ Note: use the select timesheet option to select the timesheet to be re-openned ";
                return RedirectToAction("Re_quest");
            }


            if (ModelState.IsValid)
            {
                db.reopens.Add(reopen);
                string Subject = "Request to Re-Open Timesheet  ";

                DepartmentContext DC = new DepartmentContext();
                var Depart = DC.departments.Where(D => D.DprtName == reopen.Department).Select(d => d.Email_Address).FirstOrDefault();

                string message = "Dear Sir/Madam  "
                    + "\n" + "Kindly note  "  + reopen.Staff + "  has submitted a request to re-open their timesheet. The details of the request are as follows: "
                    + "\n" + "Timesheet Details "
                    + "\n" + "  _   _   _  _ "
                    + "\n" + "Department  " + reopen.Department
                    + "\n" + "Timesheet Id " + reopen.Timesheet_id
                     + "\n" + "-            -              -"
                    + "\n" + "Reason for Re-Opening"
                    + "\n" + reopen.Reason
                     + "\n" + "------------" 
                    + "\n" + "Automated Mail Notification - No Action Required";

                Employee_Context EM = new Employee_Context();
                
                PushEmail(Depart, Subject, message, DateTime.Now);
                db.SaveChanges();
                TempData["msg"] = "Your Request  captured and submitted successfully to HOD";
                return RedirectToAction("Index");
            }

            return View(reopen);
        }

        private Timesheet_Context TT = new Timesheet_Context();
        public ActionResult GetTimesheet(int Part)
        {


            var data = TT.timesheets.FirstOrDefault(d => d.Id == Part);

            return Json(data, JsonRequestBehavior.AllowGet);
        }























        // GET: Reopen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reopen reopen = db.reopens.Find(id);
            if (reopen == null)
            {
                return HttpNotFound();
            }
            return View(reopen);
        }

        // POST: Reopen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Timesheet_id,Reason,Department,Staff")] Reopen reopen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reopen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reopen);
        }

        // GET: Reopen/Delete/5
        public ActionResult Delete(int? id)
        {
           
            Reopen reopen = db.reopens.Find(id);
            db.reopens.Remove(reopen);
            db.SaveChanges();
            TempData["msg"] = " Request  dropped  successfully ";
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
