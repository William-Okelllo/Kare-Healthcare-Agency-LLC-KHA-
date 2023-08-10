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
    [Authorize]
    public class EventsController : Controller
    {
        private EventsContext db = new EventsContext();

        // GET: Events
        public ActionResult Index(string searchBy, string search, int? page)
        {


            if (!(search == null) && (!(search == "")))
            {
                return View(db.events.OrderByDescending(p => p.Id).Where(c => c.Name == search && c.staff ==User.Identity.Name).ToList().ToPagedList(page ?? 1, 7));

            }

            else if (search == " ")
            {
                return View(db.events.OrderByDescending(p => p.Id).Where(c =>c.staff == User.Identity.Name).ToList().ToPagedList(page ?? 1, 7));

            }
            else
            {
                return View(db.events.OrderByDescending(p => p.Id).Where(c => c.staff == User.Identity.Name).ToList().ToPagedList(page ?? 1, 7));

            }

        }

        // GET: Events/Details/5
        public ActionResult Details(int? id, string search)
        {
            PeopleContext PPP = new PeopleContext();

            var Inviattions = PPP.peoples.Where(b => b.Event_Id == id).Count();
            var Confirmed =   PPP.peoples.Where(b => b.Event_Id == id && b.Confirmed_Attendance==true).Count();

            int Pending = Inviattions - Confirmed;

            ViewBag.Inviattions = Inviattions;
            ViewBag.Confirmed = Confirmed;
            ViewBag.Pending = Pending;





            if (!(search == null) && (!(search == "")))
            {
               
                var dataa = PPP.peoples.OrderByDescending(p => p.Id).Where(c => c.Event_Id == id && c.Phone == search || c.Phone.StartsWith(search));
                ViewBag.Attendees = dataa;
            }
            else
            {

                var data = PPP.peoples.OrderByDescending(p => p.Id).Where(c => c.Event_Id == id);
                ViewBag.Attendees = data;
            }

           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Event_date,Name,Venue,Type,staff")] Events events)
        {
            if (ModelState.IsValid)
            {
                db.events.Add(events);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(events);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Event_date,Name,Venue,Type,staff")] Events events)
        {
            if (ModelState.IsValid)
            {
                db.Entry(events).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(events);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            Events inclusion = db.events.Find(id);
            db.events.Remove(inclusion);
            db.SaveChanges();
            TempData["msg"] = "Event Dropped successfully ";
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
