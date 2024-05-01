using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

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
    }
}