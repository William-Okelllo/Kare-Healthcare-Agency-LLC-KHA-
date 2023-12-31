using Ishop.Infa;
using IShop.Core;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ishop.Controllers
{
    public class TeamsController : Controller
    {
        private Team_Context db = new Team_Context();

        // GET: Teams
        public ActionResult Index()
        {
            return View(db.teams.ToList());
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }
        public ActionResult CheckValueExists(string Username, string Project_Name)
        {
            bool exists = db.teams.Where(x => x.Project_Name == Project_Name).Any(c => c.Username == Username);
            return Json(exists, JsonRequestBehavior.AllowGet);
        }
        // GET: Teams/Create
        public ActionResult Add(string Project, int Id)
        {

            Employee_Context Ep = new Employee_Context();
            var Employee = Ep.employees.ToList();
            ViewBag.Employee = new SelectList(Employee, "Username", "Username");


            ViewBag.Project = Project;
            ViewBag.ProjectId = Id;

            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,Project_Name,Username,CreatedOn,Project_id,Hours")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.teams.Add(team);
                db.SaveChanges();
                TempData["msg"] = "✔   Team member added successfully ";
                return RedirectToAction("Details", "projects", new { id = team.Project_id });
            }

            return View(team);
        }


        public ActionResult Delete(int id)
        {
            Team team = db.teams.Find(id);
            db.teams.Remove(team);
            db.SaveChanges();
            TempData["msg"] = "✔   Team member dropped successfully ";
            string returnUrl = Request.UrlReferrer.ToString();
            return Redirect(returnUrl);
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
