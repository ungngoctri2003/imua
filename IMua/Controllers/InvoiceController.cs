using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMua.Models;

namespace IMua.Controllers
{
    public class InvoiceController : Controller
    {
        private IMuaDB db = new IMuaDB();
        public ActionResult Index(int? id)
        {
            var chiTietHoaDons = db.HoaDons
                .Where(p => p.MaND == id)
                .Include(p => p.ChiTietHoaDons)
                .OrderByDescending(p => p.NgayMua)
                .ToList();
            return View(chiTietHoaDons);
        }

        public ActionResult ProductInvoice(int? id)
        {
            var sanphams = db.ChiTietHoaDons.Where(p => p.MaHD == id).Include(p => p.SanPham).Select(p => p);
            return PartialView(sanphams);
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return View(new List<ChiTietHoaDon>());
            }

            var items = db.ChiTietHoaDons
                .Where(p => p.MaHD == id.Value)
                .Include(c => c.SanPham)
                .Include(c => c.HoaDon)
                .Include(c => c.HoaDon.NguoiDung)
                .ToList();

            var order = db.HoaDons
                .Include(p => p.NguoiDung)
                .FirstOrDefault(p => p.MaHD == id.Value);

            if (order == null && items.Any())
            {
                order = items.First().HoaDon;
            }

            ViewBag.Order = order;
            return View(items);
        }
    }
}