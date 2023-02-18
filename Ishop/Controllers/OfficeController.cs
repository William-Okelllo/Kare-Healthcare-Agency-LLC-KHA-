using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Ishop.Models;

namespace Ishop.Controllers
{
    public class OfficeController : Controller
    {
        private Officetable_ db = new Officetable_();

        // GET: Office
        public ActionResult Index()
        {
            return View(db.Offices.ToList());
        }

        // GET: Office/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Office office = db.Offices.Find(id);
            if (office == null)
            {
                return HttpNotFound();
            }
            return View(office);
        }

        // GET: Office/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Office/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,latitude,longitude,Office_location,Radius")] Office office)
        {
            if(office.latitude ==null || office.longitude ==null || office.Radius ==null)
            {
                TempData["msg"] = "Kindly the missing fields.";
                return RedirectToAction("Create");
            }

        

            if (ModelState.IsValid)
            {
                

                db.Offices.Add(office);
                Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
                //Modifying the AppKey from AppValue to AppValue1
                webConfigApp.AppSettings.Settings["Office_location"].Value = office.Office_location;
                webConfigApp.AppSettings.Settings["latitude"].Value = office.latitude;
                webConfigApp.AppSettings.Settings["longitude"].Value = office.longitude;
                webConfigApp.AppSettings.Settings["Distance_Radius"].Value = office.Radius.ToString() ;
                webConfigApp.Save();
                db.SaveChanges();
                TempData["msg"] = "Office cordinates saved successfully";
                return RedirectToAction("Index");
            }

            return View(office);
        }

        // GET: Office/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Office office = db.Offices.Find(id);
            if (office == null)
            {
                return HttpNotFound();
            }
            return View(office);
        }

        // POST: Office/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,latitude,longitude,Office_location,Radius")] Office office)
        {
            if (ModelState.IsValid)
            {
                db.Entry(office).State = EntityState.Modified;
                Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
                //Modifying the AppKey from AppValue to AppValue1
                webConfigApp.AppSettings.Settings["Office_location"].Value = office.Office_location;
                webConfigApp.AppSettings.Settings["latitude"].Value = office.latitude;
                webConfigApp.AppSettings.Settings["longitude"].Value = office.longitude;
                webConfigApp.AppSettings.Settings["Distance_Radius"].Value = office.Radius.ToString();
                webConfigApp.Save();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(office);
        }

        // GET: Office/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Office office = db.Offices.Find(id);
            if (office == null)
            {
                return HttpNotFound();
            }
            return View(office);
        }

        // POST: Office/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Office office = db.Offices.Find(id);
            db.Offices.Remove(office);
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
