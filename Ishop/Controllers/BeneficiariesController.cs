﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IShop.Core;
using Ishop.Infa;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PagedList;
using Ishop.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace Ishop.Controllers
{
    [Authorize]
    public class BeneficiariesController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        private Benef_context db = new Benef_context();
        private Admission_context dbb = new Admission_context();
        // GET: Beneficiaries
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null) && (!(search == "")))
            {
                return View(dbb.admissions.OrderByDescending(p => p.Id).Where(c => c.Name.StartsWith(search) || c.Name == search || c.Name.Contains(search)).ToList().ToPagedList(page ?? 1, 11));

            }
            else if (search == "")
            {
                return View(dbb.admissions.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(dbb.admissions.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));
            }

        }
        // GET: Beneficiaries/Details/5
        public ActionResult Details(int? id)
        {
            benitem_context BBB = new benitem_context();
            var Benitm =BBB.benitems.Where(c=>c.Beneficiary_Id ==id).Select(c => c.Total).DefaultIfEmpty(0).Sum();
            ViewBag.BenfitsTotal = Benitm;
            var Benfits = dbb.admissions.Where(c=>c.Id == id).ToList();
            var Benfitss = BBB.benitems.Where(c => c.Beneficiary_Id == id).ToList();
            ViewBag.Bitem = Benfitss;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admission admission = dbb.admissions.Find(id);
            if (admission == null)
            {
                return HttpNotFound();
            }
            return View(admission);
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
        // GET: Beneficiaries/Create



      








       

        // GET: Beneficiaries/Edit/5
        public async Task <ActionResult> Edit(int? id)
        {

            Item_context AA = new Item_context();
            var Admm = AA.items.ToList();
            ViewBag.Admm = new SelectList(Admm, "Name", "Name");

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

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Benef benef = db.benefs.Find(id);
            if (benef == null)
            {
                return HttpNotFound();
            }
            return View(benef);
        }

        // POST: Beneficiaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Contacts,Country,Email,Total")] Benef benef)
        {
            if (ModelState.IsValid)
            {
                db.Entry(benef).State = EntityState.Modified;
                db.SaveChanges();
                
            }
            TempData["msg"] = "Data updated successfully ";
            return View(benef);
        }

        // GET: Beneficiaries/Delete/5
        public ActionResult Delete(int? id)
        {
            
            Benef benef = db.benefs.Find(id);
            db.benefs.Remove(benef);
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
