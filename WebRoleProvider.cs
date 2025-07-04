using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using Vulnerabilty_Project.Models;

namespace Vulnerabilty_Project
{
    public class WebRoleProvider : RoleProvider
    {
        SqlConnection cnn = new SqlConnection("Data Source=DESKTOP-MO3K6UL;Initial Catalog=vuner_prog;Integrated Security=True;Encrypt=False");

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            string query = "SELECT RoleName FROM RoleMaster";
            SqlDataAdapter da = new SqlDataAdapter(query, cnn); 
            DataTable dt = new DataTable();
            da.Fill(dt);

            string[] roles = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                roles[i] = dt.Rows[i]["RoleName"].ToString();
            }

            return roles;
        }

        public override string[] GetRolesForUser(string username)
        {
            string query = " SELECT r.RoleName FROM UserMaster u INNER JOIN RoleMaster r ON u.RoleId = r.RoleId WHERE u.UserName = @UserName";
            SqlDataAdapter da = new SqlDataAdapter(query, cnn);
            da.SelectCommand.Parameters.AddWithValue("@UserName", username);
            DataTable dt = new DataTable();
            da.Fill(dt);
            string roleName = dt.Rows[0]["RoleName"].ToString();
            return new string[] { roleName };
        }

        public override string[] GetUsersInRole(string roleName)
        {   
             throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}