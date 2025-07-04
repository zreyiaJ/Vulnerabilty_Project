using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vulnerabilty_Project.Models;

namespace Vulnerabilty_Project.Controllers
{
    public class RemediationController : Controller
    {
        // GET: Remediation

        SqlConnection cnn = new SqlConnection("Data Source=HOMERSG;Initial Catalog=vulnerproj;Integrated Security=True;Encrypt=False");

        //DIsplay table \
        [Authorize(Roles = "Admin, User, Developer")]
        public ActionResult Index()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM  Remediations", cnn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<RemediationModel> list = new List<RemediationModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RemediationModel remi = new RemediationModel();
                remi.RemediationID = int.Parse(dt.Rows[i]["RemediationID"].ToString());
                remi.VulnID = int.Parse(dt.Rows[i]["VulnID"].ToString());
                remi.EngineerID = int.Parse(dt.Rows[i]["EngineerID"].ToString());
                remi.ActionTaken = dt.Rows[i]["ActionTaken"].ToString();
                if (DateTime.TryParse(dt.Rows[i]["Timestamp"].ToString(), out DateTime parsedTimestamp))
                {
                    remi.Timestamp = parsedTimestamp;
                }
                else
                {
                    remi.Timestamp = DateTime.MinValue; // or handle accordingly
                }


                list.Add(remi);

            }
            return View(list);
        }

        //Insert Table  create Data
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RemediationModel model)
        {
            cnn.Open();
            string query = "INSERT INTO RemediationMaster (VulnID, EngineerID, ActionTaken, Timestamp) VALUES (@VulnID, @EngineerID, @ActionTaken, @Timestamp)";
            SqlCommand cmd = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@VulnID", model.VulnID);
            cmd.Parameters.AddWithValue("@EngineerID", model.EngineerID);
            cmd.Parameters.AddWithValue("@ActionTaken", model.ActionTaken);
            cmd.Parameters.AddWithValue("@Timestamp", model.Timestamp);

            
            cmd.ExecuteNonQuery();
            cnn.Close();

            return RedirectToAction("Index");

        }
    }
}