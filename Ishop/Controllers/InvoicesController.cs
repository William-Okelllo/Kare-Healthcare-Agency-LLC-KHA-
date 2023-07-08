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
using System.Configuration;
using System.Data.SqlClient;
using Rotativa;

namespace Ishop.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        private Invoice_Context db = new Invoice_Context();

        // GET: Invoices
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null))
            {
                return View(db.invoices.OrderByDescending(p => p.Id).Where(c => c.Vehicle_Reg == search || c.Vehicle_Reg.StartsWith(search)).ToList().ToPagedList(page ?? 1, 7));

            }
            else
            {
                return View(db.invoices.OrderByDescending(p => p.Id).Where(c => c.Vehicle_Reg.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 6));
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
        public ActionResult Create([Bind(Include = "Id,CreatedOn,Booking_Id,Vehicle,Vehicle_Reg,Phone,Customer,staff,Invoice_Status,VAT,Invoice_Amount")] Invoice invoice)
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
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Booking_Id,Vehicle,Vehicle_Reg,Phone,Customer,staff,Invoice_Status,VAT,Invoice_Amount")] Invoice invoice)
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

        public ActionResult Mark_Paid (int id)
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["GRS"].ConnectionString;
                SqlConnection sqlCon = new SqlConnection(strcon);
                SqlCommand cmd = new SqlCommand("markinv_piad", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InvoiceId", id);
                sqlCon.Open();
                cmd.ExecuteNonQuery();
                sqlCon.Close();

            }
            catch { }
            TempData["msg"] = "Invoice makred paid successfully";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }


        public ActionResult Invoice_Print(int id)
        {

            Inspection_Context III= new Inspection_Context();

            var card = III.inspections.FirstOrDefault(c => c.Id == id);


            var card2 = III.inspections.Where(c => c.Id == id);
            ViewBag.data20 = card2;

            Inspections_partsContext bb = new Inspections_partsContext();
            var data14 = bb.inspections_Parts.Where(d => d.Booking_Id == card.Booking_Id);


            Inspec_serv_context nn = new Inspec_serv_context();
            var data15 = nn.Inspection_Servs.Where(d => d.Booking_Id == card.Booking_Id);


            decimal Autopart = bb.inspections_Parts.Where(d => d.Booking_Id == card.Booking_Id).Select(d => d.Amount).DefaultIfEmpty(0).Sum();
            decimal Services = nn.Inspection_Servs.Where(d => d.Booking_Id == card.Booking_Id).Select(d => d.Amount).DefaultIfEmpty(0).Sum();
            decimal combinedSum = Autopart;
            decimal combinedSum2 = Services;
            ViewBag.Estimate1 = combinedSum + combinedSum2;


            decimal Autopart2 = bb.inspections_Parts.Where(d => d.Booking_Id == card.Booking_Id).Select(d => d.Total_Amount).DefaultIfEmpty(0).Sum();
            decimal Services2 = nn.Inspection_Servs.Where(d => d.Booking_Id == card.Booking_Id).Select(d => d.Total_Amount).DefaultIfEmpty(0).Sum();
            decimal combinedSum222 = Autopart;
            decimal combinedSum2222 = Services;
            ViewBag.Estimate2 = combinedSum2 + combinedSum2222;









            Inspections_partsContext Pd = new Inspections_partsContext();
            var data11 = Pd.inspections_Parts.Where(d => d.Booking_Id == card.Booking_Id);
            ViewBag.Auto = data11;

            Inspec_serv_context Pe = new Inspec_serv_context();
            var data12 = Pe.Inspection_Servs.Where(d => d.Booking_Id == card.Booking_Id);
            ViewBag.Service = data12;


            var FF = III.inspections.Where(c => c.Id == id).ToList();
            ViewBag.F = FF;

            {
                Inspection customer = III.inspections.FirstOrDefault(c => c.Id == id);

                var report = new PartialViewAsPdf("~/Views/invoices/Invoice_Print.cshtml", customer);
                return report;
            }
        }
    }
}
