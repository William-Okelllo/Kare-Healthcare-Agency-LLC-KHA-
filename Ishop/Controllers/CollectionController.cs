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
using System.Text.RegularExpressions;

namespace Ishop
{
    public class CollectionController : Controller
    {
        private Collection_context db = new Collection_context();

        
        public ActionResult Collect(int id, int Projectid)
        {
            Project_Context PC = new Project_Context();
            var project = PC.projects.Where(c => c.Id == Projectid).FirstOrDefault();

            Phase_Context PhC = new Phase_Context();
            var Phase = PhC.phases.Where(c => c.Id == id).FirstOrDefault();

            ViewBag.project = project;
            ViewBag.Phase = Phase;


            var Collected = db.collections.OrderByDescending(p => p.CreatedOn).Where(c => c.Phase_Id == id).ToList();
            ViewBag.Collected = Collected;
            var CollectedTotal = db.collections.Where(c => c.Phase_Id == id).Select(c => c.Charge).DefaultIfEmpty(0).Sum(); ;
            ViewBag.CollectedTotal = CollectedTotal;

            var Balance = Phase.Budget - CollectedTotal;
            ViewBag.Balance = Balance;

            return View();
        }

        // POST: Collection/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Collect([Bind(Include = "Id,Phase_Id,Project_Id,CreatedOn,Charge")] Collection collection)
        {
            if (ModelState.IsValid)
            {
                db.collections.Add(collection);
                db.SaveChanges();
                CaptureRecord(collection.Phase_Id, collection.Charge);
                TempData["msg"] = "Collection captured successfully ";
                string returnUrl = Request.UrlReferrer.ToString();
                return Redirect(returnUrl);
            }

            return View(collection);
        }


        public void CaptureRecord(int Phase_Id, decimal Collected)
        {
            using (Phase_Context PhC = new Phase_Context())
            {
                var PhaseToUpdate = PhC.phases.Find(Phase_Id);

                if (PhaseToUpdate != null)
                {
                    // Check if Collected is null and handle accordingly
                    if (PhaseToUpdate.Collected == null)
                    {
                        PhaseToUpdate.Collected = Collected;
                    }
                    else
                    {
                        PhaseToUpdate.Collected += Collected;
                    }

                    PhC.SaveChanges();
                }
            }
        }





        // GET: Collection/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        // POST: Collection/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Phase_Id,Project_Id,CreatedOn,Charge")] Collection collection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(collection);
        }

        // GET: Collection/Delete/5
        public ActionResult Delete(int? id)
        {
            
            Collection collection = db.collections.Find(id);
            db.collections.Remove(collection);
            db.SaveChanges();
            DropCollection(collection.Phase_Id, collection.Charge);
            TempData["msg"] = "Collection dropped successfully ";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }
        public void DropCollection(int Phase_Id, decimal Collected)
        {
            using (Phase_Context PhC = new Phase_Context())
            {
                var PhaseToUpdate = PhC.phases.Find(Phase_Id);

                if (PhaseToUpdate != null)
                {
                   
                    
                        PhaseToUpdate.Collected -= Collected;
                    

                    PhC.SaveChanges();
                }
            }
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
