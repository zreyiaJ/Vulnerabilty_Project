using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vulnerabilty_Project.Controllers
{
    public class ExampleViewsController : Controller
    {
        // GET: ExampleViews
        [Authorize(Roles = "Admin")]
        public ActionResult View1()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        public ActionResult View2()
        {
            return View();
        }
        [Authorize(Roles = "Developer")]
        public ActionResult View3()
        {
            return View();
        }
    }
}