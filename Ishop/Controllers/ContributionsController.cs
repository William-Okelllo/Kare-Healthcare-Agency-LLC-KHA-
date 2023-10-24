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
using Rotativa;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.AspNet.SignalR.Messaging;

namespace Ishop.Controllers
{
    [Authorize]

    public class ContributionsController : Controller
    {
        private Contribution_Context db = new Contribution_Context();

        // GET: Contributions
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null) && (!(search == "")))
            {
                return View(db.contributions.OrderByDescending(p => p.Id).Where(c => c.Project_No.ToString() == search ).ToList().ToPagedList(page ?? 1, 11));

            }
            else if (search == "")
            {
                return View(db.contributions.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(db.contributions.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));
            }

        }

        // GET: Contributions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contribution contribution = db.contributions.Find(id);
            if (contribution == null)
            {
                return HttpNotFound();
            }
            return View(contribution);
        }

        // GET: Contributions/Create
        public ActionResult Collect(int id)
        {

            ViewBag.Childid = id;

            Child_Context CC = new Child_Context();

            var Childinfo = CC.children.Where(c => c.Id == id).FirstOrDefault();

            ViewBag.Childinfo = Childinfo;

            return View();
        }

        // POST: Contributions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Collect([Bind(Include = "Id,CreatedOn,ChildId,Paid,Month,Total,Balance,Project_No")] Contribution contribution, string[] Month)
        {
            if (ModelState.IsValid)
            {

                string concatenatedPlots = string.Join(",", Month);
                contribution.Month = concatenatedPlots;


                db.contributions.Add(contribution);

                Child_Context CC = new Child_Context();
                Child check = CC.children.Find(contribution.Id);
                if (check != null)
                {
                    check.Balance = contribution.Balance;
                    CC.SaveChanges();
                }


                db.SaveChanges();

                


                Child_Context XC = new Child_Context();
                var Childinfo = XC.children.Where(c => c.Id == contribution.ChildId).FirstOrDefault();
                ViewBag.Childinfo = Childinfo;




                

                string message =   "Kwa  mzazi wa " + Childinfo.Fullnames
                + "\n" + "Pokea salamu za bwana Yesu  ,Hi ni kukufahamisha ya kwamba tumepokea malipo yako ya mchango wa wazazi ya kila mwezi  "

                + "\n" + " Paid Amount : "+ contribution.Paid.ToString("#,##0.00")
                + "\n" + " Balance : " + contribution.Balance.ToString("#,##0.00")

                + "\n" + "Asante sana ";


                PushSms(Childinfo.PhoneNumber.ToString(), message, DateTime.Now);




                return RedirectToAction("Index");
            }

            return View(contribution);
        }

        // GET: Contributions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contribution contribution = db.contributions.Find(id);
            if (contribution == null)
            {
                return HttpNotFound();
            }
            return View(contribution);
        }

        // POST: Contributions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,ChildId,Paid,Month,Total,Balance")] Contribution contribution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contribution).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contribution);
        }

        // GET: Contributions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contribution contribution = db.contributions.Find(id);
            if (contribution == null)
            {
                return HttpNotFound();
            }
            return View(contribution);
        }

        // POST: Contributions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contribution contribution = db.contributions.Find(id);
            db.contributions.Remove(contribution);
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

        public ActionResult Receipt(int id)
        {

            Contribution customer = db.contributions.FirstOrDefault(c => c.Id == id);
            var report = new PartialViewAsPdf("~/Views/contributions/Receipt.cshtml", customer);

            Contribution_Context CON = new Contribution_Context();
            var Contributioninfo = CON.contributions.Where(c => c.Id == id).FirstOrDefault();
            ViewBag.Contributioninfo = Contributioninfo;


            Child_Context CC = new Child_Context();
            var Childinfo = CC.children.Where(c => c.Id == Contributioninfo.ChildId).FirstOrDefault();
            ViewBag.Childinfo = Childinfo;

            return report;

        }

        private string connectionString = ConfigurationManager.ConnectionStrings["Compassion"].ConnectionString;
        
        public void PushSms(string Recipient, string Subject, DateTime CreatedOn)
        {
            string query = "INSERT INTO Outgoingsms (MessageText,IsSent,CreatedOn,RecipientNumber,Trials,Response) VALUES " +
            "                                          (@MessageText,@IsSent,@CreatedOn,@RecipientNumber,@Trials,@Response)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@MessageText", Subject);
                    command.Parameters.AddWithValue("@IsSent", 0);
                    command.Parameters.AddWithValue("@CreatedOn", CreatedOn);
                    command.Parameters.AddWithValue("@RecipientNumber", Recipient);
                    command.Parameters.AddWithValue("@Trials", 0);
                    command.Parameters.AddWithValue("@Response", "--waiting--");
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
