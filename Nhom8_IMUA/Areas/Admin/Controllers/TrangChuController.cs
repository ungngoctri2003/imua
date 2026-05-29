using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom8_IMUA.Models;

namespace Nhom8_IMUA.Areas.Admin.Controllers
{
    public class TrangChuController : Controller
    {
        Nhom8DB db = new Nhom8DB();
        // GET: Admin/TrangChu
        public ActionResult Index()
        {
            ViewBag.SanPhamBanChay = db.SanPhamBanChays.Take(4);
            return View();
        }
    }
}