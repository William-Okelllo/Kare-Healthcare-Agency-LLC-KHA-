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

   
    public class NotificationsController : Controller
    {
        private Notification_context db = new Notification_context();

        // GET: Notifications

        [Authorize]
        public ActionResult Index(string searchBy, string search, int? page)
        {

            return View(db.notifications.OrderByDescending(p => p.Createdon).ToList().ToPagedList(page ?? 1, 11));

        }
        // GET: Notifications/Details/5

        // GET: Notifications/Create
        public ActionResult Add()
        {
            return View();
        }


        public JsonResult GetRecepients(string GroupName)
        {
           if(GroupName =="1")
            {
                Employee_Context EC = new Employee_Context();
                var Recepients = EC.employees.Where(c => c.Active == true).Select(c => c.Email).ToList();
                string data = string.Join(",", Recepients);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else if (GroupName == "2")
            {
                Employee_Context EC = new Employee_Context();
                HodContext HC = new HodContext();
                var Hods = HC.hODs.Select(c => c.Staff).Distinct().ToList();

                var Recepients = EC.employees.Where(employee => Hods.Any(hodUsername => employee.Username.Contains(hodUsername))).Select(employee => employee.Email).ToList();
                string data = string.Join(",", Recepients);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
           else
            {
                string data = "No email records found";
                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }





        //THis sents out notifications 
        public void Recurring()
        {

            var currentDate = DateTime.Now.Date;

            var Data = db.notifications
                .Where(c => c.Active == true && DbFunctions.TruncateTime(c.Last_sent) != currentDate) .ToList();

            Random random = new Random();
            using (var In_parts = new OutgoingEmailsContext())
            {
                foreach (var part in Data)
                {
                    OutgoingEmails fetcheddata = new OutgoingEmails
                    {
                        Id = random.Next(),
                        Recipient= part.Recepients,
                        Subject= part.Subject,
                        Body= part.Message,
                        Status=false,
                        CreatedOn=DateTime.Now,
                        Files=null,
                        Trials=0,
                        Response=""
                    };

                    In_parts.outgoingEmails.Add(fetcheddata);



                }

                In_parts.SaveChanges();
            }
            foreach (var part in Data)
            {
                part.Last_sent = DateTime.Now.Date; // Update the Date property to the current date and time
            }

            // Save the changes to the notifications table
            db.SaveChanges();
        }

        [Authorize]


        // POST: Notifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,Createdon,Last_sent,Active,Message,GroupName,Subject,Recepients")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                db.notifications.Add(notification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(notification);
        }

        // GET: Notifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notification notification = db.notifications.Find(id);
            if (notification == null)
            {
                return HttpNotFound();
            }
            return View(notification);
        }

        // POST: Notifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Createdon,Last_sent,Active,Message,GroupName,Subject")] Notification notification)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(notification);
        }

        // GET: Notifications/Delete/5
        public ActionResult Delete(int? id)
        {
           
            Notification notification = db.notifications.Find(id);
            db.notifications.Remove(notification);
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
