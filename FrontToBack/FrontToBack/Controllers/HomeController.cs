using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrontToBack.Models;
using FrontToBack.ViewModel;

namespace FrontToBack.Controllers
{
    public class HomeController : Controller
    {
        private readonly AlstarEntities _db;
        public HomeController()
        {
            _db = new AlstarEntities();
        }
        public ActionResult Index()
        {

            HomeIndexVM vm = new HomeIndexVM()
            {
                Sliders = _db.Sliders.ToList(),
                About = _db.Abouts.First(),
                Parallax1 = _db.Parallax1.First()
            };

            return View(vm);
        }

        public ActionResult Services()
        {
            return View(_db.Services.ToList());
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}