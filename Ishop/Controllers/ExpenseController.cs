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
    [Authorize]
    public class ExpenseController : Controller
    {
        private ExpenseContext db = new ExpenseContext();

        // GET: Expense
        public ActionResult Index(string searchBy, string search, int? page)
        {
            sp_allprocs l9 = new sp_allprocs();
            var boo9 = l9.sp_dash(User.Identity.Name).ToList();
            ViewBag.l9 = boo9;

            if (!(search == null))
            {
                return View(db.expenses.OrderByDescending(p => p.Id).Where(c => c.Item.StartsWith(search) || c.Item == search && c.staff==User.Identity.Name).ToList().ToPagedList(page ?? 1, 8));

            }
            else
            {
                return View(db.expenses.OrderByDescending(p => p.Id).Where(c=> c.staff == User.Identity.Name).ToList().ToPagedList(page ?? 1, 8));


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
            return PartialView("Details", expense);
        }

        // GET: Expense/Create
        public ActionResult Create()
        {
            HR2_CashmateEntities dbb = new HR2_CashmateEntities();
            var categories = dbb.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "item", "item");
            return View();
        }

        // POST: Expense/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedOn,Amount,Fuliza,Transaction_cost,Mode,item,Additional_Notes,Total,staff,Item2")] Expense expense)
        {
           

            if (expense.Item == "Other") 
            { expense.Item = expense.Item2;
                try
                {
                    string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
                    SqlConnection sqlCon = new SqlConnection(strcon);
                    SqlCommand sqlcmnd = new SqlCommand("sp_add_item", sqlCon);
                    sqlcmnd.CommandType = CommandType.StoredProcedure;
                    sqlcmnd.Parameters.AddWithValue("@item", expense.Item2);
                    sqlCon.Open();
                    sqlcmnd.ExecuteNonQuery();
                    sqlCon.Close();

                }
                catch
                {
                    TempData["msg"] = "Error adding item ";

                }
            }



            if (ModelState.IsValid)
            {
                db.expenses.Add(expense);
                db.SaveChanges();
                TempData["msg"] = "Expense posted successfully ";
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
                TempData["msg"] = "Expense records updated successfully ";
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

        public ActionResult Drop(int? id, Ticket ticket)
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
                SqlConnection sqlCon = new SqlConnection(strcon);
                SqlCommand sqlcmnd = new SqlCommand("sp_drop_expense", sqlCon);
                sqlcmnd.CommandType = CommandType.StoredProcedure;
                sqlcmnd.Parameters.AddWithValue("@Id", ticket.id);
                sqlCon.Open();
                sqlcmnd.ExecuteNonQuery();
                sqlCon.Close();
                TempData["msg"] = "Expense deleted successfully ";
                return RedirectToAction("Index");


            }
            catch
            {
                TempData["msg"] = "error occured in  on deleting Expense ";
                return RedirectToAction("Index");
            }

        }
    }
}
