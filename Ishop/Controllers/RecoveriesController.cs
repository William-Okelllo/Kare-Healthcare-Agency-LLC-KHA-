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
using System.Configuration;
using System.Data.SqlClient;

namespace Ishop.Controllers
{
    public class RecoveriesController : Controller
    {
        private Recovery_context db = new Recovery_context();

        // GET: Recoveries
        public ActionResult Index(string searchBy, string search, int? page)
        {

            if (this.User.IsInRole("Tickets_Approval"))
            {

                if (!(search == null) && (!(search == "")))
                {
                    return View(db.recoveries.OrderByDescending(p => p.id).Where(c => c.Inv_No == search ).ToList().ToPagedList(page ?? 1, 6));

                }

                else if (search == " ")
                {
                    return View(db.recoveries.OrderByDescending(p => p.id).ToList().ToPagedList(page ?? 1, 6));

                }
                else
                {
                    return View(db.recoveries.OrderByDescending(p => p.id).ToList().ToPagedList(page ?? 1, 6));

                }

            }
            else
            {
                if (!(search == null))
                {
                    return View(db.recoveries.OrderByDescending(p => p.id).Where(c => (c.Staff == User.Identity.Name)).ToList().ToPagedList(page ?? 1, 6));

                }
                else if (search == null)
                {
                    return View(db.recoveries.OrderByDescending(p => p.id).Where(c => ( c.Staff == User.Identity.Name)).ToList().ToPagedList(page ?? 1, 6));


                }
                else
                {
                    return View(db.recoveries.OrderByDescending(p => p.id).Where(c => (c.Staff == User.Identity.Name)).ToList()
                        .ToPagedList(page ?? 1, 6));
                }
            }
        
    }

        // GET: Recoveries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recovery recovery = db.recoveries.Find(id);
            if (recovery == null)
            {
                return HttpNotFound();
            }
            return View(recovery);
        }

        // GET: Recoveries/Create
        public ActionResult Create( int? id)
        {
            if (id == null) { id = 0; }
            
            sp_GetRR l2 = new sp_GetRR();
            var boo2 = l2.sp_GetRecovery(id).ToList();
            ViewBag.l2 = boo2;


            return View();
        }

       

        // POST: Recoveries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,CreatedOn,Staff,Ticket_No,Ticket_id,Inv_No,Amount_Recovered,Inv_Amount,Balance")] Recovery recovery)
        {
            if (recovery.Ticket_No == null)
            {
                TempData["msg"] = "Error ,recover the needed amount from a ticket";
                return RedirectToAction("Create");
            }


            if (ModelState.IsValid)
            {
                db.recoveries.Add(recovery);
                db.SaveChanges();
                try
                {
                    string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
                    SqlConnection sqlCon = new SqlConnection(strcon);
                    SqlCommand sqlcmnd = new SqlCommand("sp_ticket_recovery", sqlCon);
                    sqlcmnd.CommandType = CommandType.StoredProcedure;
                    sqlcmnd.Parameters.AddWithValue("@Id",recovery.Ticket_id);
                    sqlcmnd.Parameters.AddWithValue("@Balance", recovery.Balance);
                    
                    sqlCon.Open();
                    sqlcmnd.ExecuteNonQuery();
                    sqlCon.Close();

                }
                catch
                {


                }
                TempData["msg"] = recovery.Amount_Recovered +" recovered successfully on ticket";
                return RedirectToAction("Index");
            }

            return View(recovery);
        }

        // GET: Recoveries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recovery recovery = db.recoveries.Find(id);
            if (recovery == null)
            {
                return HttpNotFound();
            }
            return View(recovery);
        }

        // POST: Recoveries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,CreatedOn,Staff,Amount_Recovered,Inv_No,Inv_Amount,Balance")] Recovery recovery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recovery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recovery);
        }

        // GET: Recoveries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recovery recovery = db.recoveries.Find(id);
            if (recovery == null)
            {
                return HttpNotFound();
            }
            return View(recovery);
        }

        // POST: Recoveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recovery recovery = db.recoveries.Find(id);
            db.recoveries.Remove(recovery);
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
    }
}
