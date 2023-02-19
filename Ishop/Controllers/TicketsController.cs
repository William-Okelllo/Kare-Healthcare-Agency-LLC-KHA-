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
using PagedList;
using EASendMail;
using System.Configuration;
using System.Data.SqlClient;

namespace Ishop.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private TicketContext db = new TicketContext();

        // GET: Tickets
        public ActionResult Index(string searchBy, string search, int? page)
        {


            if (!(search == null))
            {
                return View(db.tickets.OrderByDescending(p => p.id).Where(c => c.Pax_Name == search && (!(c.Ticket_status == 99))).ToList().ToPagedList(page ?? 1, 6));

            }
            else
            {
                return View(db.tickets.OrderByDescending(p => p.id).Where(c=>(!(c.Ticket_status == 99))).ToList().ToPagedList(page ?? 1, 6));


            }
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return PartialView("Details", ticket);
        }

        // GET: Tickets/Create
        public ActionResult New_Ticket()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New_Ticket([Bind(Include = "id,CreatedOn,CONS,Airline,Service_Provider,Pax_Name,Client,Currency,Staff,Inv_Amount,Net_Amount,Gross_Profit,Incentv,Recovery,Departure_Date,Arrival_Date,Routing,CTC,USD_ACC,Remarks,Ticket_status")] Ticket ticket)
        {

            ticket.Ticket_status = 0;
            if (ModelState.IsValid)
            {
                db.tickets.Add(ticket);
                db.SaveChanges();
                TempData["msg"] = "Ticket posted successfully ";
                return RedirectToAction("Index");
            }

            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Ticket_Update(int? id)
        {
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            if (ticket.Ticket_status ==1)
            {
                TempData["msg"] = "✔ Oops Ticket already approved";
                return RedirectToAction("Index");
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ticket_Update([Bind(Include = "id,CreatedOn,CONS,Airline,Service_Provider,Staff,Pax_Name,Client,Inv_Amount,Net_Amount,Gross_Profit,Incentv,Recovery,Departure_Date,Arrival_Date,Routing,CTC,USD_ACC,Remarks,Ticket_status")] Ticket ticket)
        {
            ticket.Ticket_status = 0;
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Ticket updated successfully ";
                return RedirectToAction("Index");
            }
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.tickets.Find(id);
            db.tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete_Ticket(int? id, Ticket ticket)
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
                SqlConnection sqlCon = new SqlConnection(strcon);
                SqlCommand sqlcmnd = new SqlCommand("sp_markticket_deleted", sqlCon);
                sqlcmnd.CommandType = CommandType.StoredProcedure;
                sqlcmnd.Parameters.AddWithValue("@Id", ticket.id);
                sqlCon.Open();
                sqlcmnd.ExecuteNonQuery();
                sqlCon.Close();
                TempData["msg"] = "Ticket deleted successfully ";
                return RedirectToAction("Index");


            }
            catch
            {
                TempData["msg"] = "error occured in  on deleting ticket ";
                return RedirectToAction("Index");
            }


            
               

        }
        public ActionResult Approve_Ticket(int? id, Ticket ticket)
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
                SqlConnection sqlCon = new SqlConnection(strcon);
                SqlCommand sqlcmnd = new SqlCommand("sp_markticket_Approved", sqlCon);
                sqlcmnd.CommandType = CommandType.StoredProcedure;
                sqlcmnd.Parameters.AddWithValue("@Id", ticket.id);
                sqlCon.Open();
                sqlcmnd.ExecuteNonQuery();
                sqlCon.Close();
                TempData["msg"] = "Ticket approved successfully ";
                return RedirectToAction("Index");


            }
            catch
            {
                TempData["msg"] = "error occured in  on approving ticket ";
                return RedirectToAction("Index");
            }





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
