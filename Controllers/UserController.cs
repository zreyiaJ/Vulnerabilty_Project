using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Vulnerabilty_Project.Models;
using static Vulnerabilty_Project.Models.UserModel;

namespace Vulnerabilty_Project.Controllers
{
    public class UserController: Controller
    {
        SqlConnection cnn = new SqlConnection("Data Source=HOMERSG;Initial Catalog=vulnerproj;Integrated Security=True;Encrypt=False");

        //DIsplay table
        [Authorize(Roles = "Admin,Developer,User")]
        public ActionResult Index()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM  UserMaster",cnn);
            DataTable dt = new DataTable(); 
            da.Fill(dt);
            List <UserModel> list = new List<UserModel>();
            for (int i = 0;i<dt.Rows.Count; i++)
            {
                UserModel user = new UserModel();
                user.UserID = int.Parse(dt.Rows[i]["UserID"].ToString());
                user.UserName = dt.Rows[i]["Username"].ToString();
                user.Password = dt.Rows[i]["Password"].ToString();
                user.Email = dt.Rows[i]["Email"].ToString();

                user.Name = dt.Rows[i]["Name"].ToString();
                user.PhoneNo = dt.Rows[i]["PhoneNo"].ToString();

                list.Add(user);

            }
            return View(list);
        }

        //Insert Table  create Data
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserModel model)
        {
            cnn.Open();
            String Query = "INSERT INTO UserMaster (UserName,Password,Email,RoleId,Name,PhoneNo) VALUES (@UserName,@Password,@Email,@RoleId,@Name,@PhoneNo)";
            SqlCommand cmd = new SqlCommand(Query,cnn);
            cmd.Parameters.AddWithValue("@UserName", model.UserName);
            cmd.Parameters.AddWithValue("@Password", model.Password);
            cmd.Parameters.AddWithValue("@Email", model.Email);
            cmd.Parameters.AddWithValue("@RoleId", model.RoleId);
            cmd.Parameters.AddWithValue("@Name", model.Name);
            cmd.Parameters.AddWithValue("@PhoneNo", model.PhoneNo);
            cmd.ExecuteNonQuery();
            cnn.Close();

            return RedirectToAction("Index");
        }

        //Edit Data
        public ActionResult Edit(int id)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM UserMaster WHERE UserID = @UserID", cnn);
            da.SelectCommand.Parameters.AddWithValue("@UserID", id);

            DataTable dt = new DataTable();
            da.Fill(dt);
            int i = 0;
            UserModel user = new UserModel();
            user.UserID = int.Parse(dt.Rows[i]["UserID"].ToString());
            user.UserName = dt.Rows[i]["Username"].ToString();
            user.Password = dt.Rows[i]["Password"].ToString();
            user.Email = dt.Rows[i]["Email"].ToString();
            user.RoleId = int.Parse(dt.Rows[i]["RoleId"].ToString());
            user.Name=dt.Rows[i]["Name"].ToString();
            user.PhoneNo = dt.Rows[i]["PhoneNo"].ToString();

            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(UserModel model)
        {
            cnn.Open();
            string query = "UPDATE UserMaster SET UserName=@UserName, Password=@Password, Email=@Email, RoleId=@RoleId, Name=@Name, PhoneNo=@PhoneNo WHERE UserID = @UserID";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.Parameters.AddWithValue("@UserName", model.UserName);
            cmd.Parameters.AddWithValue("@Password", model.Password);
            cmd.Parameters.AddWithValue("@Email", model.Email);
            cmd.Parameters.AddWithValue("@RoleId", model.RoleId);
            cmd.Parameters.AddWithValue("@UserID", model.UserID);
            cmd.Parameters.AddWithValue("@Name",model.Name);
            cmd.Parameters.AddWithValue("@PhoneNo", model.PhoneNo);
            cmd.ExecuteNonQuery();
            cnn.Close();

            return RedirectToAction("Index");
        
        }

        public ActionResult Delete(int id) 
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM UserMaster WHERE UserID=" + id, cnn);
            cmd.ExecuteNonQuery();
            cnn.Close();
            return RedirectToAction("Index");
        }

        /*public ActionResult Details(string UserName)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM UserMaster WHERE UserName = @UserName", cnn);
            da.SelectCommand.Add("@UserName", UserName);

            DataTable dt = new DataTable();
            da.Fill(dt);
            int i = 0;
            UserModel user = new UserModel();
            user.UserID = int.Parse(dt.Rows[i]["UserID"].ToString());
            user.UserName = dt.Rows[i]["Username"].ToString();
            user.Password = dt.Rows[i]["Password"].ToString();
            user.Email = dt.Rows[i]["Email"].ToString();

            string roleStr = dt.Rows[i]["Role"].ToString();
            if (Enum.TryParse(roleStr, out UserRole parsedRole))
            {
                user.Role = parsedRole;
            }
            else
            {
                user.Role = UserRole.User; // Default or fallback
            }

            user.Name = dt.Rows[i]["Name"].ToString();
            user.PhoneNo = dt.Rows[i]["PhoneNo"].ToString();
            return View(user);
        }*/
        [Authorize(Roles = "Admin, Developer, User")]
        public ActionResult Details(string id)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM UserMaster WHERE UserName = @UserName", cnn);
            da.SelectCommand.Parameters.AddWithValue("@UserName", id);

            DataTable dt = new DataTable();
            da.Fill(dt);
            int i = 0;
            UserModel user = new UserModel();
            user.UserID = int.Parse(dt.Rows[i]["UserID"].ToString());
            user.UserName = dt.Rows[i]["Username"].ToString();
            user.Password = dt.Rows[i]["Password"].ToString();
            user.Email = dt.Rows[i]["Email"].ToString();
            user.RoleId = int.Parse(dt.Rows[i]["RoleId"].ToString());
            user.Name = dt.Rows[i]["Name"].ToString();
            user.PhoneNo = dt.Rows[i]["PhoneNo"].ToString();

            return View(user);


        }

    }
}