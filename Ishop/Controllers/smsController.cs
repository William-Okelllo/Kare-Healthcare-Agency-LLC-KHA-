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
    public class smsController : Controller
    {
        private sms_conf db = new sms_conf();

        public ActionResult Index()
        {
            return View(db.sms_configs.ToList());
        }

        // GET: sms_configs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sms_configs sms_configs = db.sms_configs.Find(id);
            if (sms_configs == null)
            {
                return HttpNotFound();
            }
            return View(sms_configs);
        }

        // GET: sms_configs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: sms_configs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,APIkey,partnerID,shortcode,APIUrl")] sms_configs sms_configs)
        {
            if (ModelState.IsValid)
            {
                db.sms_configs.Add(sms_configs);
                Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
                //Modifying the AppKey from AppValue to AppValue1
                webConfigApp.AppSettings.Settings["APIkey"].Value = sms_configs.APIkey;
                webConfigApp.AppSettings.Settings["partnerID"].Value = sms_configs.partnerID;
                webConfigApp.AppSettings.Settings["shortcode"].Value = sms_configs.shortcode;
                webConfigApp.AppSettings.Settings["APIUrl"].Value = sms_configs.APIUrl;


                //Save the Modified settings of AppSettings.
                webConfigApp.Save();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sms_configs);
        }

        // GET: sms_configs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sms_configs sms_configs = db.sms_configs.Find(id);
            if (sms_configs == null)
            {
                return HttpNotFound();
            }
            return View(sms_configs);
        }

        // POST: sms_configs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,APIkey,partnerID,shortcode,APIUrl")] sms_configs sms_configs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sms_configs).State = EntityState.Modified;
                Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
                //Modifying the AppKey from AppValue to AppValue1
                webConfigApp.AppSettings.Settings["APIkey"].Value = sms_configs.APIkey;
                webConfigApp.AppSettings.Settings["partnerID"].Value = sms_configs.partnerID;
                webConfigApp.AppSettings.Settings["shortcode"].Value = sms_configs.shortcode;
                webConfigApp.AppSettings.Settings["APIUrl"].Value = sms_configs.APIUrl;


                //Save the Modified settings of AppSettings.
                webConfigApp.Save();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sms_configs);
        }

        // GET: sms_configs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sms_configs sms_configs = db.sms_configs.Find(id);
            if (sms_configs == null)
            {
                return HttpNotFound();
            }
            return View(sms_configs);
        }

        // POST: sms_configs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sms_configs sms_configs = db.sms_configs.Find(id);
            db.sms_configs.Remove(sms_configs);
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

