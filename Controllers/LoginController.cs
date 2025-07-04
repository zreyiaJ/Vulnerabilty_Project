using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Vulnerabilty_Project.Models;
// To use the UserRole enum

namespace Vulnerabilty_Project.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        SqlConnection cnn = new SqlConnection("Data Source=HOMERSG;Initial Catalog=vulnerproj;Integrated Security=True;Encrypt=False");

        // GET: Login
        public ActionResult Login1()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Username and password are required.");
                return View();
            }
            string query = "SELECT * FROM UserMaster WHERE UserName = @UserName AND Password = @Password";
            cnn.Open();
            SqlCommand cmd = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.ExecuteNonQuery();
            cnn.Close();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                WebRoleProvider roleProvider = new WebRoleProvider();
                string[] roles = roleProvider.GetRolesForUser(userName);
                FormsAuthentication.SetAuthCookie(userName, false);
                

                ViewBag.UserName = dt.Rows[0]["UserName"].ToString(); ;
/*
                // Store user details in session
                Session["UserID"] = dt.Rows[0]["UserID"].ToString();
                Session["UserName"] = dt.Rows[0]["UserName"].ToString();

                Session["Email"] = dt.Rows[0]["Email"].ToString();*/


                // Redirect to dashboard
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View();
            }


        }

        // Logout action to clear session
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        /*SqlConnection cnn = new SqlConnection("Data Source=DESKTOP-MO3K6UL;Initial Catalog=vuner_prog;Integrated Security=True;Encrypt=False");
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

          

            using (cnn)
            {
                string query = "SELECT * FROM UserMaster WHERE UserName = @UserName AND Password = @Password";

                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@UserName", model.UserName);
                cmd.Parameters.AddWithValue("@Password", model.Password); 

                cnn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string role = reader["Role"].ToString();
                    string userName = reader["UserName"].ToString();

                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                   *//* // Create Forms Auth Ticket with Role
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,
                        userName,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        false,
                        role,
                        FormsAuthentication.FormsCookiePath
                    );

                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);*//*

                    return RedirectToAction("Index", "Home"); // Or your dashboard
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View(model);
                }
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }*/
    }
}
