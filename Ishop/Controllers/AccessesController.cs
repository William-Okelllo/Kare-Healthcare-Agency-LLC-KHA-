using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IShop.Core;
using Ishop.Infa;
using System.Data.SqlClient;
using Ishop.Models;
using System.Configuration;
using PagedList;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ishop.Controllers
{
    public class AccessesController : Controller
    {
        private AccessContext db = new AccessContext();

        // GET: Accesses
        public ActionResult Index(string searchBy, string search, int? page)
        {
            HREntities r10 = new HREntities();
            var data10 = r10.AccessRolesSp().ToList();
            ViewBag.A10 = data10;

            if (searchBy == "Reg")
            {
                return View(db.Acesss.Where(c => c.Username == search || search == null).ToList().ToPagedList(page ?? 1, 6));

            }
            else
            {
                return View(db.Acesss.Where(c => c.Username.StartsWith(search) || search == null).ToList().ToPagedList(page ?? 1, 6));

            }






        }

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
        public ActionResult Grant_Role(int? id)
        {

            ViewBag.Roles2 = GetSystemRole();
            return View();
        }
        private static List<A> GetSystemRole()
        {
            List<A> fruits = new List<A>();
            string constr = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "sp_GetRoles";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            fruits.Add(new A
                            {
                                Role = sdr["Role"].ToString(),


                            });
                        }
                    }
                    con.Close();
                }
            }

            return fruits;
        }

        // POST: Access/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Grant_Role([Bind(Include = "id,Role,Status")] Access access,string Id)
        {
            access.Username = Id;

            
           

            
                db.Acesss.Add(access);
                db.SaveChanges();
                try
                {
                    string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
                    SqlConnection cnn = new SqlConnection(cnnString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "SetRole";
                    cmd.Parameters.AddWithValue("@status", access.Status);
                    cmd.Parameters.AddWithValue("@role", access.Role);
                    cmd.Parameters.AddWithValue("@id", Id);
                    //add any parameters the stored procedure might require
                    cnn.Open();

                    object o = cmd.ExecuteScalar();
                    cnn.Close();
                }
                catch (SqlException sqlEx)
                {
                    TempData["msg2"] = "✔ Role already exist on account ";
                    return RedirectToAction("Grant_Role");
                    throw sqlEx;
                   
                }

                TempData["msg2"] = "✔ Success updating user account role ";
                return RedirectToAction("Index", "Data");
            
            
            return View(access);
        }
        // GET: Accesses/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Accesses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Username,Role,Status")] Access access)
        {
            if (ModelState.IsValid)
            {
                db.Entry(access).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(access);
        }

        // GET: Accesses/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Accesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Access access = db.Acesss.Find(id);
            db.Acesss.Remove(access);
            db.SaveChanges();
            return RedirectToAction("Index");
        }





        public ActionResult Acc_Profile()
        {
            ZO F = new ZO();
            var FF = F.Acc_Profile(User.Identity.Name).ToList();
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