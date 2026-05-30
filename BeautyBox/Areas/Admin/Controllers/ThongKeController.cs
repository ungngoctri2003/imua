using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BeautyBox.Models;

namespace BeautyBox.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly BeautyBoxDB db = new BeautyBoxDB();

        public ActionResult Index()
        {
            var now = DateTime.Now;
            var dauThang = new DateTime(now.Year, now.Month, 1);
            var hoaDons = db.HoaDons.ToList();

            var model = new ThongKeViewModel
            {
                TongDoanhThu = hoaDons.Sum(h => h.ThanhTien),
                DoanhThuThangNay = hoaDons.Where(h => h.NgayMua >= dauThang).Sum(h => h.ThanhTien),
                TongHoaDon = hoaDons.Count,
                HoaDonThangNay = hoaDons.Count(h => h.NgayMua >= dauThang),
                TongSanPhamDaBan = db.ChiTietHoaDons.Sum(c => (int?)c.SoLuong) ?? 0,
                TongSanPham = db.SanPhams.Count(),
                TongDanhMuc = db.DanhMucs.Count(),
                TongNguoiDung = db.NguoiDungs.Count(),
                NguoiDungHoatDong = db.NguoiDungs.Count(n => n.TrangThai),
                DoanhThuTheoThang = BuildDoanhThuTheoThang(hoaDons, now),
                NguoiDungTheoNhom = BuildNguoiDungTheoNhom()
            };

            return View(model);
        }

        private static List<ThongKeThangItem> BuildDoanhThuTheoThang(List<HoaDon> hoaDons, DateTime now)
        {
            var result = new List<ThongKeThangItem>();

            for (var i = 5; i >= 0; i--)
            {
                var thang = now.AddMonths(-i);
                var dau = new DateTime(thang.Year, thang.Month, 1);
                var cuoi = dau.AddMonths(1);
                var trongThang = hoaDons.Where(h => h.NgayMua >= dau && h.NgayMua < cuoi).ToList();

                result.Add(new ThongKeThangItem
                {
                    Nhan = thang.ToString("MM/yyyy"),
                    SoHoaDon = trongThang.Count,
                    DoanhThu = trongThang.Sum(h => h.ThanhTien)
                });
            }

            return result;
        }

        private List<ThongKeNhomItem> BuildNguoiDungTheoNhom()
        {
            var tenNhom = db.UserGroups.ToDictionary(g => g.GroupID, g => g.Name);
            return db.NguoiDungs
                .GroupBy(n => n.GroupID)
                .Select(g => new { GroupID = g.Key, SoLuong = g.Count() })
                .ToList()
                .Select(g => new ThongKeNhomItem
                {
                    TenNhom = tenNhom.ContainsKey(g.GroupID) ? tenNhom[g.GroupID] : g.GroupID,
                    SoLuong = g.SoLuong
                })
                .OrderByDescending(x => x.SoLuong)
                .ToList();
        }
    }
}
