using Ishop.Infa;
using Ishop.Models;
using IShop.Core;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ishop.Controllers
{
    [Authorize]
    public class AccessesController : Controller
    {
        private AccessContext db = new AccessContext();

        // GET: Accesses


        // GET: Accesses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Access access = db.Acesss.Find(id);
            if (access == null)
            {
                return HttpNotFound();
            }
            return View(access);
        }

        // GET: Accesses/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Grant_Role(string UserId)
        {

            ApplicationDbContext AAA = new ApplicationDbContext();
            var role = AAA.Roles.ToList();
            ViewBag.role = new SelectList(role, "Name", "Name");

            ViewBag.key = UserId;



            ViewBag.Username = UserId;

            var Userinfo = dbb.AspNetUsers.Where(a => a.Id == UserId).FirstOrDefault();

            ViewBag.Userinfo = Userinfo;

            string strcon = ConfigurationManager.ConnectionStrings["Planning"].ConnectionString;
            using (SqlConnection sqlCon = new SqlConnection(strcon))
            {
                using (SqlCommand cmd = new SqlCommand("Assigned_user_roles", sqlCon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", UserId);
                    sqlCon.Open();

                    // Execute the stored procedure and get the result
                    object result = cmd.ExecuteScalar();

                    sqlCon.Close();

                    // Assign the result to a ViewBag variable
                    ViewBag.Result = result;

                }
            }




            return View();
        }
        private Userstable dbb = new Userstable();

        // POST: Access/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Grant_Role([Bind(Include = "id,Role,Status,Username")] Access access, Guid Userid)
        {
            ApplicationDbContext AAA = new ApplicationDbContext();
            var role = AAA.Roles.SingleOrDefault(r => r.Name == access.Role);


            if (access.Status == "1")
            {

                string existenceQuery = "SELECT 1 FROM AspNetUserRoles WHERE UserId = @UserId AND RoleId = @RoleId";
                var userIdParam = new SqlParameter("@UserId", Userid);
                var roleIdParam = new SqlParameter("@RoleId", role.Id);

                var exists = AAA.Database.SqlQuery<int>(existenceQuery, userIdParam, roleIdParam).Any();

                if (!exists)
                {
                    Set_Role(Userid, role.Id);
                    TempData["msg"] = access.Role + " role added on the account";
                }
                else
                {
                    TempData["msg"] = access.Role + " already exist for the account";
                    string returnUrl = Request.UrlReferrer.ToString();
                    return Redirect(returnUrl);
                }
            }
            else
            {
                string existenceQuery = "SELECT 1 FROM AspNetUserRoles WHERE UserId = @UserId AND RoleId = @RoleId";
                var userIdParam = new SqlParameter("@UserId", Userid);
                var roleIdParam = new SqlParameter("@RoleId", role.Id);
                bool exists = AAA.Database.SqlQuery<int>(existenceQuery, userIdParam, roleIdParam).Any();
                if (exists == false)
                {
                    TempData["msg"] = access.Role + " role does not exisit on the account";
                    string returnUrl = Request.UrlReferrer.ToString();
                    return Redirect(returnUrl);
                }
                else
                {
                    Drop_Role(Userid, role.Id);
                    TempData["msg"] = access.Role + " role dropped on the account";
                }
            }

            db.Acesss.Add(access);
            db.SaveChanges();

            string returnUrl2 = Request.UrlReferrer.ToString();
            return Redirect(returnUrl2);
        }
        // GET: Accesses/Edit/5

        private string connectionString = ConfigurationManager.ConnectionStrings["Planning"].ConnectionString;
        public void Set_Role(Guid UserId, string RoleId)
        {
            string query = "Insert into AspNetUserRoles" +
                " (UserId,RoleId) VALUES " +
            "     (@UserId,@RoleId)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@RoleId", RoleId);


                    command.ExecuteNonQuery();
                }
            }

        }
        public void Drop_Role(Guid UserId, string RoleId)
        {
            string query = "DELETE FROM AspNetUserRoles WHERE UserId = @UserId AND RoleId = @RoleId";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@RoleId", RoleId);

                    command.ExecuteNonQuery();
                }
            }
        }


        public ActionResult Acc_Profile()
        {
            ApplicationDbContext F = new ApplicationDbContext();
            var FF = F.Users.Where(c => c.UserName == User.Identity.Name).ToList();
            ViewBag.F = FF;
            string Current = User.Identity.Name;
            return View();
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