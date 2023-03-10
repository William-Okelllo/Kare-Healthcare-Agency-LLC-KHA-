using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Ishop.Models;
using PagedList;

namespace Ishop.Controllers
{
    public class payment_modesController : Controller
    {
        private tickets_datasetsv2 db = new tickets_datasetsv2();

        // GET: payment_modes
        public ActionResult Index(string searchBy, string search, int? page)
        {


            if (!(search == null) && (!(search == "")))
            {
                return View(db.Ticket_payment_modes.OrderByDescending(p => p.id).Where(c => c.Mode == search).ToList().ToPagedList(page ?? 1, 6));

            }

            else if (search == " ")
            {
                return View(db.Ticket_payment_modes.OrderByDescending(p => p.id).ToList().ToPagedList(page ?? 1, 6));

            }
            else
            {
                return View(db.Ticket_payment_modes.OrderByDescending(p => p.id).ToList().ToPagedList(page ?? 1, 6));

            }

        }

        // GET: payment_modes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket_payment_modes ticket_payment_modes = db.Ticket_payment_modes.Find(id);
            if (ticket_payment_modes == null)
            {
                return HttpNotFound();
            }
            return View(ticket_payment_modes);
        }

        // GET: payment_modes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: payment_modes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Mode,CreatedOn")] Ticket_payment_modes ticket_payment_modes)
        {
            bool isProductExist = db.Ticket_payment_modes.Any(p => p.Mode == ticket_payment_modes.Mode);
            if (isProductExist)
            {
                ModelState.AddModelError("PaymentMode", "Product already exists.");
            }

            if (ModelState.IsValid)
            {
                
                
                
                    db.Ticket_payment_modes.Add(ticket_payment_modes);
                    db.SaveChanges();
                TempData["msg"] = "Payment Mode added successfully ";
                return RedirectToAction("Index");
                
            }

            return View(ticket_payment_modes);
        }

        // GET: payment_modes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket_payment_modes ticket_payment_modes = db.Ticket_payment_modes.Find(id);
            if (ticket_payment_modes == null)
            {
                return HttpNotFound();
            }
            return View(ticket_payment_modes);
        }

        // POST: payment_modes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Mode,CreatedOn")] Ticket_payment_modes ticket_payment_modes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket_payment_modes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticket_payment_modes);
        }

        // GET: payment_modes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket_payment_modes ticket_payment_modes = db.Ticket_payment_modes.Find(id);
            if (ticket_payment_modes == null)
            {
                return HttpNotFound();
            }
            return View(ticket_payment_modes);
        }

        // POST: payment_modes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket_payment_modes ticket_payment_modes = db.Ticket_payment_modes.Find(id);
            db.Ticket_payment_modes.Remove(ticket_payment_modes);
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
