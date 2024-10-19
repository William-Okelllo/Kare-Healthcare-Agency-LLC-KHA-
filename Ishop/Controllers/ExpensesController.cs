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
using IShop.Core.Interface;

namespace Ishop.Controllers
{
    public class ExpensesController : Controller
    {
        private Expense_context db = new Expense_context();
        private Direct_cost_context dbb = new Direct_cost_context();
        private Indirect_context dbbb = new Indirect_context();

        // GET: Expenses
        public ActionResult Index(string searchBy, string search, int? page)
        {
            ViewBag.Expenses = (db.expenses.Select(c => c.Amount).DefaultIfEmpty(0).Sum());

            var Direct_cost=dbb.direct_Costs.Select(c => c.Total).DefaultIfEmpty(0).Sum();
            var inDirect = dbbb.indirects.Select(c => c.Amount).DefaultIfEmpty(0).Sum();

            ViewBag.Budget=Direct_cost + inDirect;

            ViewBag.Balance = ((Direct_cost + inDirect) - ((db.expenses.Select(c => c.Amount).DefaultIfEmpty(0).Sum())));
                

            if (!(search == null))
            {
                return View(db.expenses.OrderByDescending(p => p.Id).Where(c => c.Title == search).ToList().ToPagedList(page ?? 1, 6));

            }
            else
            {
                return View(db.expenses.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 6));


            }
        }


        // GET: Expenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // GET: Expenses/Create
        public ActionResult Add()
        {
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,AddedOn,Track,Description,Amount,Title")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.expenses.Add(expense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expense);
        }

        // GET: Expenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expense expense = db.expenses.Find(id);
            if (expense == null)
            {
                return HttpNotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AddedOn,Track,Description,Amount,Title")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Expense updated successfully ";
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public ActionResult Delete(int? id)
        {
           
            Expense expense = db.expenses.Find(id);
            db.expenses.Remove(expense);
            db.SaveChanges();
            TempData["msg"] = "Expense dropped successfully ";
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
