using Ishop.Models;
using PagedList;
using System.Linq;
using System.Web.Mvc;

namespace Ishop.Controllers
{
    [Authorize]
    public class DataController : Controller
    {
        private Userstable db = new Userstable();

        // GET: Data
        public ActionResult Index(string searchBy, string search, int? page)
        {

            return View(db.AspNetUsers.ToList().ToPagedList(page ?? 1, 11));

        }



        // GET: Data/Details/5


        // GET: Data/Create
        public ActionResult Delete_User(string id)
        {

            var entityToDelete = db.AspNetUsers.Find(id);


            db.AspNetUsers.Remove(entityToDelete);
            db.SaveChanges();


            TempData["msg"] = "✔ Account deleted successfully";

            return RedirectToAction("Index", "Data");
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
