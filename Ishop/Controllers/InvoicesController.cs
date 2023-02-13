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
using Ishop.Models;
using EASendMail;
using System.Configuration;
using System.Data.SqlClient;

namespace Ishop.Controllers
{ [Authorize]
    public class InvoicesController : Controller
    {
        private InvoiceContext db = new InvoiceContext();

        // GET: Invoices
        public ActionResult Index(string searchBy, string search, int? page)
        {

            HREntities l = new HREntities();
            var boo = l.sp_invoices().ToList();
            ViewBag.l = boo;


            if (!(search == null))
            {
                return View(db.invoices.OrderByDescending(p => p.Id).Where(c => c.Client_Name == search).ToList().ToPagedList(page ?? 1, 6));

            }
            else
            {
                return View(db.invoices.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 6));


            }
        }

            // GET: Invoices/Details/5
            public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Client_Name,CreatedOn,Due_Date,Amount,Status,Additional_Notes")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Client_Name,CreatedOn,Due_Date,Amount,Status,Additional_Notes")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.invoices.Find(id);
            db.invoices.Remove(invoice);
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

        [Authorize(Roles = "Admin")]
        public ActionResult Paid(int? id, Invoice invoice )
        { try
            {
                string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
                SqlConnection sqlCon = new SqlConnection(strcon);
                SqlCommand sqlcmnd = new SqlCommand("sp_markpaid_Invoice", sqlCon);
                sqlcmnd.CommandType = CommandType.StoredProcedure;
                sqlcmnd.Parameters.AddWithValue("@Id", invoice.Id);
                sqlCon.Open();
                sqlcmnd.ExecuteNonQuery();
                sqlCon.Close();

            }
            catch
            {
                TempData["msg"] = "error occured in cancelling Shift ";
                return RedirectToAction("Mine");
            }

           
            {
                TempData["msg"] = "✔ Invoice Marked as Paid ";
                return RedirectToAction("Index");
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Cancelled(int? id, Invoice invoice)
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
                SqlConnection sqlCon = new SqlConnection(strcon);
                SqlCommand sqlcmnd = new SqlCommand("sp_markcancelled_Invoice", sqlCon);
                sqlcmnd.CommandType = CommandType.StoredProcedure;
                sqlcmnd.Parameters.AddWithValue("@Id", invoice.Id);
                sqlCon.Open();
                sqlcmnd.ExecuteNonQuery();
                sqlCon.Close();

            }
            catch
            {
                TempData["msg"] = "error occured in cancelling invoice ";
                return RedirectToAction("Index");
            }


            {
                TempData["msg"] = "✔ Invoiced Cancelled ";
                return RedirectToAction("Index");
            }
        }

    }
}
