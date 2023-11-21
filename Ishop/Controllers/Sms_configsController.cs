using Ishop.Infa;
using IShop.Core;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Ishop.Controllers
{
    public class Sms_configsController : Controller
    {
        private Sms_configs_Context db = new Sms_configs_Context();

        // GET: Sms_configs
        public ActionResult Index()
        {
            return View(db.sms_Configs.ToList());
        }

        // GET: Sms_configs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sms_configs sms_configs = db.sms_Configs.Find(id);
            if (sms_configs == null)
            {
                return HttpNotFound();
            }
            return View(sms_configs);
        }

        // GET: Sms_configs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sms_configs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,APIkey,partnerID,shortcode,APIUrl")] Sms_configs sms_configs)
        {
            if (ModelState.IsValid)
            {
                db.sms_Configs.Add(sms_configs);
                Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
                //Modifying the AppKey from AppValue to AppValue1
                webConfigApp.AppSettings.Settings["APIkey"].Value = sms_configs.APIkey;
                webConfigApp.AppSettings.Settings["partnerID"].Value = sms_configs.partnerID;
                webConfigApp.AppSettings.Settings["shortcode"].Value = sms_configs.shortcode;
                webConfigApp.AppSettings.Settings["APIUrl"].Value = sms_configs.APIUrl;
                webConfigApp.Save();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sms_configs);
        }

        // GET: Sms_configs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sms_configs sms_configs = db.sms_Configs.Find(id);
            if (sms_configs == null)
            {
                return HttpNotFound();
            }
            return View(sms_configs);
        }

        // POST: Sms_configs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,APIkey,partnerID,shortcode,APIUrl")] Sms_configs sms_configs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sms_configs).State = EntityState.Modified;
                db.SaveChanges();
                Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
                //Modifying the AppKey from AppValue to AppValue1
                webConfigApp.AppSettings.Settings["APIkey"].Value = sms_configs.APIkey;
                webConfigApp.AppSettings.Settings["partnerID"].Value = sms_configs.partnerID;
                webConfigApp.AppSettings.Settings["shortcode"].Value = sms_configs.shortcode;
                webConfigApp.AppSettings.Settings["APIUrl"].Value = sms_configs.APIUrl;
                webConfigApp.Save();
                return RedirectToAction("Index");
            }
            return View(sms_configs);
        }

        // GET: Sms_configs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sms_configs sms_configs = db.sms_Configs.Find(id);
            if (sms_configs == null)
            {
                return HttpNotFound();
            }
            return View(sms_configs);
        }

        // POST: Sms_configs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sms_configs sms_configs = db.sms_Configs.Find(id);
            db.sms_Configs.Remove(sms_configs);
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
