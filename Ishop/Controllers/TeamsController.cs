using Ishop.Infa;
using IShop.Core;
using System.Data;
using System.Data.Entity;
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
            var data = db.teams.Where(c => c.Project_Name == Project_Name && c.Username == Username).ToList();

            if (data.Any())
            {
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var message = "Staff has no assigned phases";
                return Json(new { Message = message }, JsonRequestBehavior.AllowGet);
            }
        }





        private Direct_Context dbb = new Direct_Context();
        public ActionResult GetPhaseId(string Name)
        {
            var data = dbb.directs.FirstOrDefault(d => d.Name == Name);

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        // GET: Teams/Create
        public ActionResult Add(string Project, int Id)
        {
            Phase_Context PP = new Phase_Context();
            var Phase = PP.phases.Where(p=>p.Project_id ==Id).ToList();
            ViewBag.Phase = new SelectList(Phase, "Name", "Name");


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
        public ActionResult Add([Bind(Include = "Id,Project_Name,Username,CreatedOn,Project_id,Hours,Name,Step")] Team team)
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
        public ActionResult Edit(int? id)
        {
            var Teamm = db.teams.Where(c => c.Id == id).FirstOrDefault();

            Phase_Context PP = new Phase_Context();
            var Phase = PP.phases.Where(p => p.Project_id == Teamm.Project_id).ToList();
            ViewBag.Phase = new SelectList(Phase, "Name", "Name");


            Employee_Context Ep = new Employee_Context();
            var Employee = Ep.employees.ToList();
            ViewBag.Employee = new SelectList(Employee, "Username", "Username");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team inDirect = db.teams.Find(id);
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
        public ActionResult Edit([Bind(Include = "Id,Project_Name,Username,CreatedOn,Project_id,Hours,Name,Step")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Team member info updated successfully ";
               
            }
            return RedirectToAction("Details", "projects", new { id = team.Project_id });
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
