using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMua.Models;

namespace IMua.Areas.Admin.Controllers
{
    public class TrangChuController : Controller
    {
        IMuaDB db = new IMuaDB();
        // GET: Admin/TrangChu
        public ActionResult Index()
        {
            ViewBag.SanPhamBanChay = db.SanPhamBanChays.Take(4);
            return View();
        }
    }
}