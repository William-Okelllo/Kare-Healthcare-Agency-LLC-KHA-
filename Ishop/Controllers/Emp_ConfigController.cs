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
    public class Emp_ConfigController : Controller
    {
        private leaves_emp_config_table db = new leaves_emp_config_table();

        // GET: Emp_Config
        public ActionResult Index()
        {
            return View(db.Employees_Config.ToList());
        }

        // GET: Emp_Config/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees_Config employees_Config = db.Employees_Config.Find(id);
            if (employees_Config == null)
            {
                return HttpNotFound();
            }
            return View(employees_Config);
        }

        // GET: Emp_Config/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Emp_Config/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Total_leaves_per_year,Leave_pre_days,Advance_pay_limit")] Employees_Config employees_Config)
        {
            if (ModelState.IsValid)
            {
                db.Employees_Config.Add(employees_Config);

                Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
                //Modifying the AppKey from AppValue to AppValue1
                webConfigApp.AppSettings.Settings["Total_leaves_per_year"].Value = employees_Config.Total_leaves_per_year.ToString(); ;
                webConfigApp.AppSettings.Settings["Leave_pre_days"].Value = employees_Config.Leave_pre_days.ToString(); ;
                webConfigApp.AppSettings.Settings["Advance_pay_limit"].Value = employees_Config.Advance_pay_limit.ToString(); ;
                webConfigApp.Save();



                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employees_Config);
        }

        // GET: Emp_Config/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees_Config employees_Config = db.Employees_Config.Find(id);
            if (employees_Config == null)
            {
                return HttpNotFound();
            }
            return View(employees_Config);
        }

        // POST: Emp_Config/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Total_leaves_per_year,Leave_pre_days,Advance_pay_limit")] Employees_Config employees_Config)
        {
            if (ModelState.IsValid)
            {
                Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
                //Modifying the AppKey from AppValue to AppValue1
                webConfigApp.AppSettings.Settings["Total_leaves_per_year"].Value = employees_Config.Total_leaves_per_year.ToString(); ;
                webConfigApp.AppSettings.Settings["Leave_pre_days"].Value = employees_Config.Leave_pre_days.ToString(); ;
                webConfigApp.AppSettings.Settings["Advance_pay_limit"].Value = employees_Config.Advance_pay_limit.ToString(); ;
                webConfigApp.Save();
                db.Entry(employees_Config).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employees_Config);
        }

        // GET: Emp_Config/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employees_Config employees_Config = db.Employees_Config.Find(id);
            if (employees_Config == null)
            {
                return HttpNotFound();
            }
            return View(employees_Config);
        }

        // POST: Emp_Config/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employees_Config employees_Config = db.Employees_Config.Find(id);
            db.Employees_Config.Remove(employees_Config);
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
