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
    public class ItemsController : Controller
    {
        private Item_context db = new Item_context();

        // GET: Items
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null) && (!(search == "")))
            {
                return View(db.items.OrderByDescending(p => p.Id).Where(c => c.Name.StartsWith(search) || c.Name == search || c.Name.Contains(search)).ToList().ToPagedList(page ?? 1, 11));

            }
            else if (search == "")
            {
                return View(db.items.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(db.items.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));
            }

        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = db.items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            return View(items);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Amount")] Items items)
        {
            if (ModelState.IsValid)
            {
                db.items.Add(items);
                db.SaveChanges();
                
            }

            TempData["msg"] = "Item Added successfully ";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }




        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = db.items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            return PartialView(items);
        }

        // POST: Grants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Amount")] Items items)
        {
            if (ModelState.IsValid)
            {
                db.Entry(items).State = EntityState.Modified;
                db.SaveChanges();

            }
            TempData["msg"] = "Item updated successfully ";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }













        public ActionResult Delete(int? id)
        {
           
            Items items = db.items.Find(id);
            db.items.Remove(items);
            db.SaveChanges();
            TempData["msg"] = "Data deleted successfully ";
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
