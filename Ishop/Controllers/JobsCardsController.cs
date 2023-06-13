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
using static System.Net.Mime.MediaTypeNames;
using PagedList;

namespace Ishop.Controllers
{
    public class JobsCardsController : Controller
    {
        private Cardcontext db = new Cardcontext();

        // GET: JobsCards
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null))
            {
                return View(db.cards.Where(c => c.Vehicle_Reg == search || c.Vehicle_Reg.StartsWith(search)).ToList().ToPagedList(page ?? 1, 7));

            }
            else
            {
                return View(db.cards.Where(c => c.Vehicle_Reg.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 6));
            }




        }

        // GET: JobsCards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // GET: JobsCards/Create
        public ActionResult Create(int? id)
        {

            CheckInContext dbbb = new CheckInContext();
            var data10 = dbbb.checkIns.Where(d => d.Id == id);
            ViewBag.CheckIn=data10;

            PartContext Pd = new PartContext();
            var data11 = Pd.parts.Where(d => d.Booking_Id == id);
            ViewBag.Auto = data11;

            ServiceContext Pe = new ServiceContext();
            var data12 = Pe.services.Where(d => d.Booking_Id == id);
            ViewBag.Service = data12;

            decimal Autopart = Pd.parts.Where(d => d.Booking_Id == id).Sum(d =>d.Amount);
            decimal Services = Pe.services.Where(d => d.Booking_Id == id).Sum(d => d.Amount);
            decimal combinedSum = Autopart + Services;

            ViewBag.Estimate = combinedSum;

            return View();
        }

        // POST: JobsCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedOn,Booking_Id,Vehicle,Vehicle_Reg,Phone,Description,Customer,staff,Estimate")] Card card)
        {


            if (ModelState.IsValid)
            {
                db.cards.Add(card);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(card);
        }

        // GET: JobsCards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // POST: JobsCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Booking_Id,Vehicle,Vehicle_Reg,Phone,Description,Customer,staff")] Card card)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(card);
        }

        // GET: JobsCards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = db.cards.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // POST: JobsCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Card card = db.cards.Find(id);
            db.cards.Remove(card);
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
