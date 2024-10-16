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
using Ishop.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace Ishop.Controllers
{
    [Authorize]
    public class SheetController : Controller
    {
        private Sheet_context db = new Sheet_context();

        // GET: Sheet
        public ActionResult Calendar()
        {
            List<A> results = new List<A>();
            string strcon = ConfigurationManager.ConnectionStrings["Kare"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(strcon))
            {
                sqlCon.Open();

                using (SqlCommand command = new SqlCommand("sp_calendar_addon", sqlCon))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@staff", User.Identity.Name);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            A result = new A
                            {
                                Monday = Convert.ToDecimal(reader["Monday"]),
                                Tuesday = Convert.ToDecimal(reader["Tuesday"]),
                                Wednesday = Convert.ToDecimal(reader["Wednesday"]),
                                Thursday = Convert.ToDecimal(reader["Thursday"]),
                                Friday = Convert.ToDecimal(reader["Friday"]),
                                Saturday = Convert.ToDecimal(reader["Saturday"]),
                                Sunday = Convert.ToDecimal(reader["Sunday"]),
                            };

                            results.Add(result);
                        }
                    }
                }
            }

            ViewBag.Results = results;

            return View();
        }



        // GET: Sheet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sheet sheet = db.sheets.Find(id);
            if (sheet == null)
            {
                return HttpNotFound();
            }
            return View(sheet);
        }

        public ActionResult GetTimesheetData(string date)
        {
            // Convert the string date if necessary
            DateTime selectedDate = DateTime.Parse(date);

            // Fetch data based on the selected date (for example, from the database)
            var timesheetData = db.sheets.Where(c => c.Added == selectedDate && c.Staff ==User.Identity.Name).ToList();


            if (timesheetData == null || !timesheetData.Any())
            {
                return PartialView("_NoTimesheetData");
            }

            // You can return a partial view or JSON, depending on how you want to handle it
            return PartialView("GetTimesheetData", timesheetData);
        }






        // GET: Sheet/Create
        public ActionResult Post(DateTime selectedDate)
        {
            Employee_Context DBB = new Employee_Context();
            var Emm = DBB.employees.Where(c => c.Username == User.Identity.Name).FirstOrDefault();
            ViewBag.Emm = Emm;  

            ViewBag.selectedDate = selectedDate;    
            return View();
        }

        // POST: Sheet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Post([Bind(Include = "Id,Employee,Added,Start_time,End_time,Position,Description,Hours,staff")] Sheet sheet)
        {
            if (ModelState.IsValid)
            {
                db.sheets.Add(sheet);
                db.SaveChanges();
                TempData["msg"] = "Timeseheet posted successfully ";
                return RedirectToAction("Calendar");
            }

            return View(sheet);
        }

        // GET: Sheet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sheet sheet = db.sheets.Find(id);
            if (sheet == null)
            {
                return HttpNotFound();
            }
            return View(sheet);
        }

        // POST: Sheet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Employee,Added,Start_time,End_time,Position,Description,Hours,staff")] Sheet sheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sheet).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Timeseheet updated successfully ";
                
            }
            return RedirectToAction("Calendar");
        }

        // GET: Sheet/Delete/5
        public ActionResult Delete(int? id)
        {
            
            Sheet sheet = db.sheets.Find(id);
            db.sheets.Remove(sheet);
            db.SaveChanges();
            TempData["msg"] = "Timesheet dropped successfully ";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        
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
