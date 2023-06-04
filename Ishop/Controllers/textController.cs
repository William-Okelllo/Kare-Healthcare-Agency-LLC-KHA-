

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;

namespace Ishop.Controllers
{
    public class textController : Controller
    {
        // GET: text
        public ActionResult Index()
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
                    message = "ddddddd",
                    shortcode = shortcode1,
                    mobile = 0727422197

                };
                var json = JsonConvert.SerializeObject(requestBody);
                try
                {
                    var response = client.PostAsync(apiUrl, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { success = true, message = "SMS sent successfully." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var errorResponse = response.Content.ReadAsStringAsync().Result;
                        return Json(new { success = false, message = "Failed to send SMS: " + errorResponse }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }

        }
    }
}