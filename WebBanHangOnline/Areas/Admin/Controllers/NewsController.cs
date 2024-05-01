using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        ApplicationDbContext dbConect = new ApplicationDbContext();
        // GET: Admin/News
        public ActionResult Index()
        {
            var items = dbConect.News.OrderByDescending(x => x.Title).ToList();
            return View(items);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(News model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.CategoryId = 2;
                model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                dbConect.News.Add(model);
                dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var item = dbConect.News.Find(id);
            if (item != null)
            {
                dbConect.News.Remove(item);
                dbConect.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public ActionResult Edit(int id)
        {
            var item = dbConect.News.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News model)
        {
            if (ModelState.IsValid)
            {
                dbConect.News.Attach(model);
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                dbConect.Entry(model).Property(x => x.Title).IsModified = true;
                //dbConect.Entry(model).Property(x => x.CategoryId).IsModified = true;
                dbConect.Entry(model).Property(x => x.Description).IsModified = true;
                dbConect.Entry(model).Property(x => x.Alias).IsModified = true;
                dbConect.Entry(model).Property(x => x.SeoDescription).IsModified = true;
                dbConect.Entry(model).Property(x => x.SeoKeyword).IsModified = true;
                dbConect.Entry(model).Property(x => x.SeoTitle).IsModified = true;
                dbConect.Entry(model).Property(x => x.ModifiedDate).IsModified = true;
                dbConect.Entry(model).Property(x => x.ModeifiedBy).IsModified = true;
                dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}