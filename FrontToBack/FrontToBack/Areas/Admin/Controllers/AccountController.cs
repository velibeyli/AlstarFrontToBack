using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrontToBack.Models;
using System.Web.Helpers;

namespace FrontToBack.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly AlstarEntities _db;

        public AccountController()
        {
            _db = new AlstarEntities();
        }
        // GET: Admin/Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AdminSetting entrySetting)
        {
            AdminSetting adminSetting = _db.AdminSettings.First();

            if(entrySetting.Username == adminSetting.Username && 
                Crypto.VerifyHashedPassword(adminSetting.Password, entrySetting.Password))
            {
                Session["adminLogged"] = true;
                return RedirectToAction("Index","Dashboard");
            }

            ViewBag.LoginError = "Username or password is wrong";

            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        [AuthorizeAdminFilter]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [AuthorizeAdminFilter]
        [HttpPost]
        public ActionResult ChangePassword(ChangePassword changePassword)
        {
            if (ModelState.IsValid)
            {
                if(Crypto.VerifyHashedPassword(_db.AdminSettings.First().Password, changePassword.OldPassword))
                {
                    _db.AdminSettings.First().Password = Crypto.HashPassword(changePassword.NewPassword);

                    _db.SaveChanges();

                    Session.Clear();
                    return RedirectToAction("Login","Account");
                }

                ModelState.AddModelError("OldPassword","Old password is wrong");
            }

            return View();
        }


        [AuthorizeAdminFilter]
        public ActionResult ChangeUsername()
        {
            return View();
        }

        [AuthorizeAdminFilter]
        [HttpPost]
        public ActionResult ChangeUsername(string username)
        {
            if(string.IsNullOrEmpty(username) || username.Length < 3)
            {
                ViewBag.UsernameError = "Username","Username is not valid. It should be at least 3 long");
                return View();
            }

            _db.AdminSettings.First().Username = username;
            _db.SaveChanges();

            return RedirectToAction("Index","Dashboard");
        }
    }
}