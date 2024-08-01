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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Ishop.Controllers
{
    [Authorize]
    public class leave_BalanceController : Controller
    {
        private leaves_Days_trackContext db = new leaves_Days_trackContext();
        private Employee_Context dbb = new Employee_Context();
        // GET: leave_Balance
        public ActionResult Index(string search, int? page)
        {
            if (!(search == null) && (!(search == "")))
            {
                return View(dbb.employees.OrderByDescending(p => p.Id).Where(c => c.Username.StartsWith(search) || c.Username == search || c.Fullname.Contains(search) && c.Active==true).ToList().ToPagedList(page ?? 1, 12));

            }
            else if (search == "")
            {
                return View(dbb.employees.OrderByDescending(p => p.Id).Where(c=>c.Active == true).ToList().ToPagedList(page ?? 1, 12));


            }
            else
            {
                return View(dbb.employees.OrderByDescending(p => p.Id).Where(c => c.Active == true).ToList().ToPagedList(page ?? 1, 12));
            }
        }



        public ActionResult Acc_View(string Username )
        {
            var Employee = dbb.employees.Where(c => c.Username == Username).FirstOrDefault();
            ViewBag.Emp= Employee;
            var Account = db.Leaves_Days_Tracks.Where(c => c.Username == Username).ToList();
            ViewBag.Account = Account;
            return View();
        }

        public ActionResult Insert_days(string Username)
        {
            leaveTypesContext LVV = new leaveTypesContext();
            var existingRecords = db.Leaves_Days_Tracks.Where(c => c.Username == Username).ToList();
            var Parts = LVV.leaves_Types.ToList();

            leaveContext LLL = new leaveContext();

            Random random = new Random();

            using (var In_parts = new leaves_Days_trackContext())
            {
                foreach (var part in Parts)
                {
                    // Calculate the sum of RequestedDays for the specific type of leave
                    var RequestedDays = LLL.leave
                        .Where(x => x.Employee == Username && x.Status == 2 && x.Type == part.Type)
                        .Select(c => c.Days)
                        .DefaultIfEmpty(0)
                        .Sum();

                    // Check if a record with the same Username and Type already exists
                    bool recordExists = existingRecords.Any(r => r.Type == part.Type);

                    if (!recordExists)
                    {
                        leaves_Days_track inspections_Parts = new leaves_Days_track
                        {
                            Id = random.Next(),
                            Requested_leaves = RequestedDays, // Set Requested_leaves for this specific leave type
                            Remaining_leaves = part.Days - RequestedDays,
                            Username = Username,
                            Type = part.Type,
                            Total_leaves_per_year = part.Days
                        };

                        In_parts.Leaves_Days_Tracks.Add(inspections_Parts);
                    }
                }

                In_parts.SaveChanges();
            }

            TempData["msg"] = "Leave Balance fetched successfully";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }



        public ActionResult Purge(string Username)
        {
            leaveTypesContext LVV = new leaveTypesContext();
            var existingRecords = db.Leaves_Days_Tracks.Where(c => c.Username == Username).ToList();
            var Parts = LVV.leaves_Types.ToList();


            using (var In_parts = new leaves_Days_trackContext())
            {


                foreach (var part in Parts)
                {

                    // Find records with the specified Type and Username (if applicable) that should be deleted
                    var recordsToDelete = In_parts.Leaves_Days_Tracks
                        .Where(r => r.Type == part.Type && r.Username == Username)
                        .ToList();

                    // Remove each record from the context
                    foreach (var record in recordsToDelete)
                    {
                        In_parts.Leaves_Days_Tracks.Remove(record);
                    }


                    // Save changes to the database
                    In_parts.SaveChanges();

                }

                TempData["msg"] = "Leave Tracker Cleanup completed";
                string returnUrl = Request.UrlReferrer.ToString();
                return Redirect(returnUrl);
            }
        }






            public ActionResult Insert_daysAll()
        {
            var activeEmployees = dbb.employees.Where(c => c.Active == true).ToList();
            leaveTypesContext LVV = new leaveTypesContext();
            var Parts = LVV.leaves_Types.ToList();
            Random random = new Random();

            using (var In_parts = new leaves_Days_trackContext())
            {
                foreach (var employee in activeEmployees)
                {
                    var existingRecords = db.Leaves_Days_Tracks.Where(c => c.Username == employee.Username).ToList();

                    foreach (var part in Parts)
                    {
                        // Check if a record with the same Username and Type already exists
                        bool recordExists = existingRecords.Any(r => r.Type == part.Type);

                        if (!recordExists)
                        {
                            leaves_Days_track inspections_Parts = new leaves_Days_track
                            {
                                Id = random.Next(),
                                Requested_leaves = 0,
                                Remaining_leaves = part.Days,
                                Username = employee.Username,
                                Type = part.Type,
                                Total_leaves_per_year = part.Days
                            };

                            In_parts.Leaves_Days_Tracks.Add(inspections_Parts);
                        }
                    }
                }
                In_parts.SaveChanges();
            }
            TempData["msg"] = "Leave Balance fetched successfully";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }




        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            leaves_Days_track leaves_Days_track = db.Leaves_Days_Tracks.Find(id);
            if (leaves_Days_track == null)
            {
                return HttpNotFound();
            }
            return View(leaves_Days_track);
        }

        // POST: leave_Balance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Total_leaves_per_year,Requested_leaves,Type,Remaining_leaves,Username")] leaves_Days_track leaves_Days_track)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leaves_Days_track).State = EntityState.Modified;
                db.SaveChanges();
                
            }
            TempData["msg"] = "✔   Phase added successfully ";
            return RedirectToAction("Acc_View", "leave_Balance", new { Username = leaves_Days_track.Username });
        }

        // GET: leave_Balance/Delete/5
        public ActionResult Delete(int? id)
        {
            leaves_Days_track leaves_Days_track = db.Leaves_Days_Tracks.Find(id);
            db.Leaves_Days_Tracks.Remove(leaves_Days_track);
            db.SaveChanges();
            TempData["msg"] = "leave dropped successfully ";
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
