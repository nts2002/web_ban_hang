using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext dbConect = new ApplicationDbContext();
        // GET: Admin/Category
        public ActionResult Index()
        {
            var item = dbConect.Categories;
            return View(item);
        }

        public ActionResult Add()
        {
            return View();
        }
        /// <summary>
        /// HIển thị form thêm mới. Trả về view <<Add>> và model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model) 
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                dbConect.Categories.Add(model);
                dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }    
        /// <summary>
        /// Link đến trang sửa theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var item = dbConect.Categories.Find(id);
            return View(item);   
        }

        /// <summary>
        /// Form sửa theo Id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                dbConect.Categories.Attach(model);
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                dbConect.Entry(model).State = System.Data.Entity.EntityState.Modified;
                //dbConect.Entry(model).Property(x=>x.Title).IsModified = true;
                //dbConect.Entry(model).Property(x=>x.Description).IsModified = true;
                //dbConect.Entry(model).Property(x=>x.Alias).IsModified = true;
                //dbConect.Entry(model).Property(x=>x.SeoDescription).IsModified = true;
                //dbConect.Entry(model).Property(x=>x.SeoKeyword).IsModified = true;
                //dbConect.Entry(model).Property(x=>x.SeoTitle).IsModified = true;
                //dbConect.Entry(model).Property(x=>x.Position).IsModified = true;
                //dbConect.Entry(model).Property(x=>x.ModifiedDate).IsModified = true;
                //dbConect.Entry(model).Property(x=>x.ModeifiedBy).IsModified = true;
                dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var item = dbConect.Categories.Find(id);
            if (item != null)
            {
                dbConect.Categories.Remove(item);
                dbConect.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public ActionResult IsActive(int id)
        {
            var item = dbConect.Categories.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                dbConect.Entry(item).State = System.Data.Entity.EntityState.Modified;
                dbConect.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }


    }
}