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
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Ishop.Controllers
{
    [Authorize]
    public class ImmigrantsController : Controller
    {
        private Immigrant_context db = new Immigrant_context();
        private static readonly HttpClient client = new HttpClient();
        // GET: Immigrants
        public ActionResult Index(string searchBy, string search, int? page)
        {


            if (!(search == null))
            {
                return View(db.immigrants.OrderByDescending(p => p.Id).Where(c => c.Name == search).ToList().ToPagedList(page ?? 1, 6));

            }
            else
            {
                return View(db.immigrants.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 6));


            }
        }
        // GET: Immigrants/Details/5
        public  ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Immigrant immigrant =  db.immigrants.Find(id);
            if (immigrant == null)
            {
                return HttpNotFound();
            }
            return View(immigrant);
        }
        public ActionResult ValidateEmail(string email)
        {
            bool isValid = IsValidEmail(email);

            return Json(new { isValid = isValid });
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Regex pattern to validate email addresses
            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);

            return regex.IsMatch(email);
        }
        // GET: Immigrants/Create
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

        // POST: Immigrants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,Name,Contacts,Email,Gender,Country,Social_security,Work_Permit,Date,Year")] Immigrant immigrant)
        {
            if (ModelState.IsValid)
            {
                db.immigrants.Add(immigrant);
                 db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(immigrant);
        }

        // GET: Immigrants/Edit/5
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
            Immigrant immigrant = await db.immigrants.FindAsync(id);
            if (immigrant == null)
            {
                return HttpNotFound();
            }
            return View(immigrant);
        }

        // POST: Immigrants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Contacts,Email,Gender,Country,Social_security,Work_Permit,Date,Year")] Immigrant immigrant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(immigrant).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(immigrant);
        }

        // GET: Immigrants/Delete/5
        public ActionResult Delete(int? id)
        {
            Immigrant immigrant =  db.immigrants.Find(id);
            db.immigrants.Remove(immigrant);
             db.SaveChanges();
            TempData["msg"] = "Data deleted successfully ";
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
