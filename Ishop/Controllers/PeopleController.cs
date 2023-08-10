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
    public class PeopleController : Controller
    {
        private PeopleContext db = new PeopleContext();

        // GET: People
        public ActionResult Index(string searchBy, string search, int? page)
        {


            if (!(search == null) && (!(search == "")))
            {
                return View(db.peoples.OrderByDescending(p => p.Id).Where(c => c.Name == search).ToList().ToPagedList(page ?? 1, 7));

            }

            else if (search == " ")
            {
                return View(db.peoples.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 7));

            }
            else
            {
                return View(db.peoples.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 7));

            }

        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            People people = db.peoples.Find(id);
            if (people == null)
            {
                return HttpNotFound();
            }
            return View(people);
        }

        // GET: People/Create
        public ActionResult Create(int id)
        {
            ViewBag.Event_id = id;
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Phone,Event_Id,Confirmed_Attendance")] People people)
        {


            if (ModelState.IsValid)
            {
                db.peoples.Add(people);
                db.SaveChanges();
                TempData["msg"] = "Attendee added successfully";
                return RedirectToAction("Details", "Events", new { id = people.Event_Id });
            }

            return View(people);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            People people = db.peoples.Find(id);
            if (people == null)
            {
                return HttpNotFound();
            }
            return View(people);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Phone,Event_Id,Confirmed_Attendance")] People people)
        {
            if (ModelState.IsValid)
            {
                db.Entry(people).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(people);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            People inclusion = db.peoples.Find(id);
            db.peoples.Remove(inclusion);
            db.SaveChanges();
            TempData["msg"] = "Attendee Dropped successfully ";
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
