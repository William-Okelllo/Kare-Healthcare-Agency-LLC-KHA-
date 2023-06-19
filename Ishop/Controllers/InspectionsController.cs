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
using Rotativa;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Ishop
{
    [Authorize]
    public class InspectionsController : Controller
    {
        private Inspection_Context db = new Inspection_Context();

        // GET: Inspections
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null))
            {
                return View(db.inspections.OrderByDescending(p => p.Id).Where(c => c.Vehicle_Reg == search || c.Vehicle_Reg.StartsWith(search)).ToList().ToPagedList(page ?? 1, 7));

            }
            else
            {
                return View(db.inspections.OrderByDescending(p => p.Id).Where(c => c.Vehicle_Reg.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 6));
            }




        }

        
        
        // GET: Inspections/Edit/5
        public ActionResult Inspect(int? id )
        {
            ViewBag.id2 = id;


            var data10 = db.inspections.FirstOrDefault(d => d.Id == id);


            Inspections_partsContext Pd = new Inspections_partsContext();
            var data11 = Pd.inspections_Parts.OrderByDescending(c => c.Id).Where(d => d.Booking_Id == data10.Booking_Id);
            ViewBag.Auto = data11;

            Inspec_serv_context Dp = new Inspec_serv_context();
            var data12 = Dp.Inspection_Servs.OrderByDescending(c => c.Id).Where(d => d.Booking_Id == data10.Booking_Id);
            ViewBag.Services = data12;

            decimal Autopart = Pd.inspections_Parts.Where(d => d.Booking_Id == data10.Booking_Id).Select(d => d.Total_Amount).DefaultIfEmpty(0).Sum();
            decimal Services = Dp.Inspection_Servs.Where(d => d.Booking_Id == data10.Booking_Id).Select(d => d.Total_Amount).DefaultIfEmpty(0).Sum();
            decimal combinedSum = Autopart;
            decimal combinedSum2 = Services;

            ViewBag.InspectionTotals = combinedSum + combinedSum2;




            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspection inspection = db.inspections.Find(id);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            return View(inspection);
        }

        // POST: Inspections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inspect([Bind(Include = "Id,CreatedOn,Booking_Id,Vehicle,Vehicle_Reg,Phone,Description,Customer,staff,Inspection_Status,Estimate,Inspection_Total")] Inspection inspection ,string action)
        
            {
            if (action == "save")
            {
                inspection.Inspection_Status = 369;
                if (ModelState.IsValid)
                {
                    db.Entry(inspection).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["msg"] = "Inspection saved successfully";
                    return RedirectToAction("Index");
                }
            }
            else if (action == "saveAndSubmit")
            {
                inspection.Inspection_Status = 1;
                if (ModelState.IsValid)
                {
                    db.Entry(inspection).State = EntityState.Modified;
                    db.SaveChanges();

                    try
                    {
                        string strcon = ConfigurationManager.ConnectionStrings["GRS"].ConnectionString;
                        SqlConnection sqlCon = new SqlConnection(strcon);
                        SqlCommand cmd = new SqlCommand("Gen_Invoices", sqlCon);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Bookingid", inspection.Booking_Id);
                        sqlCon.Open();
                        cmd.ExecuteNonQuery();
                        sqlCon.Close();
                       
                    }
                    catch { }
                    TempData["msg"] = "Inspection completed successfully";
                    return RedirectToAction("Index");

                    string daytime;
                    if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 12)
                    { daytime = "Good Morning"; }
                    else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
                    { daytime = "Good Afternoon"; }
                    else
                    { daytime = "Good Evenning"; }

                    string message = daytime + " ," + inspection.Customer
                    + "\n" + "Your vehilce is ready for pickup kindly find payment invoice details "
                    + "\n" + "Invoice Details"
                    + "\n" + "..."
                    + "\n" + "Invoice Date : " + inspection.CreatedOn.ToString("dd-MMM-yyyy")
                    + "\n" + "Client : " + inspection.Customer
                    + "\n" + "Invoice Amount : " + inspection.Inspection_Total
                    + "\n" + "Regards ,  Accounts ";

                    try
                    {
                        sms_send(inspection.Phone, message);
                    }
                    catch { }
                }
            }
            return View(inspection);
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
        // GET: Inspections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspection inspection = db.inspections.Find(id);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            return View(inspection);
        }

        // POST: Inspections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inspection inspection = db.inspections.Find(id);
            db.inspections.Remove(inspection);
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

        public ActionResult Inspection_Print(int id)
        {
            var card = db.inspections.FirstOrDefault(c => c.Id == id);


            var card2 = db.inspections.Where(c => c.Id == id);
            ViewBag.data20 = card2;

            Inspections_partsContext bb = new Inspections_partsContext();
            var data14 = bb.inspections_Parts.Where(d => d.Booking_Id == card.Booking_Id);


            Inspec_serv_context nn = new Inspec_serv_context();
            var data15 = nn.Inspection_Servs.Where(d => d.Booking_Id == card.Booking_Id);


            decimal Autopart = bb.inspections_Parts.Where(d => d.Booking_Id == card.Booking_Id).Select(d => d.Amount).DefaultIfEmpty(0).Sum();
            decimal Services = nn.Inspection_Servs.Where(d => d.Booking_Id == card.Booking_Id).Select(d => d.Amount).DefaultIfEmpty(0).Sum();
            decimal combinedSum = Autopart;
            decimal combinedSum2 = Services;
            ViewBag.Estimate1 = combinedSum + combinedSum2;


            decimal Autopart2 = bb.inspections_Parts.Where(d => d.Booking_Id == card.Booking_Id).Select(d => d.Total_Amount).DefaultIfEmpty(0).Sum();
            decimal Services2 = nn.Inspection_Servs.Where(d => d.Booking_Id == card.Booking_Id).Select(d => d.Total_Amount).DefaultIfEmpty(0).Sum();
            decimal combinedSum222 = Autopart;
            decimal combinedSum2222 = Services;
            ViewBag.Estimate2 = combinedSum2 + combinedSum2222;









            Inspections_partsContext Pd = new Inspections_partsContext();
            var data11 = Pd.inspections_Parts.Where(d => d.Booking_Id == card.Booking_Id);
            ViewBag.Auto = data11;

            Inspec_serv_context Pe = new Inspec_serv_context();
            var data12 = Pe.Inspection_Servs.Where(d => d.Booking_Id == card.Booking_Id);
            ViewBag.Service = data12;


            var FF = db.inspections.Where(c => c.Id == id).ToList();
            ViewBag.F = FF;

            {
                Inspection customer = db.inspections.FirstOrDefault(c => c.Id == id);

                var report = new PartialViewAsPdf("~/Views/Inspections/Inspection_Print.cshtml", customer);
                return report;
            }
        }
    }
}
