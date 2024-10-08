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
using Ishop.Models;

namespace Ishop.Controllers
{
    public class GrantsController : Controller
    {
        private Grant_context db = new Grant_context();

        // GET: Grants
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null) && (!(search == "")))
            {
                return View(db.grants.OrderByDescending(p => p.Id).Where(c => c.Name.StartsWith(search) || c.Name == search || c.Name.Contains(search)).ToList().ToPagedList(page ?? 1, 11));

            }
            else if (search == "")
            {
                return View(db.grants.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(db.grants.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));
            }

        }

        // GET: Grants/Details/5
        public ActionResult Details(int? id)
        {
            Exp_context EE = new Exp_context();
            var Exx = EE.exps.Where(c => c.Budget_Id == id).ToList();
            ViewBag.Exps = Exx;






            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grant grant = db.grants.Find(id);
            if (grant == null)
            {
                return HttpNotFound();
            }
            return View(grant);
        }

        // GET: Grants/Create
        public ActionResult Create()
        {
            Userstable AA = new Userstable();
            var Admm = AA.AspNetUsers.ToList();
            ViewBag.Admm = new SelectList(Admm, "username", "username");


            return PartialView("Create");
        }

        // POST: Grants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,From_Date,To_Date,Total,Name,Type,Track,Status,AddedOn,Submitted_by")] Grant grant)
        {
            if (ModelState.IsValid)
            {
                db.grants.Add(grant);
                db.SaveChanges();
                TempData["msg"] = "Budget added successfully ";
                return RedirectToAction("Index");
            }

            return View(grant);
        }

        // GET: Grants/Edit/5
        public ActionResult Edit(int? id)
        {
            Userstable AA = new Userstable();
            var Admm = AA.AspNetUsers.ToList();
            ViewBag.Admm = new SelectList(Admm, "username", "username");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grant grant = db.grants.Find(id);
            if (grant == null)
            {
                return HttpNotFound();
            }
            return PartialView(grant);
        }

        // POST: Grants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,From_Date,To_Date,Total,Name,Type,Track,Status,AddedOn,Submitted_by")] Grant grant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grant).State = EntityState.Modified;
                db.SaveChanges();

            }
            TempData["msg"] = "Budget updated successfully ";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }

            // GET: Grants/Delete/5
            public ActionResult Delete(int? id)
        {
            
            Grant grant = db.grants.Find(id);
            db.grants.Remove(grant);
            db.SaveChanges();
            TempData["msg"] = "Budget deleted successfully ";
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
