﻿using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace yourlook.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    public class ColorController : Controller
    {
        YourlookContext db=new YourlookContext();
        [Route("color")]
        public IActionResult Color(int? page)
        {
            var name = HttpContext.Session.GetString("NameAdmin");
            if (name == null)
            {
                return RedirectToAction("Login", "HomeAdmin");
            }
            int pageSize = 10;
            int pageNumber = page ?? 1;
            var lstColor=db.DbColors.AsNoTracking().OrderBy(x=>x.CreateDate).ToList();
            PagedList<DbColor> lst = new PagedList<DbColor>(lstColor,pageNumber, pageSize);
            return View(lst);
        }
        [Route("taocolor")]
        [HttpGet]
        public IActionResult TaoColor()
        {
            return View();
        }
        [Route("taocolor")]
        [HttpPost]
        public IActionResult TaoColor(DbColor color)
        {
            if (ModelState.IsValid)
            {
                color.CreateDate = DateTime.Now;
                db.DbColors.Add(color);
                db.SaveChanges();
            }
            return RedirectToAction("Color");
        }
        [Route("suacolor")]
        [HttpGet]
        public IActionResult SuaColor(int colorid)
        {
            var sp = db.DbColors.Find(colorid);
            return View(sp);
        }
        [Route("suacolor")]
        [HttpPost]
        public IActionResult SuaColor(DbColor color)
        {
            if (ModelState.IsValid)
            {
                color.ModifiedDate = DateTime.Now;
                db.DbColors.Attach(color);
                db.Entry(color).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Color");
        }
        [Route("xoacolor")]
        [HttpGet]
        public IActionResult XoaColor(int colorid)
        {
            TempData["Message"] = "";
            var sp= db.DbChiTietSanPhams.Any(x=>x.ColorId==colorid);
            if (sp)
            {
                TempData["Message"] = "Màu đã được sử dụng nên không thể xóa";
                return RedirectToAction("Color");
            }
            var color=db.DbColors.Find(colorid);
            if (color !=null)
            {                
                db.DbColors.Remove(color);
                db.SaveChanges();
            }
            TempData["Message"] = "Màu ĐÃ ĐƯỢC XÓA";
            return RedirectToAction("Color");
        }
    }
}
