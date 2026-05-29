using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom8_IMUA.Models;

namespace Nhom8_IMUA.Controllers
{
    public class InvoiceController : Controller
    {
        private Nhom8DB db = new Nhom8DB();
        public ActionResult Index(int? id)
        {
            var chiTietHoaDons = db.HoaDons.Where(p => p.MaND == id).Select(p => p);
            return View(chiTietHoaDons);
        }

        public ActionResult ProductInvoice(int? id)
        {
            var sanphams = db.ChiTietHoaDons.Where(p => p.MaHD == id).Include(p => p.SanPham).Select(p => p);
            return PartialView(sanphams);
        }

        public ActionResult Details(int? id)
        {
            var chiTietHoaDon = db.ChiTietHoaDons.Where(p => p.MaHD == id).Include(c => c.HoaDon).Include(c => c.SanPham).Select(p => p);
            ViewBag.User = db.HoaDons.Where(p => p.MaHD == id).Include(p => p.NguoiDung).Select(p => p);
            return View(chiTietHoaDon);
        }
    }
}