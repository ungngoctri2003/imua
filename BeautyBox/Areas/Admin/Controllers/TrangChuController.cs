using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeautyBox.Models;

namespace BeautyBox.Areas.Admin.Controllers
{
    public class TrangChuController : Controller
    {
        BeautyBoxDB db = new BeautyBoxDB();
        // GET: Admin/TrangChu
        public ActionResult Index()
        {
            ViewBag.SanPhamBanChay = db.SanPhamBanChays.Take(4);
            ViewBag.CountSanPham = db.SanPhams.Count();
            ViewBag.CountHoaDon = db.HoaDons.Count();
            ViewBag.CountNguoiDung = db.NguoiDungs.Count();
            ViewBag.CountTinTuc = db.TinTucs.Count();
            return View();
        }
    }
}