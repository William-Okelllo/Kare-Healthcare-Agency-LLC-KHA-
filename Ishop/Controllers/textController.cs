using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using System;
using Jose;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using Encoding = System.Text.Encoding;
namespace Ishop.Controllers
{
    public class textController : Controller
    {
        // GET: text
        public ActionResult Index()
        {
            



            sms_send();


            return View();
        }
    public void sms_send()
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
                message = "ddfsdffd",
                shortcode = shortcode1,
                mobile = 727422197

            };
            var json = JsonConvert.SerializeObject(requestBody);
            try
            {
                var response = client.PostAsync(apiUrl, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["msg"] = "SMS sent successfully.";
                }
                else
                {
                    var errorResponse = response.Content.ReadAsStringAsync().Result;
                    TempData["msg"] = "Failed to send SMS: ";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex;
            }
        }
    }
}
}