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

namespace Ishop.Controllers
{
    [Authorize]
    public class leave_BalanceController : Controller
    {
        private leaves_Days_trackContext db = new leaves_Days_trackContext();
        private Employee_Context dbb = new Employee_Context();
        // GET: leave_Balance
        public ActionResult Index(string search, int? page)
        {
            if (!(search == null) && (!(search == "")))
            {
                return View(dbb.employees.OrderByDescending(p => p.Id).Where(c => c.Username.StartsWith(search) || c.Username == search || c.Fullname.Contains(search)).ToList().ToPagedList(page ?? 1, 15));

            }
            else if (search == "")
            {
                return View(dbb.employees.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(dbb.employees.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));
            }
        }



        public ActionResult Acc_View(string Username )
        {
            var Employee = dbb.employees.Where(c => c.Username == Username).FirstOrDefault();
            ViewBag.Emp= Employee;
            var Account = db.Leaves_Days_Tracks.Where(c => c.Username == Username).ToList();
            ViewBag.Account = Account;
            return View();
        }

        public ActionResult Insert_days(string Username)
        {
            leaveTypesContext LVV = new leaveTypesContext();
            var Account = db.Leaves_Days_Tracks.Where(c => c.Username == Username).ToList();
            var Parts = LVV.leaves_Types.ToList();
            Random random = new Random();
            using (var In_parts = new leaves_Days_trackContext())
            {
                foreach (var part in Parts)
                {
                    leaves_Days_track inspections_Parts = new leaves_Days_track
                    {
                        Id = random.Next(),
                        Requested_leaves=0,
                        Remaining_leaves= part.Days,
                        Username= Username,
                        Type= part.Type,
                        Total_leaves_per_year = part.Days
                    };

                    In_parts.Leaves_Days_Tracks.Add(inspections_Parts);
                }

                In_parts.SaveChanges();
            }
            TempData["msg"] = "Leave Balance fetched  successfully ";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
        }








        // GET: leave_Balance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: leave_Balance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Total_leaves_per_year,Requested_leaves,Type,Remaining_leaves,Username")] leaves_Days_track leaves_Days_track)
        {
            if (ModelState.IsValid)
            {
                db.Leaves_Days_Tracks.Add(leaves_Days_track);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(leaves_Days_track);
        }

        // GET: leave_Balance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            leaves_Days_track leaves_Days_track = db.Leaves_Days_Tracks.Find(id);
            if (leaves_Days_track == null)
            {
                return HttpNotFound();
            }
            return View(leaves_Days_track);
        }

        // POST: leave_Balance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Total_leaves_per_year,Requested_leaves,Type,Remaining_leaves,Username")] leaves_Days_track leaves_Days_track)
        {
            if (ModelState.IsValid)
            {
                db.Entry(leaves_Days_track).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(leaves_Days_track);
        }

        // GET: leave_Balance/Delete/5
        public ActionResult Delete(int? id)
        {
            leaves_Days_track leaves_Days_track = db.Leaves_Days_Tracks.Find(id);
            db.Leaves_Days_Tracks.Remove(leaves_Days_track);
            db.SaveChanges();
            TempData["msg"] = "leave dropped successfully ";
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
