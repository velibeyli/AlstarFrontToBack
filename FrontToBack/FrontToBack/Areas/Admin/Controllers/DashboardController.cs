using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontToBack.Areas.Admin.Controllers
{
    [AuthorizeAdminFilter]
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
       
        public ActionResult Index()
        {
            return View();
        }
    }
}