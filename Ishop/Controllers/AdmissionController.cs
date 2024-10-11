using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IShop.Core;
using Ishop.Infa;
using PagedList;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Ishop.Controllers
{
    [Authorize]
    public class AdmissionController : Controller
    {
        private Admission_context db = new Admission_context();
        private static readonly HttpClient client = new HttpClient();
        // GET: Admission
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null) && (!(search == "")))
            {
                return View(db.admissions.OrderByDescending(p => p.Id).Where(c => c.Admin_No.StartsWith(search) || c.Name == search || c.Name.Contains(search)).ToList().ToPagedList(page ?? 1, 11));

            }
            else if (search == "")
            {
                return View(db.admissions.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(db.admissions.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));
            }

        }
        // GET: Admission/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admission admission = await db.admissions.FindAsync(id);
            if (admission == null)
            {
                return HttpNotFound();
            }
            return View(admission);
        }

        // GET: Admission/Create
        public async Task<ActionResult> Add()
        {
            string apiUrl = "https://restcountries.com/v3.1/all";
            List<string> countryNames = new List<string>();

            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    JArray countries = JArray.Parse(responseData);

                    // Extract country names
                    foreach (var country in countries)
                    {
                        var countryName = country["name"]["common"].ToString();
                        countryNames.Add(countryName);
                    }

                    // Sort the list of country names alphabetically
                    countryNames.Sort();
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during API call
                Console.WriteLine("Error fetching countries: " + ex.Message);
            }

            // Passing the list of countries to the ViewBag
            ViewBag.CountriesList = new SelectList(countryNames);






            List<int> years = new List<int>();
            int startYear = 2000;
            int currentYear = DateTime.Now.Year;

            for (int i = startYear; i <= currentYear; i++)
            {
                years.Add(i);
            }

            // Passing the list of years to the ViewBag
            ViewBag.YearsList = new SelectList(years);
            return View();
        }

        // POST: Admission/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add([Bind(Include = "Id,Name,Contacts,Email,Gender,Date,Admin_No,Country,Social_security,Work_Permit,Date,Year")] Admission admission)
        {






            if (ModelState.IsValid)
            {
                db.admissions.Add(admission);
                await db.SaveChangesAsync();
                TempData["msg"] = "Student Admitted successfully ";
                return RedirectToAction("Index");
            }

            return View(admission);
        }



        public ActionResult Performance(string Id)
        {
            Perform_context PP = new Perform_context();
            var Perfom = PP.performs.Where(c => c.Student == Id).ToList();

            // Get unique subjects and grades
            var subjects = Perfom.Select(p => p.Subjects).Distinct().ToList();
            var grades = Perfom.Select(p => p.Pass).Distinct().ToList();


            // Prepare data for the chart
            var datasets = subjects.Select(subject => new {
                subject = subject,
                data = grades.Select(grade => {
                    var performance = Perfom.FirstOrDefault(p => p.Subjects == subject && p.Pass == grade);
                    return performance?.Percentage ?? 0;
                }).ToList()
            }).ToList();

            ViewBag.Grades = JsonConvert.SerializeObject(grades);
            ViewBag.Datasets = JsonConvert.SerializeObject(datasets);
            ViewBag.Perfom = Perfom;

            return View();
        }










        // GET: Admission/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            string apiUrl = "https://restcountries.com/v3.1/all";
            List<string> countryNames = new List<string>();

            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    JArray countries = JArray.Parse(responseData);

                    // Extract country names
                    foreach (var country in countries)
                    {
                        var countryName = country["name"]["common"].ToString();
                        countryNames.Add(countryName);
                    }

                    // Sort the list of country names alphabetically
                    countryNames.Sort();
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during API call
                Console.WriteLine("Error fetching countries: " + ex.Message);
            }

            // Passing the list of countries to the ViewBag
            ViewBag.CountriesList = new SelectList(countryNames);






            List<int> years = new List<int>();
            int startYear = 2000;
            int currentYear = DateTime.Now.Year;

            for (int i = startYear; i <= currentYear; i++)
            {
                years.Add(i);
            }

            // Passing the list of years to the ViewBag
            ViewBag.YearsList = new SelectList(years);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admission admission = await db.admissions.FindAsync(id);
            if (admission == null)
            {
                return HttpNotFound();
            }
            return View(admission);
        }

        // POST: Admission/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Contacts,Email,Gender,Date,Admin_No,Country,Social_security,Work_Permit,Date,Year")] Admission admission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admission).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["msg"] = "Admission updated successfully ";
                return RedirectToAction("Index");
            }
            return View(admission);
        }

        // GET: Admission/Delete/5
        public ActionResult Delete(int? id)
        {
           
            Admission admission =  db.admissions.Find(id);
            db.admissions.Remove(admission);
             db.SaveChanges();
            TempData["msg"] = "Admission deleted successfully ";
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
