using Ishop.Infa;
using Ishop.Models;
using IShop.Core;
using PagedList;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Ishop.Controllers
{
    [Authorize]
    public class OutgoingEmailsController : Controller
    {
        private OutgoingEmailsContext db = new OutgoingEmailsContext();

        // GET: OutgoingEmails
        public ActionResult Index(string searchBy, string search, int? page)
        {
            if (!(search == null) && (!(search == "")))
            {
                return View(db.outgoingEmails.OrderByDescending(p => p.Id).Where(c => c.Recipient.StartsWith(search) || c.Recipient == search).ToList().ToPagedList(page ?? 1, 11));

            }
            else if (search == "")
            {
                return View(db.outgoingEmails.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));


            }
            else
            {
                return View(db.outgoingEmails.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 11));
            }

        }

        public ActionResult Mine (string searchBy, string search, int? page)
        {
            Userstable users = new Userstable();
            var currentmail =users.AspNetUsers.Where(c=>c.UserName ==User.Identity.Name).FirstOrDefault();
            return View(db.outgoingEmails.OrderByDescending(p => p.Id).Where(c=>c.Recipient== currentmail.Email).ToList().ToPagedList(page ?? 1, 11));
            

        }

        // GET: OutgoingEmails/Details/5

        public ActionResult Send_email()
        {

            return View();
        }

        // POST: landloards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Send_email([Bind(Include = "Id,CreatedOn,Recipient,Subject,Body,Trials,Response")] OutgoingEmails outgoingEmails)
        {
            outgoingEmails.Trials = 0;
            outgoingEmails.Response = "--waiting--";
            outgoingEmails.Status = false;
            if (ModelState.IsValid)
            {
                db.outgoingEmails.Add(outgoingEmails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(outgoingEmails);
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
