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
using Ishop.Models;
using System.Configuration;
using System.Web.Configuration;

namespace Ishop.Controllers
{
    public class GarageConfigController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: GarageConfig
        public ActionResult Index()
        {
            return View(db.garages.ToList());
        }

        // GET: GarageConfig/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Garage garage = db.garages.Find(id);
            if (garage == null)
            {
                return HttpNotFound();
            }
            return View(garage);
        }

        // GET: GarageConfig/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GarageConfig/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,location,Phone,Email,VAT,Mark_Up")] Garage garage)
        {
            if (ModelState.IsValid)
            {
                db.garages.Add(garage);
                Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
                //Modifying the AppKey from AppValue to AppValue1
                webConfigApp.AppSettings.Settings["Name"].Value = garage.Name;
                webConfigApp.AppSettings.Settings["location"].Value = garage.location;
                webConfigApp.AppSettings.Settings["Phone"].Value = garage.Phone;
                webConfigApp.AppSettings.Settings["Email"].Value = garage.Email;
                webConfigApp.AppSettings.Settings["VAT"].Value = garage.VAT.ToString();
                webConfigApp.AppSettings.Settings["MarkUp"].Value = garage.Mark_Up.ToString();

                //Save the Modified settings of AppSettings.
                webConfigApp.Save();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(garage);
        }

        // GET: GarageConfig/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Garage garage = db.garages.Find(id);
            if (garage == null)
            {
                return HttpNotFound();
            }
            return View(garage);
        }

        // POST: GarageConfig/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,location,Phone,Email,VAT,Mark_Up")] Garage garage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(garage).State = EntityState.Modified;
                Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
                //Modifying the AppKey from AppValue to AppValue1
                webConfigApp.AppSettings.Settings["Name"].Value = garage.Name;
                webConfigApp.AppSettings.Settings["location"].Value = garage.location;
                webConfigApp.AppSettings.Settings["Phone"].Value = garage.Phone;
                webConfigApp.AppSettings.Settings["Email"].Value = garage.Email;
                webConfigApp.AppSettings.Settings["VAT"].Value = garage.VAT.ToString();
                webConfigApp.AppSettings.Settings["MarkUp"].Value = garage.Mark_Up.ToString();

                //Save the Modified settings of AppSettings.
                webConfigApp.Save();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(garage);
        }

        // GET: GarageConfig/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Garage garage = db.garages.Find(id);
            if (garage == null)
            {
                return HttpNotFound();
            }
            return View(garage);
        }

        // POST: GarageConfig/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Garage garage = db.garages.Find(id);
            db.garages.Remove(garage);
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
