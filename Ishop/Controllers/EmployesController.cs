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
using System.Configuration;
using System.Data.SqlClient;
using PagedList;
using Ishop.Models;

namespace Ishop.Controllers
{
    [Authorize]
    public class EmployesController : Controller
    {
        private Employee_Context db = new Employee_Context();

        // GET: Employes
        public ActionResult Index(string searchBy, string search, int? page)
        {
            

            if (!(search == null))
            {
                return View(db.Employees.OrderByDescending(p => p.Id).Where(c =>c.Username==search).ToList().ToPagedList(page ?? 1, 6));

            }
            else
            {
                return View(db.Employees.OrderByDescending(p => p.Id).ToList().ToPagedList(page ?? 1, 6));


            }
        }


        // GET: Employes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employes/Create


        // GET: Employes/Edit/5
        public ActionResult Update_Profile(int? id)
        {
           

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            ViewBag.Departments = Get_Departments();

            return View(employee);
        }
        private static List<E> Get_Departments()
        {
            List<E> fruits2 = new List<E>();
            string constr = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                string query = "sp_departments";
                string u2 = System.Web.HttpContext.Current.User.Identity.Name;

                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            fruits2.Add(new E
                            {
                                Department = sdr["Department"].ToString(),

                            });
                        }
                    }
                    con.Close();
                }
            }

            return fruits2;
        }

            // POST: Employes/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for 
            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update_Profile([Bind(Include = "Id,Username,Fullname,Phone,Department,Employee_Address,Home_Address,Notes,Last_Update,Email,Emergency_Contact,Wage,Current_Access")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Profile Updated Successfully";

                try
                {
                    string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
                    SqlConnection sqlCon = new SqlConnection(strcon);
                    SqlCommand sqlcmnd = new SqlCommand("AddPhone", sqlCon);
                    sqlcmnd.CommandType = CommandType.StoredProcedure;
                    sqlcmnd.Parameters.AddWithValue("@Phone", employee.Phone);
                    sqlcmnd.Parameters.AddWithValue("@Email", employee.Email);
                    sqlcmnd.Parameters.AddWithValue("@user", employee.Username);
                    sqlCon.Open();
                    sqlcmnd.ExecuteNonQuery();
                    sqlCon.Close();


                }
                catch
                {

                }
               
                    return RedirectToAction("Index");
                
            }

            return View(employee);
        }
        public ActionResult Employee_Files (string searchBy, string search, int? page,int? id)
        {

            string EmployeeName = null;
            int EmployeeId = (int)id;

            string constr = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(constr))
            {

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    cmd.CommandText = "GetEmpname2";
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value =EmployeeId;


                    EmployeeName = (string)cmd.ExecuteScalar();

                    conn.Close();


                }



            }




            Files_list_ db = new Files_list_();

            if (searchBy == "Product")
            {
                return View(db.Files.OrderByDescending(p => p.id).Where(c => c.Access == EmployeeName).ToList().ToPagedList(page ?? 1, 9));

            }
            else
            {
                return View(db.Files.OrderByDescending(p => p.id).Where(c => c.Access == EmployeeName).ToList().ToPagedList(page ?? 1, 9));

            }



        }

        // GET: Employes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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


        public ActionResult Add(Employee employee)
        {
            var Check = "1";

            string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(strcon);
            SqlCommand sqlcmnd = new SqlCommand("Role_Set", sqlCon);
            sqlcmnd.CommandType = CommandType.StoredProcedure;
            sqlcmnd.Parameters.AddWithValue("@id", employee.Id);
            sqlcmnd.Parameters.AddWithValue("@status", Check);

         

            sqlCon.Open();
            sqlcmnd.ExecuteNonQuery();
            sqlCon.Close();

            TempData["msg"] = "Account approved successfully";
            return RedirectToAction("Update_Profile", "Employes", new { id = employee.Id });
        }
        public ActionResult Revoke(Employee employee)
        {
            var Check = "0";

            string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(strcon);
            SqlCommand sqlcmnd = new SqlCommand("Role_Set", sqlCon);
            sqlcmnd.CommandType = CommandType.StoredProcedure;
            sqlcmnd.Parameters.AddWithValue("@id", employee.Id);
            sqlcmnd.Parameters.AddWithValue("@status", Check);

            sqlCon.Open();
            sqlcmnd.ExecuteNonQuery();
            sqlCon.Close();

            TempData["msg"] = "Account revoked successfully";
            return RedirectToAction("Update_Profile", "Employes", new { id = employee.Id });
        }








        public ActionResult Delete_File(int? id, string returnUrl)
        {
            Files_list_ db = new Files_list_();

            string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(strcon);
            SqlCommand sqlcmnd = new SqlCommand("Delete_File", sqlCon);
            sqlcmnd.CommandType = CommandType.StoredProcedure;
            sqlcmnd.Parameters.AddWithValue("@id", id);
           

            sqlCon.Open();
            sqlcmnd.ExecuteNonQuery();
            sqlCon.Close();
            return Redirect(returnUrl);
        }



        public ActionResult Delete_Acc (int? id)
        {
            uss db = new uss();
            string strcon = ConfigurationManager.ConnectionStrings["Ishop"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand("Delete_User", sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", id);
            sqlCon.Open();
            cmd.ExecuteNonQuery();
            sqlCon.Close();
            TempData["msg"] = "✔ Account deleted successfully";

            return RedirectToAction("Index", "Employes");


            
        }
    }
}