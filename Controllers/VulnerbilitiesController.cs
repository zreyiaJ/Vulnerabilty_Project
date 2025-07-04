using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vulnerabilty_Project.Models;

namespace Vulnerabilty_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VulnerbilitiesController : Controller
    {

        SqlConnection cnn = new SqlConnection("Data Source=HOMERSG;Initial Catalog=vulnerproj;Integrated Security=True;Encrypt=False");

        // GET: Vulnerbilities/Index
        
        public ActionResult Index()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM  Vulnerabilities", cnn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<VulnerbilitiesModel> list = new List<VulnerbilitiesModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                VulnerbilitiesModel vulner = new VulnerbilitiesModel();
                vulner.VulnID = Convert.ToInt32(dt.Rows[i]["VulnID"]);
                vulner.Title = dt.Rows[i]["Title"].ToString();
                vulner.Description = dt.Rows[i]["Description"].ToString();
                vulner.SeverityLevel = (Severity)Enum.Parse(typeof(Severity), dt.Rows[i]["SeverityLevel"].ToString());
                vulner.CurrentStatus = (Status)Enum.Parse(typeof(Status), dt.Rows[i]["CurrentStatus"].ToString());
                vulner.DiscoveredDate = Convert.ToDateTime(dt.Rows[i]["DiscoveredDate"]);
                vulner.AffectedSystem = dt.Rows[i]["AffectedSystem"].ToString();
                vulner.ReporterID = Convert.ToInt32(dt.Rows[i]["ReporterID"]);
                list.Add(vulner);

            }

            return View(list);
        }

        // GET: Vulnerbilities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vulnerbilities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VulnerbilitiesModel model)
        {

            cnn.Open();
            string query = @"INSERT INTO Vulnerbilities 
                        (Title, Description, SeverityLevel, CurrentStatus, DiscoveredDate, AffectedSystem, ReporterID)
                        VALUES (@Title, @Description, @SeverityLevel, @CurrentStatus, @DiscoveredDate, @AffectedSystem, @ReporterID)";

            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.Parameters.AddWithValue("@Title", model.Title);
            cmd.Parameters.AddWithValue("@Description", model.Description);
            cmd.Parameters.AddWithValue("@SeverityLevel", model.SeverityLevel.ToString());
            cmd.Parameters.AddWithValue("@CurrentStatus", model.CurrentStatus.ToString());
            cmd.Parameters.AddWithValue("@DiscoveredDate", model.DiscoveredDate);
            cmd.Parameters.AddWithValue("@AffectedSystem", model.AffectedSystem);
            cmd.Parameters.AddWithValue("@ReporterID", model.ReporterID);


            cmd.ExecuteNonQuery();
            cnn.Close();

            return RedirectToAction("Index");

        }

        public ActionResult Edit(int id)
        {
            int i = 0;
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM  Vulnerabilities WHERE VulnID ="+id, cnn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            VulnerbilitiesModel vulner = new VulnerbilitiesModel();
            vulner.VulnID = Convert.ToInt32(dt.Rows[i]["VulnID"]);
            vulner.Title = dt.Rows[i]["Title"].ToString();
            vulner.Description = dt.Rows[i]["Description"].ToString();
            vulner.SeverityLevel = (Severity)Enum.Parse(typeof(Severity), dt.Rows[i]["SeverityLevel"].ToString());
            vulner.CurrentStatus = (Status)Enum.Parse(typeof(Status), dt.Rows[i]["CurrentStatus"].ToString());
            vulner.DiscoveredDate = Convert.ToDateTime(dt.Rows[i]["DiscoveredDate"]);
            vulner.AffectedSystem = dt.Rows[i]["AffectedSystem"].ToString();
            vulner.ReporterID = Convert.ToInt32(dt.Rows[i]["ReporterID"]);

            return View(vulner);

        }
    }
}