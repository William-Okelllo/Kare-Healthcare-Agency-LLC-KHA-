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

namespace Ishop.Web.Controllers
{
    [Authorize]
    public class OutgoingsmsController : Controller
    {
        private OutgoingsmsContext db = new OutgoingsmsContext();

        // GET: Outgoingsms
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null) && (!(search == "")))
            {
                return View(db.outgoingsms.OrderByDescending(p => p.Id).Where(c => c.RecipientNumber.StartsWith(search) || c.RecipientNumber == search).ToList().ToPagedList(page ?? 1, 11));

            }
            else if (search == "")
            {
                return View(db.outgoingsms.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(db.outgoingsms.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));
            }

        }
        public ActionResult GetDataName(string Code)
        {
            Template_Context TT = new Template_Context();
            

            var data = TT.templates.FirstOrDefault(d => d.Code == Code);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Send_sms(int? id)
        {

           
            

            return View();
        }

        // POST: landloards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Send_sms([Bind(Include = "Id,CreatedOn,RecipientNumber,MessageText,Trials,Response,Code")] Outgoingsms outgoingsms)
        {
            outgoingsms.Trials = 0;
            outgoingsms.Response = "--waiting--";
            outgoingsms.IsSent = false;
            if (ModelState.IsValid)
            {
                db.outgoingsms.Add(outgoingsms);
                db.SaveChanges();
                TempData["msg"] = "Sms  queued  successfully ";
                return RedirectToAction("Index");
            }

            return View(outgoingsms);
        }
        public ActionResult Group()
        {


            Template_Context dbbb = new Template_Context();
            var Temp = dbbb.templates.ToList();
            ViewBag.Temp = new SelectList(Temp, "Code", "Code");

           
           
            return View();
        }

        // POST: landloards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Group([Bind(Include = "Id,CreatedOn,RecipientNumber,MessageText,Trials,Response")] Outgoingsms outgoingsms)
        {
            outgoingsms.Trials = 0;
            outgoingsms.Response = "--waiting--";
            outgoingsms.IsSent = false;
            if (ModelState.IsValid)
            {
                db.outgoingsms.Add(outgoingsms);
                db.SaveChanges();
                TempData["msg"] = "Group sms  queued  successfully ";
                return RedirectToAction("Index");
            }

            return View(outgoingsms);
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
