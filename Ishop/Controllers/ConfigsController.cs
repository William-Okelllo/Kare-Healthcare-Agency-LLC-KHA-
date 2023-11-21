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
    public class ConfigsController : Controller
    {
        private ConfigsContext db = new ConfigsContext();

        // GET: Configs
        public ActionResult Index()
        {
            return View(db.configs.ToList());
        }

        // GET: Configs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configs config = db.configs.Find(id);
            if (config == null)
            {
                return HttpNotFound();
            }
            return View(config);
        }

        // GET: Configs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Configs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Email,Password,SSRSReportsUrl,Business_mail,Smtp,Port,RunTime")] Configs config)
        {
            if (ModelState.IsValid)
            {
                db.configs.Add(config);
                Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
                //Modifying the AppKey from AppValue to AppValue1
                webConfigApp.AppSettings.Settings["Email"].Value = config.Email;
                webConfigApp.AppSettings.Settings["Password"].Value = config.Password;
                webConfigApp.AppSettings.Settings["smtp"].Value = config.Smtp;
                webConfigApp.AppSettings.Settings["SSRSReportsUrl"].Value = config.SSRSReportsUrl;
                webConfigApp.AppSettings.Settings["Businesssmail"].Value = config.Business_mail;
                webConfigApp.AppSettings.Settings["Port"].Value = config.port;
                webConfigApp.AppSettings.Settings["RunTime"].Value = config.RunTime.ToString();
                //Save the Modified settings of AppSettings.
                webConfigApp.Save();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(config);
        }

        // GET: Configs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configs config = db.configs.Find(id);
            if (config == null)
            {
                return HttpNotFound();
            }
            return View(config);
        }

        // POST: Configs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Email,Password,SSRSReportsUrl,Business_mail,Smtp,RunTime,port")] Configs config)
        {
            if (ModelState.IsValid)
            {
                db.Entry(config).State = EntityState.Modified;
                Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
                //Modifying the AppKey from AppValue to AppValue1
                webConfigApp.AppSettings.Settings["Email"].Value = config.Email;
                webConfigApp.AppSettings.Settings["Password"].Value = config.Password;
                webConfigApp.AppSettings.Settings["smtp"].Value = config.Smtp;
                webConfigApp.AppSettings.Settings["SSRSReportsUrl"].Value = config.SSRSReportsUrl;
                webConfigApp.AppSettings.Settings["Businesssmail"].Value = config.Business_mail;
                webConfigApp.AppSettings.Settings["Port"].Value = config.port;
                webConfigApp.AppSettings.Settings["RunTime"].Value = config.RunTime.ToString();
                //Save the Modified settings of AppSettings.
                webConfigApp.Save();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(config);
        }

        // GET: Configs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Configs config = db.configs.Find(id);
            if (config == null)
            {
                return HttpNotFound();
            }
            return View(config);
        }

        // POST: Configs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Configs config = db.configs.Find(id);
            db.configs.Remove(config);
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
