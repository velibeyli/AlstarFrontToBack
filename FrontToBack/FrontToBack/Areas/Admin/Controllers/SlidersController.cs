﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrontToBack.Models;

namespace FrontToBack.Areas.Admin.Controllers
{
    public class SlidersController : Controller
    {
        private readonly AlstarEntities _db;
        // GET: Admin/Sliders
        public SlidersController()
        {
            _db = new AlstarEntities();
        }
        public ActionResult Index()
        {
            return View(_db.Sliders);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Image")]Slider slider,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Extensions.CheckImageType(Image) && Extensions.CheckImageSize(Image, 10))
                {
                   slider.Image = Extensions.SaveImage(Server.MapPath("~/Public/img/team"),Image);

                    _db.Sliders.Add(slider);
                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Image","Sekilin tipi uygun deyil ve ya olcusu 10 mb - dan artiqdir");
                }
            }
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
                return HttpNotFound("Id is missing");

            Slider slider = _db.Sliders.Find(id);

            if (slider == null)
                return HttpNotFound("Id is not correct");

            return View(slider);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound("Id is missing");

            Slider slider = _db.Sliders.Find(id);

            if (slider == null)
                return HttpNotFound("Id is not correct");

            return View(slider);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Slider slider = _db.Sliders.Find(id);

            if(!Extensions.DeleteImage(Server.MapPath("~/Public/img/team"), slider.Image))
            {
                ViewBag.DeleteError = "File does not exist";
                return View();

            }

            _db.Sliders.Remove(slider);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}