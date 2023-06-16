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
using System.Configuration;
using System.Data.SqlClient;
using Rotativa;

namespace Ishop.Controllers
{
    [Authorize]
    public class EstimateController : Controller
    {
        private Cardcontext db = new Cardcontext();

        // GET: JobsCards
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null))
            {
                return View(db.cards.OrderByDescending(p => p.Id).Where(c => c.Vehicle_Reg == search || c.Vehicle_Reg.StartsWith(search)).ToList().ToPagedList(page ?? 1, 7));

            }
            else
            {
                return View(db.cards.OrderByDescending(p => p.Id).Where(c => c.Vehicle_Reg.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 6));
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
            
            var data1 = db.cards.OrderByDescending(p => p.Id).Where(c => c.Id == id).ToList();
            ViewBag.F = data1;

            bool exists = db.cards.Any(m => m.Booking_Id == id );
            if (exists)
            {
                TempData["msg"] = " Estimate already created";
                return RedirectToAction("Index", "CheckIns");
            }


            CheckInContext dbbb = new CheckInContext();
            var data10 = dbbb.checkIns.Where(d => d.Id == id);
            ViewBag.CheckIn=data10;

            PartContext Pd = new PartContext();
            var data11 = Pd.parts.Where(d => d.Booking_Id == id);
            ViewBag.Auto = data11;

            ServiceContext Pe = new ServiceContext();
            var data12 = Pe.services.Where(d => d.Booking_Id == id);
            ViewBag.Service = data12;

            decimal Autopart = Pd.parts.Where(d => d.Booking_Id == id).Select(d => d.Total_Amount).DefaultIfEmpty(0).Sum();
            decimal Services = Pe.services.Where(d => d.Booking_Id == id).Select(d => d.Total_Amount).DefaultIfEmpty(0).Sum();
            decimal combinedSum = Autopart;
            decimal combinedSum2 = Services;
            ViewBag.Estimate = combinedSum + combinedSum2;

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


        public ActionResult Estimate_Print(int id)
        {
            var card = db.cards.FirstOrDefault(c => c.Id == id);
           

            var card2 = db.cards.Where(c => c.Id == id);
            ViewBag.data20 = card2;

            PartContext bb = new PartContext();
            var data14 = bb.parts.Where(d => d.Booking_Id == card.Booking_Id);
            

            ServiceContext nn = new ServiceContext();
            var data15 = nn.services.Where(d => d.Booking_Id == card.Booking_Id);
            

            decimal Autopart = bb.parts.Where(d => d.Booking_Id == card.Booking_Id).Select(d => d.Amount).DefaultIfEmpty(0).Sum();
            decimal Services = nn.services.Where(d => d.Booking_Id == card.Booking_Id).Select(d => d.Amount).DefaultIfEmpty(0).Sum();
            decimal combinedSum = Autopart;
            decimal combinedSum2 = Services;
            ViewBag.Estimate1 = combinedSum + combinedSum2;


            decimal Autopart2 = bb.parts.Where(d => d.Booking_Id == card.Booking_Id).Select(d => d.Total_Amount).DefaultIfEmpty(0).Sum();
            decimal Services2 = nn.services.Where(d => d.Booking_Id == card.Booking_Id).Select(d => d.Total_Amount).DefaultIfEmpty(0).Sum();
            decimal combinedSum222 = Autopart;
            decimal combinedSum2222 = Services;
            ViewBag.Estimate2 = combinedSum2 + combinedSum2222;









            PartContext Pd = new PartContext();
            var data11 = Pd.parts.Where(d => d.Booking_Id == card.Booking_Id);
            ViewBag.Auto = data11;

            ServiceContext Pe = new ServiceContext();
            var data12 = Pe.services.Where(d => d.Booking_Id == card.Booking_Id);
            ViewBag.Service = data12;


            var FF = db.cards.Where(c => c.Id == id).ToList();
            ViewBag.F = FF;

            {
                Card customer = db.cards.FirstOrDefault(c => c.Id == id);

                var report = new PartialViewAsPdf("~/Views/Estimate/Estimate_Print.cshtml", customer);
                return report;
            }
        }










        // GET: JobsCards/Delete/5
        public ActionResult Delete(int? id ,int BookingId)
        {

            CheckInContext dbbb = new CheckInContext();
            var data10 = dbbb.checkIns.Where(d => d.Id == BookingId);
            ViewBag.CheckIn = data10;

            PartContext Pd = new PartContext();
            var data11 = Pd.parts.Where(d => d.Booking_Id == BookingId);
            ViewBag.Auto = data11;

            ServiceContext Pe = new ServiceContext();
            var data12 = Pe.services.Where(d => d.Booking_Id == BookingId);
            ViewBag.Service = data12;


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
        public ActionResult DeleteConfirmed(int id, int BookingId)
        {
            Card card = db.cards.Find(id);
            db.cards.Remove(card);
            try {
                string strcon = ConfigurationManager.ConnectionStrings["GRS"].ConnectionString;
                SqlConnection sqlCon = new SqlConnection(strcon);
                SqlCommand cmd = new SqlCommand("sp_drop_services_autoparts", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@key", BookingId);
                sqlCon.Open();
                cmd.ExecuteNonQuery();
                sqlCon.Close();
                TempData["msg"] = "✔ Estimate Deleted ";
            } catch { }
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
