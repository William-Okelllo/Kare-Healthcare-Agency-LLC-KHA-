using Ishop.Infa;
using Ishop.Models;
using IShop.Core;
using IShop.Core.Interface;
using Microsoft.AspNet.SignalR.Hosting;
using Newtonsoft.Json;
using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Ishop.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private string apiKey = System.Configuration.ConfigurationManager.AppSettings["APIkey"].ToString();
        private string apiUrl = System.Configuration.ConfigurationManager.AppSettings["BalanceUrl"].ToString();
        private string shortcode1 = System.Configuration.ConfigurationManager.AppSettings["shortcode"].ToString();
        private string partnerID = System.Configuration.ConfigurationManager.AppSettings["partnerID"].ToString();
        public ActionResult Index()
        {

            Child_Context CZ = new Child_Context();
            var TotalChild = CZ.children.Select(c => c.Id).Count();
            ViewBag.Children = TotalChild;

            using (HttpClient httpClient = new HttpClient())
            {
                var Url = apiUrl;
                httpClient.BaseAddress = new Uri(Url);

               try
                {
                    var response = httpClient.GetAsync($"getbalance/?apikey={apiKey}&partnerID={partnerID}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        var apiData = JsonConvert.DeserializeObject<ApiResponse>(content);

                        ViewBag.Credit = apiData.Credit;
                        ViewBag.Response = "API is active and responsive";
                    }
                }
               catch (Exception ex)
                
                {
                    ViewBag.Credit = 0;
                    ViewBag.Response = "API is Inactive and Non-responsive " + ex.Message;

                }
            }

                return View();
        }

        

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}