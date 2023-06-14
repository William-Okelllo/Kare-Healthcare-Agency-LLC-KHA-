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
using System.Web.UI.WebControls.WebParts;
using System.Configuration;
using System.Data.SqlClient;

namespace Ishop.Controllers
{
    public class ServicesController : Controller
    {
        private ServiceContext db = new ServiceContext();

        // GET: Services
        public ActionResult Index()
        {
            return View(db.services.ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Add(int? id)
        {
            CheckInContext dbbb = new CheckInContext();
            var data10 = dbbb.checkIns.Where(d => d.Id == id);
            ViewBag.CheckIn = data10;
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,Booking_Id,Service_Name,Amount,Total_Amount,VAT")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.services.Add(service);
                db.SaveChanges();
                return RedirectToAction("create", "JobsCards", new { id = service.Booking_Id });
            }

            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Booking_Id,Service_Name,Amount")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.services.Find(id);
            db.services.Remove(service);
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
        public ActionResult Remove(int id)
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["GRS"].ConnectionString;
                SqlConnection sqlCon = new SqlConnection(strcon);
                SqlCommand cmd = new SqlCommand("sp_remove_Services", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                sqlCon.Open();
                cmd.ExecuteNonQuery();
                sqlCon.Close();

                TempData["msg"] = "Service removed successfully";


            }

            catch
            {
                return HttpNotFound();
                TempData["msg"] = "error";
            }



            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }
    }
}
