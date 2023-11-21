using Ishop.Infa;
using IShop.Core;
using PagedList;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ishop.Controllers
{
    public class TemplatesController : Controller
    {
        private Template_Context db = new Template_Context();

        // GET: Templates
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null) && (!(search == "")))
            {
                return View(db.templates.OrderByDescending(p => p.Id).Where(c => c.Code.StartsWith(search) || c.Code == search).ToList().ToPagedList(page ?? 1, 11));

            }
            else if (search == "")
            {
                return View(db.templates.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(db.templates.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));
            }

        }

        // GET: Templates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = db.templates.Find(id);
            if (template == null)
            {
                return HttpNotFound();
            }
            return View(template);
        }

        // GET: Templates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Templates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedOn,Code,Content")] Template template)
        {
            if (ModelState.IsValid)
            {
                db.templates.Add(template);
                db.SaveChanges();
                TempData["msg"] = "Templated added successfully ";
                return RedirectToAction("Index");
            }

            return View(template);
        }

        // GET: Templates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Template template = db.templates.Find(id);
            if (template == null)
            {
                return HttpNotFound();
            }
            return View(template);
        }

        // POST: Templates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Code,Content")] Template template)
        {
            if (ModelState.IsValid)
            {
                db.Entry(template).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(template);
        }


        public ActionResult Delete(int id)
        {
            Template template = db.templates.Find(id);
            db.templates.Remove(template);
            db.SaveChanges();
            TempData["msg"] = "Templated dropped successfully ";
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
