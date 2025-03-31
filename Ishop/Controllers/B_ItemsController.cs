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

namespace Ishop.Controllers
{
    public class B_ItemsController : Controller
    {
        private benitem_context db = new benitem_context();
        private readonly Item_context AA = new Item_context();
        // GET: B_Items
        public ActionResult Index()
        {
            return View(db.benitems.ToList());
        }

        // GET: B_Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Benitem benitem = db.benitems.Find(id);
            if (benitem == null)
            {
                return HttpNotFound();
            }
            return View(benitem);
        }


        private Item_context dbb = new Item_context();
        public ActionResult GetPhaseId(string Name)
        {
            var data = dbb.items.FirstOrDefault(d => d.Name == Name);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

       

        // GET: B_Items/Create
        public ActionResult Create(int Tag)
        {
            ViewBag.Tag = Tag;
            var Admm = AA.items.ToList();
            ViewBag.Admm = new SelectList(Admm, "Name", "Name");
            return View();
        }

        // POST: B_Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Beneficiary_Id,Name,Quantity,Total,Each")] Benitem benitem)
        {
            if (ModelState.IsValid)
            {
                db.benitems.Add(benitem);
                db.SaveChanges();
            }
          
            TempData["msg"] = "Item added successfully ";
            return RedirectToAction("Profile", "Admission", new { Id = benitem.Beneficiary_Id });
        }

        public ActionResult Edit(int? id)
        {
            var Admm = AA.items.ToList();
            ViewBag.Admm = new SelectList(Admm, "Name", "Name");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Benitem benitem = db.benitems.Find(id);
            if (benitem == null)
            {
                return HttpNotFound();
            }
            return View(benitem);
        }

        // POST: Waitlist/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Beneficiary_Id,Name,Quantity,Total,Each")] Benitem benitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(benitem).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Waitlist updated successfully ";
            }
            return RedirectToAction("Profile", "Admission", new { Id = benitem.Beneficiary_Id });
        }



        // GET: B_Items/Edit/5

        // GET: B_Items/Delete/5
        public ActionResult Delete(int? id)
        {
           
            Benitem benitem = db.benitems.Find(id);
            db.benitems.Remove(benitem);
            db.SaveChanges();
            TempData["msg"] = "Item Deleted successfully ";
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
