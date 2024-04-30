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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model) 
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                dbConect.Categories.Add(model);
                dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }    

    }
}