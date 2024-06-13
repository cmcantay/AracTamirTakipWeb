﻿using AracTamirTakip.BusinessLayer.Abstract;
using AracTamirTakip.Entities.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AracTamirTakip.Web.Controllers.Web
{
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        Repository<Blog> rpBlog = new Repository<Blog>();

        // GET: Blog
        public ActionResult Index()
        {
            return View(rpBlog.List().OrderByDescending(x=>x.BlogId));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Blog blog)
        {
            try
            {
                blog.Tarih = DateTime.Now;
                rpBlog.Insert(blog);
                TempData["Ok"] = "Blog Kaydedildi";
            }
            catch (Exception)
            {
                TempData["No"] = "Hata Oluştu";
            }
            return RedirectToAction("Index");
        }
    public ActionResult Sil(int blogid)
        {
            var silinecek = rpBlog.GetById(blogid);
            return View(silinecek);
        }
        [HttpPost]
        public ActionResult Sil(Blog blog)
        {
            var silinecek = rpBlog.GetById(blog.BlogId);
            rpBlog.Delete(silinecek);
            TempData["Ok"] = "Kayıt Silindi";
            return RedirectToAction("Index");
        }
        public ActionResult BlogDuzenle(int blogid)
        {
            return View(rpBlog.GetById(blogid));
        }
        [HttpPost]
        public ActionResult BlogDuzenle(Blog blog)
        {
            blog.Tarih = DateTime.Now;
            rpBlog.Update(blog);
            return RedirectToAction("Index");
        }
    }
}