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
    [Authorize]
    public class InDirectsController : Controller
    {
        private InDirect_Context db = new InDirect_Context();

        // GET: InDirects
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null) && (!(search == "")))
            {
                return View(db.InDirects.OrderByDescending(p => p.Id).Where(c => c.Name.StartsWith(search) || c.Name == search).ToList().ToPagedList(page ?? 1, 11));

            }
            else if (search == "")
            {
                return View(db.InDirects.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(db.InDirects.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));
            }

        }

        // GET: InDirects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InDirect inDirect = db.InDirects.Find(id);
            if (inDirect == null)
            {
                return HttpNotFound();
            }
            return View(inDirect);
        }

        // GET: InDirects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InDirects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreatedOn,Name,User")] InDirect inDirect)
        {
            if (ModelState.IsValid)
            {
                db.InDirects.Add(inDirect);
                db.SaveChanges();
                TempData["msg"] = "Category added successfully ";
                return RedirectToAction("Index");
            }

            return View(inDirect);
        }

        // GET: InDirects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InDirect inDirect = db.InDirects.Find(id);
            if (inDirect == null)
            {
                return HttpNotFound();
            }
            return View(inDirect);
        }

        // POST: InDirects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Name,User")] InDirect inDirect)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inDirect).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Category updated successfully ";
                return RedirectToAction("Index");
            }
            return View(inDirect);
        }

        // GET: InDirects/Delete/5

        public ActionResult Delete(int id)
        {
            InDirect inDirect = db.InDirects.Find(id);
            db.InDirects.Remove(inDirect);
            db.SaveChanges();
            TempData["msg"] = "Category deleted successfully ";
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
