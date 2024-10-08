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
using static System.Data.Entity.Infrastructure.Design.Executor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Ishop.Controllers
{
    public class ExpsController : Controller
    {
        private Grant_context dbb = new Grant_context();
        private Exp_context db = new Exp_context();

        // GET: Exps
        public ActionResult Index()
        {
            return View(db.exps.ToList());
        }

        // GET: Exps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exp exp = db.exps.Find(id);
            if (exp == null)
            {
                return HttpNotFound();
            }
            return View(exp);
        }

        // GET: Exps/Create
        public ActionResult Create(int Tag)
        {
            ViewBag.Tag = Tag;
            return PartialView("Create");
        }

        // POST: Exps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type,Budget,Expenditures,Shared,Available,Budget_Id")] Exp exp)
        {
            if (ModelState.IsValid)
            {
                db.exps.Add(exp);
                db.SaveChanges();

                Update(exp.Budget_Id);


                TempData["msg"] = "Budget item added successfully ";
                string returnUrl = Request.UrlReferrer.ToString();
                return Redirect(returnUrl);
            }

            return View(exp);
        }




        public void Update(int Id)
        {
            Grant_context BCC = new Grant_context();
            var Benn = BCC.grants.Find(Id);

            var Itemsum=db.exps.Where(c=>c.Budget_Id==Id).Select(c=>c.Budget).DefaultIfEmpty(0).Sum();

            if (Benn != null)
            {
                Benn.Total = Itemsum;
            }
            BCC.SaveChanges();

        }
        








        // GET: Exps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exp exp = db.exps.Find(id);
            if (exp == null)
            {
                return HttpNotFound();
            }
            return PartialView(exp);

        }

        // POST: Exps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,Budget,Expenditures,Shared,Available,Budget_Id")] Exp exp)
        {
           
                db.Entry(exp).State = EntityState.Modified;
                db.SaveChanges();
                Update(exp.Budget_Id);
                
            
            TempData["msg"] = "Budget item update successfully ";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }

        // GET: Exps/Delete/5
        public ActionResult Delete(int? id)
        {

            Exp exp = db.exps.Find(id);
            db.exps.Remove(exp);
            db.SaveChanges();
            Update(exp.Budget_Id);
            TempData["msg"] = "Budget item deleted successfully ";
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
