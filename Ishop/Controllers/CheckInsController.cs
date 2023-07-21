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
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using static Antlr.Runtime.Tree.TreeWizard;
using Ishop.Models;

namespace Ishop.Controllers
{
    [Authorize]
    public class CheckInsController : Controller
    {
        VehicleContext dbb = new VehicleContext();
        private CheckInContext db = new CheckInContext();

        // GET: CheckIns
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null))
            {
                return View(db.checkIns.OrderByDescending(p => p.Id).Where(c => c.Vehicle_Reg == search || c.Make.StartsWith(search)).ToList().ToPagedList(page ?? 1, 7));

            }
            else
            {
                return View(db.checkIns.OrderByDescending(p => p.Id).Where(c => c.Vehicle_Reg.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 6));
            }

        }
            // GET: CheckIns/Details/5
            public ActionResult Details(int? id)

        {
            filetab dbbb = new filetab();

            var data11 = db.checkIns.FirstOrDefault(c => c.Id == id);

            var data10 = dbbb.Files.Where(d => d.Uploaded_By == data11.Vehicle_Reg);
            ViewBag.CheckIn = data10;


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckIn checkIn = db.checkIns.Find(id);
            if (checkIn == null)
            {
                return HttpNotFound();
            }
            return View(checkIn);
        }


       

        // GET: CheckIns/Create
        public ActionResult Create()
        {
            Makes_context dbbb = new Makes_context();
            var autopart = dbbb.makes.ToList();
            ViewBag.Make = new SelectList(autopart, "Make", "Make");

            VehicleContext VVV = new VehicleContext();
            var Make = VVV.vehicles.Distinct().ToList();
            ViewBag.Model = new SelectList(Make, "Model", "Model");

            CoverContext CCC = new CoverContext();
            var Insuarance = CCC.covers.ToList();
            ViewBag.Insuarance = new SelectList(Insuarance, "Insuarance", "Insuarance");

            return View();
        }
        public ActionResult GetModelsByMake(string make)
        {
            using (var VVV = new VehicleContext())
            {
                var models = VVV.vehicles.Where(v => v.Make == make).Select(v => v.Model).Distinct().ToList();
                return Json(models, JsonRequestBehavior.AllowGet);
            }
        }
        // POST: CheckIns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedOn,Make,Model,Vehicle_Reg,Insuarance,Description,Customer,Phone,staff")] CheckIn checkIn)
        {
            if (ModelState.IsValid)
            {
                db.checkIns.Add(checkIn);
                db.SaveChanges();

                string daytime;
                if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 12)
                { daytime = "Good Morning"; }
                else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
                { daytime = "Good Afternoon"; }
                else
                { daytime = "Good Evenning"; }

                string message = daytime + " ," + checkIn.Customer
                + "\n" + "Your vehicle has been succesfully checkedin "
                + "\n" + "Vehicle Details"
                + "\n" + "..."
                + "\n" + "CheckIn Date : "+ checkIn.CreatedOn.ToString("dd-MMM-yyyy")
                + "\n" + "Vehicle Reg : " + checkIn.Vehicle_Reg
                + "\n" + "Vehicle : "+checkIn.Make +" "+" "+checkIn.Model
                + "\n" + "Regards , Receiption Desk ";

                try 
                {
                    sms_send(checkIn.Phone, message);
                }
                catch { }
                TempData["msg"] = "Vehicle CheckIn successfully";
                return RedirectToAction("Index");
            }

            return View(checkIn);
        }
        public void sms_send(string recipient, string message)
        {
            string APIkey1 = System.Configuration.ConfigurationManager.AppSettings["APIkey"].ToString();
            string apiUrl = System.Configuration.ConfigurationManager.AppSettings["APIUrl"].ToString();
            string shortcode1 = System.Configuration.ConfigurationManager.AppSettings["shortcode"].ToString();
            int partnerID1 = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["partnerID"].ToString());

            using (HttpClient client = new HttpClient())
            {
                // Prepare the request body
                var requestBody = new
                {
                    apikey = APIkey1,
                    partnerID = partnerID1,
                    message = message,
                    shortcode = shortcode1,
                    mobile = recipient

                };
                var json = JsonConvert.SerializeObject(requestBody);
                try
                {
                    var response = client.PostAsync(apiUrl, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["msg"] = "Vehicle CheckIn successfully";
                    }
                    else
                    {
                        var errorResponse = response.Content.ReadAsStringAsync().Result;
                        TempData["msg"] = "error occured  : ";
                    }
                }
                catch (Exception ex)
                {
                    TempData["msg"] = "An error occurred while sending the SMS:";
                }
            }
        }
        // GET: CheckIns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckIn checkIn = db.checkIns.Find(id);
            if (checkIn == null)
            {
                return HttpNotFound();
            }
            return View(checkIn);
        }

        // POST: CheckIns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Make,Model,Type,Vehicle_Reg,Insuarance,Description,Customer,staff")] CheckIn checkIn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(checkIn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(checkIn);
        }

        // GET: CheckIns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CheckIn checkIn = db.checkIns.Find(id);
            if (checkIn == null)
            {
                return HttpNotFound();
            }
            return View(checkIn);
        }

        // POST: CheckIns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CheckIn checkIn = db.checkIns.Find(id);
            db.checkIns.Remove(checkIn);
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
