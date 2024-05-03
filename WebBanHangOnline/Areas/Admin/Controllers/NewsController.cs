using PagedList;
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
        ApplicationDbContext dbConect = new ApplicationDbContext();// Khai bao database
        // GET: Admin/News
        public ActionResult Index(string SearchText, int? page)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<News> items = dbConect.News.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x => x.Alias.Contains(SearchText) || x.Title.Contains(SearchText));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
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
                //dbConect.News.Attach(model);
                //model.ModifiedDate = DateTime.Now;
                //model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                //dbConect.Entry(model).Property(x => x.Title).IsModified = true;
                //dbConect.Entry(model).Property(x => x.Image).IsModified = true;
                //dbConect.Entry(model).Property(x => x.Description).IsModified = true;
                //dbConect.Entry(model).Property(x => x.Alias).IsModified = true;
                //dbConect.Entry(model).Property(x => x.SeoDescription).IsModified = true;
                //dbConect.Entry(model).Property(x => x.SeoKeyword).IsModified = true;
                //dbConect.Entry(model).Property(x => x.SeoTitle).IsModified = true;
                //dbConect.Entry(model).Property(x => x.ModifiedDate).IsModified = true;
                //dbConect.Entry(model).Property(x => x.ModeifiedBy).IsModified = true;
                //dbConect.SaveChanges();

                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                dbConect.News.Attach(model);
                dbConect.Entry(model).State = System.Data.Entity.EntityState.Modified;// Dòng này thay cho code từ dòng 70 ->78
                dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult IsActive(int id)
        {
            var item = dbConect.News.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                dbConect.Entry(item).State = System.Data.Entity.EntityState.Modified;
                dbConect.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive});
            }
            return Json(new { success = false});
        }

        public ActionResult DeleteAll(string ids) {

            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = dbConect.News.Find(Convert.ToInt32(item));
                        dbConect.News.Remove(obj);
                        dbConect.SaveChanges();  
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
            
        }
    }
}