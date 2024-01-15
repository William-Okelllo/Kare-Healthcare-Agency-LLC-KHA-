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

namespace Ishop.Controllers
{
    public class HODsController : Controller
    {
        private HodContext db = new HodContext();

        // GET: HODs
        public ActionResult Index()
        {
            return View(db.hODs.ToList());
        }

        // GET: HODs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOD hOD = db.hODs.Find(id);
            if (hOD == null)
            {
                return HttpNotFound();
            }
            return View(hOD);
        }

        // GET: HODs/Create
        public ActionResult Add(string Name, int Id)
        {
            ViewBag.Departmentname = Name;
            ViewBag.DepartId = Id;


            Userstable db = new Userstable();
            var boo = db.AspNetUsers.ToList();
            ViewBag.l = boo;
            return View();
        }

        // POST: HODs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,Department_Id,DprtName,Staff")] HOD hOD)
        {
            if (ModelState.IsValid)
            {
                db.hODs.Add(hOD);
                db.SaveChanges();
                TempData["msg"] = hOD.Staff +  " added successfully  as HOD";
                string returnUrl = Request.UrlReferrer.ToString();
                return RedirectToAction("Details", "Departments", new { id = hOD.Department_Id });
            }

            return View(hOD);
        }

        // GET: HODs/Edit/5
       
        public ActionResult Delete(int? id)
        
        {
            HOD hOD = db.hODs.Find(id);
            db.hODs.Remove(hOD);
            db.SaveChanges();
            TempData["msg"] = "HOD member dropped successfully ";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
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
