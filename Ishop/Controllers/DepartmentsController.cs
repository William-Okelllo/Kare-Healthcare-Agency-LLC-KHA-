using Ishop.Infa;
using Ishop.Models;
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
    public class DepartmentsController : Controller
    {
        private DepartmentContext db = new DepartmentContext();

        // GET: Departments
        public ActionResult Index(string searchBy, string search, int? page)
        {


            if (!(search == null))
            {
                return View(db.departments.OrderByDescending(p => p.Id).Where(c => c.DprtName == search).ToList().ToPagedList(page ?? 1, 6));

            }
            else
            {
                return View(db.departments.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 6));


            }
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            Userstable db = new Userstable();
            var boo = db.AspNetUsers.ToList();
            ViewBag.l = boo;



            return View();

        }


        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DprtName,Contact,Manager,Department,Email_Address")] Department department)
        {


            if (ModelState.IsValid)
            {
                db.departments.Add(department);
                db.SaveChanges();
                TempData["msg"] = "Department added successfully ";
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            Userstable ddb = new Userstable();
            var boo = ddb.AspNetUsers.ToList();
            ViewBag.l = boo;


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DprtName,Contact,Manager,Department,Email_Address")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Department information updated successfully ";
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.departments.Find(id);
            db.departments.Remove(department);
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
