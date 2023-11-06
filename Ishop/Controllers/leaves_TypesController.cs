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

namespace Ishop.Controllers
{
    public class leaves_TypesController : Controller
    {
        private leaveTypesContext db = new leaveTypesContext();

        // GET: leaves_Types
        public ActionResult Index(string searchBy, string search, int? page)
        {


            if (!(search == null))
            {
                return View(db.leaves_Types.OrderByDescending(p => p.Id).Where(c => c.Type == search).ToList().ToPagedList(page ?? 1, 12));

            }
            else
            {
                return View(db.leaves_Types.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 12));


            }
        }

        // GET: leaves_Types/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            leaves_Types leaves_Types = db.leaves_Types.Find(id);
            if (leaves_Types == null)
            {
                return HttpNotFound();
            }
            return View(leaves_Types);
        }

        // GET: leaves_Types/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: leaves_Types/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedOn,Type,Days")] leaves_Types leaves_Types)
        {
            if (ModelState.IsValid)
            {
                db.leaves_Types.Add(leaves_Types);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(leaves_Types);
        }

        // GET: leaves_Types/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            leaves_Types leaves_Types = db.leaves_Types.Find(id);
            if (leaves_Types == null)
            {
                return HttpNotFound();
            }
            return View(leaves_Types);
        }

        // POST: leaves_Types/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Type,Days")] leaves_Types leaves_Types)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leaves_Types).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leaves_Types);
        }

        // GET: leaves_Types/Delete/5
        public ActionResult Delete(int? id)
        {
             var llll = db.leaves_Types.Find(id);
            db.leaves_Types.Remove(llll);
            db.SaveChanges();

            TempData["msg"] = "Leave type dropped successfully";
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
