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
    public class EmployeesController : Controller
    {
        private Employee_Context db = new Employee_Context();

        // GET: Employees
        public ActionResult Index( string search, int? page, string Department)
        {

            var Employ = db.employees.Select(t => t.Username).Distinct().ToList();
            ViewBag.Usernames = Employ;

            var Departments = db.employees.Select(t => t.DprtName).Distinct().ToList();
            ViewBag.Departments = Departments;

            if (!(search == null) && (!(search == "")))
            {
                return View(db.employees.OrderByDescending(p => p.Id).Where(c => c.Username == search && Department == "" || c.DprtName == (Department)).ToList().ToPagedList(page ?? 1, 11));

            }
            else if (search == "")
            {
                return View(db.employees.OrderByDescending(p => p.Id).Where(c => c.DprtName == "" || c.DprtName == (Department)).ToList().ToPagedList(page ?? 1, 11));


            }
            else if (Department == "")
            {
                return View(db.employees.OrderByDescending(p => p.Id).Where(c => c.Username == "" || c.Username == (search)).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(db.employees.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));
            }

        }


        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {

            DepartmentContext DC = new DepartmentContext();
            var Department = DC.departments.ToList();
            ViewBag.Department = new SelectList(Department, "DprtName", "DprtName");



            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreatedOn,Username,Fullname,Contact,DprtName,Designation,Userid,Rate,Email,Active")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "✔ Employee info updated successfully";
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5


        // POST: Employees/Delete/5
        private Userstable dbb = new Userstable();

        public ActionResult Delete(int id)
        {
            var Emp = db.employees.Where(emp => emp.Id == id).FirstOrDefault();
            var Euser = dbb.AspNetUsers.Where(emp => emp.UserName == Emp.Username).FirstOrDefault();
            AspNetUser u = dbb.AspNetUsers.Find(Euser.Id);
            dbb.AspNetUsers.Remove(u);
            dbb.SaveChanges();

            Employee employee = db.employees.Find(id);
            db.employees.Remove(employee);
            db.SaveChanges();
            TempData["msg"] = "✔ Employee Account deleted successfully";



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
