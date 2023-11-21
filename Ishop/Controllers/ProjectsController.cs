using Ishop.Infa;
using Ishop.Models;
using IShop.Core;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ishop.Controllers
{
    [Authorize]  
    public class ProjectsController : Controller
    {
        private Project_Context db = new Project_Context();

        // GET: Projects
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null) && (!(search == "")))
            {
                return View(db.projects.OrderByDescending(p => p.Id).Where(c => c.Project_Name.StartsWith(search) || c.Project_Name == search).ToList().ToPagedList(page ?? 1, 11));

            }
            else if (search == "")
            {
                return View(db.projects.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(db.projects.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));
            }

        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {

            string strcon = ConfigurationManager.ConnectionStrings["Planning"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(strcon))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Project_details", sqlCon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Projectid", id);

                    sqlCon.Open();

                    // Use SqlDataReader to read the result set
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Check if there are rows in the result set
                        if (reader.HasRows)
                        {
                            // Create a list to store the results
                            List<Muru> resultList = new List<Muru>();

                            // Read each row in the result set
                            while (reader.Read())
                            {
                                // Map the columns to properties in your model
                                Muru resultItem = new Muru
                                {
                                    Project_Name = reader["Project_Name"].ToString(),
                                    Budget = Convert.ToDecimal(reader["Budget"]),
                                    Phase = reader["Phase"].ToString(),
                                    logged = Convert.ToDecimal(reader["logged"]),
                                    Balance = Convert.ToDecimal(reader["Balance"]),
                                    StartDate = Convert.ToDateTime(reader["Start_Date"]),
                                    End_Date = Convert.ToDateTime(reader["End_Date"])
                                };

                                // Add the result to the list
                                resultList.Add(resultItem);
                            }

                            // Assign the list to ViewBag or ViewData
                            ViewBag.ResultList = resultList;
                        }
                        else
                        {
                            ViewBag.ResultList = new List<Muru>();
                        }
                    }
                }
            }















            Phase_Context AA = new Phase_Context();
            var Phase = AA.phases.Where(a => a.Project_id == id).ToList();
            ViewBag.Phase = Phase;

            var Projectinfo = db.projects.Where(p => p.Id == id).FirstOrDefault();

            Activities_Context AC = new Activities_Context();
            var Activities = AC.activities.Where(c => c.Project_Name == Projectinfo.Project_Name).Select(d => d.Charge).DefaultIfEmpty(0).Sum();
            ViewBag.Performnce = Activities;



            Team_Context T = new Team_Context();
            var Team = T.teams.Where(a => a.Project_id == id).ToList();
            ViewBag.Team = Team;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {

            Direct_Context DC = new Direct_Context();
            var DirectC = DC.directs.ToList();
            ViewBag.Direct = new SelectList(DirectC, "Name", "Name");



            PCategory_Context PC = new PCategory_Context();
            var Category = PC.pcategories.ToList();
            ViewBag.Category = new SelectList(Category, "Category", "Category");



            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedOn,Project_Name,location,Status,Budget,Category,Client,Fee_Budget")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.projects.Add(project);
                db.SaveChanges();
                TempData["msg"] = "✔   Project added successfully ";
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Project_Name,location,Status,Budget,Category,Client,Fee_Budget")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "✔   Project updated successfully ";
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.projects.Find(id);
            db.projects.Remove(project);
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
    }
}
