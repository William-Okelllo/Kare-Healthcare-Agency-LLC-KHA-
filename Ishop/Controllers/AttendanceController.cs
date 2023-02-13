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
using System.Device.Location;
using PagedList;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls.WebParts;

namespace Ishop.Controllers
{
    public class AttendanceController : Controller
    {
        private AttendanceContext db = new AttendanceContext();

        // GET: Attendance
        public ActionResult Index(string searchBy, string search, int? page)
        {

            if (!(search == null))
            {
                return View(db.attendances.OrderByDescending(p => p.Id).Where(c => c.User_Account ==User.Identity.Name).ToList().ToPagedList(page ?? 1, 6));

            }
            else
            {
                return View(db.attendances.OrderByDescending(p => p.Id).Where(c => c.User_Account == User.Identity.Name).ToList().ToPagedList(page ?? 1, 6));


            }
        }
        public ActionResult Index_(string searchBy, string search, int? page)
        {

            if (!(search == null))
            {
                return View(db.attendances.OrderByDescending(p => p.Id).Where(c => c.User_Account == search).ToList().ToPagedList(page ?? 1, 6));

            }
            else
            {
                return View(db.attendances.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 6));


            }
        }

        // GET: Attendance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // GET: Attendance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Attendance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedOn,Business_location,latitude,longitude,Check_status,User_Account,Distance,Device,User_latitude,User_longitude")] Attendance attendance)
        {
            if (attendance.user_latitude == "Geolocation Denied")
            {
                TempData["msg"] = "Kindly enable location request on your device.";
                return RedirectToAction("Create");
            }

            attendance.Device = attendance.Check_status + Convert.ToString(attendance.CreatedOn.ToString("dd"));
            var sCoord = new GeoCoordinate(Convert.ToDouble(attendance.user_latitude), (Convert.ToDouble(attendance.user_longitude)));
            var eCoord = new GeoCoordinate(Convert.ToDouble(attendance.latitude), (Convert.ToDouble(attendance.longitude)));

            var Distance = sCoord.GetDistanceTo(eCoord);

            int Radius = Int16.Parse(System.Configuration.ConfigurationManager.AppSettings["Distance_Radius"]);

            if (Math.Round(Distance) >= Radius)
            {
                TempData["msg"] = User.Identity.Name + " you are not within " + attendance.Business_location + " premises " + "\n" + "   current location is :- " + Math.Round(Distance) + " meters away";
                return RedirectToAction("Create");
            }



            if (ModelState.IsValid)
            {

                try
                {
                    string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
                    SqlConnection sqlCon = new SqlConnection(strcon);
                    SqlCommand sqlcmnd = new SqlCommand("Checkin/Checkout", sqlCon);
                    sqlcmnd.CommandType = CommandType.StoredProcedure;
                    sqlcmnd.Parameters.AddWithValue("@Username", attendance.User_Account);
                    sqlcmnd.Parameters.AddWithValue("@type", attendance.Check_status);
                    sqlCon.Open();
                    sqlcmnd.ExecuteNonQuery();
                    sqlCon.Close();
                    
                    if (DateTime.Now.ToString("tt") == "AM")
                    {
                        attendance.Check_status = "CheckIn";
                        TempData["msg"] = "Successfully CheckedIn";
                    }
                    else
                    {
                        attendance.Check_status = "CheckOut";
                        TempData["msg"] = "Successfully CheckedOut";
                    }

                    attendance.Distance = ((decimal)(Math.Round(Distance)));
                    db.attendances.Add(attendance);
                    db.SaveChanges();

                    return RedirectToAction("Index");


                }
                catch (SqlException sqlEx)
                {
                    TempData["msg"] = "✔ Already Checked for the day";


                    return Redirect("Index");

                }
            }

            return View(attendance);
        }

        // GET: Attendance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: Attendance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Business_location,latitude,longitude,Check_status,User_Account")] Attendance attendance)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attendance);
        }

        // GET: Attendance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: Attendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendance attendance = db.attendances.Find(id);
            db.attendances.Remove(attendance);
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
