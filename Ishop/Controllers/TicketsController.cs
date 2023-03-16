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
using EASendMail;
using System.Configuration;
using System.Data.SqlClient;
using Ishop.Models;
using Rotativa;

namespace Ishop.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private TicketContext db = new TicketContext();

        // GET: Tickets
        public ActionResult Index(string searchBy, string search, int? page)
        {
            
            

            if (this.User.IsInRole("Tickets_Approval"))
            {

                if (!(search == null) && (!(search == "")))
                {
                    return View(db.tickets.OrderByDescending(p => p.id).Where(c => c.Pax_Name == search && c.Ticket_status != 99).ToList().ToPagedList(page ?? 1, 15));

                }
            
            else if (search == " ")
            {
                return View(db.tickets.OrderByDescending(p => p.id).Where(c => c.Pax_Name.StartsWith(search) ||  c.Ticket_status != 99).ToList().ToPagedList(page ?? 1, 15));

            }
            else
            {
                return View(db.tickets.OrderByDescending(p => p.id).Where(c => c.Pax_Name.StartsWith(search) || c.Ticket_status != 99).ToList().ToPagedList(page ?? 1, 15));

            }

        }
            else
            {
                if (!(search == null))
                {
                    return View(db.tickets.OrderByDescending(p => p.id).Where(c => c.Pax_Name == search && c.Staff==User.Identity.Name && (!(c.Ticket_status == 99))).ToList().ToPagedList(page ?? 1, 15));

                }
                else if (search == null)
                {
                    return View(db.tickets.OrderByDescending(p => p.id).Where(c => c.Pax_Name.StartsWith(search) ||  (!(c.Ticket_status == 99))  && c.Staff == User.Identity.Name).ToList().ToPagedList(page ?? 1, 15));


                }
                else
                {
                    return View(db.tickets.OrderByDescending(p => p.id).Where(c => c.Pax_Name.StartsWith(search) || (!(c.Ticket_status == 99))).ToList()
                        .ToPagedList(page ?? 1, 6));
                }
            }
        }

        public ActionResult Groups(string searchBy, string search, int? page)
        {



            if (this.User.IsInRole("Tickets_Approval"))
            {

                if (!(search == null) && (!(search == "")))
                {
                    return View(db.tickets.OrderByDescending(p => p.id).Where(c => c.Group_id == search && c.Ticket_status != 99 && c.Group_ticket==true).ToList().ToPagedList(page ?? 1, 15));

                }

                else if (search == " ")
                {
                    return View(db.tickets.OrderByDescending(p => p.id).Where(c => c.Group_id.StartsWith(search) || c.Ticket_status != 99 && c.Group_ticket == true).ToList().ToPagedList(page ?? 1, 15));

                }
                else
                {
                    return View(db.tickets.OrderByDescending(p => p.id).Where(c => c.Group_id.StartsWith(search) || c.Ticket_status != 99 && c.Group_ticket == true).ToList().ToPagedList(page ?? 1, 15));

                }

            }
            else
            {
                if (!(search == null))
                {
                    return View(db.tickets.OrderByDescending(p => p.id).Where(c => c.Group_id == search && c.Staff == User.Identity.Name && (!(c.Ticket_status == 99)) && c.Group_ticket == true).ToList().ToPagedList(page ?? 1, 15));

                }
                else if (search == null)
                {
                    return View(db.tickets.OrderByDescending(p => p.id).Where(c => c.Group_id.StartsWith(search) || (!(c.Ticket_status == 99)) && c.Staff == User.Identity.Name && c.Group_ticket == true ).ToList().ToPagedList(page ?? 1, 15));


                }
                else
                {
                    return View(db.tickets.OrderByDescending(p => p.id).Where(c => c.Group_id.StartsWith(search) || (!(c.Ticket_status == 99)) && c.Group_ticket == true).ToList()
                        .ToPagedList(page ?? 1, 6));
                }
            }
        }
        // GET: Tickets/Details/5
        public ActionResult Details(int id)
        {
            tikos_logs dbb = new tikos_logs();

            var data10 = dbb.Ticket_logs.Where(c => c.Ticket_id==id).ToList();
            ViewBag.F = data10;


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

       
           
            public JsonResult GetDropdownData(string searchTerm)
        {
            tickets_datasetsv2 dbbbb = new tickets_datasetsv2();
            var data = dbbbb.Ticketing_Airlines
                            .Where(x => x.Airline.Contains(searchTerm))
                            .OrderBy(x => x.Airline)
                            .Select(x => new { id = x.id, FieldName = x.Airline })
                            .ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }




            // GET: Tickets/Create
            public ActionResult New_Ticket()
        {
            tickets_datasetsv2 dbb = new tickets_datasetsv2();
            var categories = dbb.Ticketing_Airlines.ToList();
            ViewBag.Categories = new SelectList(categories, "Airline", "Airline");

            tickets_datasetsv2 routelist = new tickets_datasetsv2();
            var routes = routelist.Ticketing_Routes.ToList();
            ViewBag.routes = new SelectList(routes, "Routing", "Routing");

            tickets_datasetsv2 service = new tickets_datasetsv2();
            var servicep = service.Ticketing_service_providers.ToList();
            ViewBag.servicep = new SelectList(servicep, "Service_Provider", "Service_Provider");

            tickets_datasetsv2 pp = new tickets_datasetsv2();
            var payment = pp.Ticket_payment_modes.ToList();
            ViewBag.payment = new SelectList(payment, "Mode", "Mode");

            tickets_datasetsv2 cc = new tickets_datasetsv2();
            var client = cc.Ticketing_Clients.ToList();
            ViewBag.client = new SelectList(client, "Client", "Client");

            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New_Ticket([Bind(Include = "id,CreatedOn,Invoice_As_One,issue_Date,Group_ticket,Ticket_No,Group_id,Airline,Mode,Ticketing_System,Service_Provider,Pax_Name,Payer,Client,Currency,Staff,Inv_Amount,Net_Amount,Incentv,Recovery,Departure_Date,Arrival_Date,Routing,CTC,Remarks,Ticket_status")] Ticket ticket)
        {

            ticket.Ticket_status = 0;
            ticket.Gross_Profit = (((ticket.Inv_Amount - ticket.Net_Amount) - (ticket.Recovery + ticket.Incentv)));
            if (ticket.Group_ticket == true) { ticket.Group_id = ticket.Client + " - " + ticket.Group_id; }
            else { ticket.Group_id = null; }
            if (ModelState.IsValid)
            {
                

                db.tickets.Add(ticket);
                db.SaveChanges();
                try
                {
                    string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
                    SqlConnection sqlCon = new SqlConnection(strcon);
                    SqlCommand sqlcmnd = new SqlCommand("sp_Ticketlog_tracker", sqlCon);
                    sqlcmnd.CommandType = CommandType.StoredProcedure;
                    sqlcmnd.Parameters.AddWithValue("@Id", ticket.id);
                    sqlcmnd.Parameters.AddWithValue("@Action", "Ticket Created");
                    sqlcmnd.Parameters.AddWithValue("@User", User.Identity.Name);
                    sqlCon.Open();
                    sqlcmnd.ExecuteNonQuery();
                    sqlCon.Close();
                   
                }
                catch
                {
                   
                    
                }


                TempData["msg"] = "Ticket posted successfully ";
                
                if(ticket.Group_ticket==true)
                { return RedirectToAction("groups"); }else { return RedirectToAction("Index"); }
            }

            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Ticket_Update(int? id)
        {
            tickets_datasetsv2 dbb = new tickets_datasetsv2();
            var categories = dbb.Ticketing_Airlines.ToList();
            ViewBag.Categories = new SelectList(categories, "Airline", "Airline");

            tickets_datasetsv2 routelist = new tickets_datasetsv2();
            var routes = routelist.Ticketing_Routes.ToList();
            ViewBag.routes = new SelectList(routes, "Routing", "Routing");

            tickets_datasetsv2 service = new tickets_datasetsv2();
            var servicep = service.Ticketing_service_providers.ToList();
            ViewBag.servicep = new SelectList(servicep, "Service_Provider", "Service_Provider");

            tickets_datasetsv2 pp = new tickets_datasetsv2();
            var payment = pp.Ticket_payment_modes.ToList();
            ViewBag.payment = new SelectList(payment, "Mode", "Mode");

            tickets_datasetsv2 cc = new tickets_datasetsv2();
            var clientlist = cc.Ticketing_Clients.ToList();
            ViewBag.clientlist = new SelectList(clientlist, "Client", "Client");


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
        public ActionResult Ticket_Update([Bind(Include = "id,Group_ticket,Invoice_As_One,Group_id,issue_Date,Ticket_No,CreatedOn,CONS,Airline,Mode,Ticketing_System,Service_Provider,Payer,Staff,Pax_Name,Client,Inv_Amount,Net_Amount,Gross_Profit,Incentv,Recovery,Departure_Date,Arrival_Date,Routing,CTC,Remarks,Ticket_status")] Ticket ticket)
        {
            ticket.Ticket_status = 0;
            if (ModelState.IsValid)
            {
                ticket.Gross_Profit = (((ticket.Inv_Amount - ticket.Net_Amount) - (ticket.Recovery + ticket.Incentv)));
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                try
                {
                    string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
                    SqlConnection sqlCon = new SqlConnection(strcon);
                    SqlCommand sqlcmnd = new SqlCommand("sp_Ticketlog_tracker", sqlCon);
                    sqlcmnd.CommandType = CommandType.StoredProcedure;
                    sqlcmnd.Parameters.AddWithValue("@Id", ticket.id);
                    sqlcmnd.Parameters.AddWithValue("@Action", "Ticket Editted");
                    sqlcmnd.Parameters.AddWithValue("@User", User.Identity.Name);
                    sqlCon.Open();
                    sqlcmnd.ExecuteNonQuery();
                    sqlCon.Close();

                }
                catch
                {


                }
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
                sqlcmnd.Parameters.AddWithValue("@Action", "Ticket Approved");
                sqlcmnd.Parameters.AddWithValue("@User", User.Identity.Name);
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

        public ActionResult Ticket_Invoicing(int? id, Ticket ticket)
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
                SqlConnection sqlCon = new SqlConnection(strcon);
                SqlCommand sqlcmnd = new SqlCommand("sp_markticket_Invoiced", sqlCon);
                sqlcmnd.CommandType = CommandType.StoredProcedure;
                sqlcmnd.Parameters.AddWithValue("@Id", ticket.id);
                sqlcmnd.Parameters.AddWithValue("@Action", "Ticket Invoiced");
                sqlcmnd.Parameters.AddWithValue("@User", User.Identity.Name);
                sqlCon.Open();
                sqlcmnd.ExecuteNonQuery();
                sqlCon.Close();
                TempData["msg"] = "Ticket Invoiced successfully ";
                return RedirectToAction("Index");

                   
                }
            catch
            {
                TempData["msg"] = "error occured in  on Invoiced ticket ";
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


        public ActionResult View_Grp(string idd ,Ticket ticket)
        {
            tike_print Rec=new tike_print();
            var bnb = Rec.sp_print_grp(idd).ToList();
            ViewBag.pp = bnb;

            tike_print Recc = new tike_print();
            var bau = Recc.sp_print_grp_InvTT(idd).ToList();
            ViewBag.R = bau;

            
            return View();
        }
        
    }
}
