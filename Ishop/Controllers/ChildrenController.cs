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

namespace Ishop.Controllers
{
    public class ChildrenController : Controller
    {
        private Child_Context db = new Child_Context();

        // GET: Children
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null) && (!(search == "")))
            {
                return View(db.children.OrderByDescending(p => p.Id).Where(c =>  c.Project_No.ToString() == search || c.Fullnames.StartsWith(search) || c.Fullnames == search).ToList().ToPagedList(page ?? 1, 11));

            }
            else if (search == "")
            {
                return View(db.children.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(db.children.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));
            }

        }

        public ActionResult ValidateEmail(string email)
        {
            bool isValid = IsValidEmail(email);

            return Json(new { isValid = isValid });
        }

        public ActionResult CheckValueExists(int Project_No)
        {
            bool exists = db.children.Any(c => c.Project_No == Project_No);
            return Json(exists, JsonRequestBehavior.AllowGet);
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
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = db.children.Find(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            return View(child);
        }

        // GET: Children/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Children/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Project_No,Fullnames,DOB,Email_Address,PhoneNumber,Createdon")] Child child)
        {
            if (ModelState.IsValid)
            {
                db.children.Add(child);
                db.SaveChanges();
                TempData["msg"] = "Child data Captured successfully ";
                return RedirectToAction("Index");
            }

            return View(child);
        }

        // GET: Children/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Child child = db.children.Find(id);
            if (child == null)
            {
                return HttpNotFound();
            }
            return View(child);
        }

        // POST: Children/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Project_No,Fullnames,DOB,Email_Address,PhoneNumber,Createdon")] Child child)
        {
            if (ModelState.IsValid)
            {
                db.Entry(child).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Child data updated successfully ";
                return RedirectToAction("Index");
            }
            return View(child);
        }

       
       
        public ActionResult Delete(int id)
        {
            Child child = db.children.Find(id);
            db.children.Remove(child);
            db.SaveChanges();
            TempData["msg"] = "Child info delete successfully ";
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
