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
using System.Configuration;
using System.Data.SqlClient;
using Ishop.Models;

namespace Ishop.Controllers
{
    public class ExpenseController : Controller
    {
        private ExpenseContext db = new ExpenseContext();

        // GET: Expense
        public ActionResult Index(string searchBy, string search, int? page)
        {
            HREntities l = new HREntities();
            var boo = l.sp_expenses().ToList();
            ViewBag.l = boo;



            if (!(search == null))
            {
                return View(db.expenses.OrderByDescending(p => p.Id).Where(c => c.Additional_Notes == search).ToList().ToPagedList(page ?? 1, 6));

            }
            else
            {
                return View(db.expenses.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 6));


            }
        }

        // GET: Expense/Details/5
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

        // GET: Expense/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Expense/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedOn,Amount,Status,Additional_Notes")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.expenses.Add(expense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expense);
        }

        // GET: Expense/Edit/5
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

        // POST: Expense/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Amount,Status,Additional_Notes")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        // GET: Expense/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Expense/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Expense expense = db.expenses.Find(id);
            db.expenses.Remove(expense);
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
        [Authorize(Roles = "Admin")]
        public ActionResult Cancelled(int? id, Expense expense)
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
                SqlConnection sqlCon = new SqlConnection(strcon);
                SqlCommand sqlcmnd = new SqlCommand("sp_markcancelled_expense", sqlCon);
                sqlcmnd.CommandType = CommandType.StoredProcedure;
                sqlcmnd.Parameters.AddWithValue("@Id", expense.Id);
                sqlCon.Open();
                sqlcmnd.ExecuteNonQuery();
                sqlCon.Close();

            }
            catch
            {
                TempData["msg"] = "error occured in cancelling Expense ";
                return RedirectToAction("Index");
            }


            {
                TempData["msg"] = "✔ Expense Cancelled ";
                return RedirectToAction("Index");
            }
        }

    }
}
