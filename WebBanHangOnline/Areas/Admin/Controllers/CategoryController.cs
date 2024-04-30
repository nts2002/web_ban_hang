using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

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
    }
}