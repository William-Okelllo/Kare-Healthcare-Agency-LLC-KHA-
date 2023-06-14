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
using System.Configuration;
using System.Data.SqlClient;

namespace Ishop.Controllers
{
    public class AutopartsController : Controller
    {
        private PartContext db = new PartContext();

        // GET: Autoparts
        public ActionResult Index()
        {
            return View(db.parts.ToList());
        }

        // GET: Autoparts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part part = db.parts.Find(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            return View(part);
        }

        // GET: Autoparts/Create
        public ActionResult Add(int? id)
        {
            CheckInContext dbbb = new CheckInContext();
            var data10 = dbbb.checkIns.Where(d => d.Id == id);
            ViewBag.CheckIn = data10;
            return View();
        }

        // POST: Autoparts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,Booking_Id,Part_Name,Condition,Amount,Total_Amount,VAT")] Part part)
        {
            if (ModelState.IsValid)
            {
                db.parts.Add(part);
                db.SaveChanges();
                return RedirectToAction("create", "JobsCards", new { id = part.Booking_Id });
            }

            return View(part);
        }

        // GET: Autoparts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part part = db.parts.Find(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            return View(part);
        }

        // POST: Autoparts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Booking_Id,Part_Name,Condition,Amount")] Part part)
        {
            if (ModelState.IsValid)
            {
                db.Entry(part).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(part);
        }

        // GET: Autoparts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part part = db.parts.Find(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            return View(part);
        }

        // POST: Autoparts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Part part = db.parts.Find(id);
            db.parts.Remove(part);
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
                SqlCommand cmd = new SqlCommand("sp_remove_autopart", sqlCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);

                sqlCon.Open();
                cmd.ExecuteNonQuery();
                sqlCon.Close();

                TempData["msg"] = "Autopart removed successfully";


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
