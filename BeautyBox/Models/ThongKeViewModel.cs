using System.Collections.Generic;

namespace BeautyBox.Models
{
    public class ThongKeViewModel
    {
        public decimal TongDoanhThu { get; set; }
        public decimal DoanhThuThangNay { get; set; }
        public int TongHoaDon { get; set; }
        public int HoaDonThangNay { get; set; }
        public int TongSanPhamDaBan { get; set; }
        public int TongSanPham { get; set; }
        public int TongDanhMuc { get; set; }
        public int TongNguoiDung { get; set; }
        public int NguoiDungHoatDong { get; set; }
        public List<ThongKeThangItem> DoanhThuTheoThang { get; set; }
        public List<ThongKeNhomItem> NguoiDungTheoNhom { get; set; }
    }

    public class ThongKeThangItem
    {
        public string Nhan { get; set; }
        public int SoHoaDon { get; set; }
        public decimal DoanhThu { get; set; }
    }

    public class ThongKeNhomItem
    {
        public string TenNhom { get; set; }
        public int SoLuong { get; set; }
    }
}
