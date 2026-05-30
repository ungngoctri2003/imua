namespace BeautyBox.Models
{
    public class ProductCardViewModel
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string AnhDaiDien { get; set; }
        public decimal Gia { get; set; }
        public int KhuyenMai { get; set; }

        public static ProductCardViewModel From(SanPham product)
        {
            return new ProductCardViewModel
            {
                MaSP = product.MaSP,
                TenSP = product.TenSP,
                AnhDaiDien = product.AnhDaiDien,
                Gia = product.Gia,
                KhuyenMai = product.KhuyenMai
            };
        }

        public static ProductCardViewModel From(SanPhamBanChay product)
        {
            return new ProductCardViewModel
            {
                MaSP = product.MaSP,
                TenSP = product.TenSP,
                AnhDaiDien = product.AnhDaiDien,
                Gia = product.Gia,
                KhuyenMai = product.KhuyenMai
            };
        }
    }
}
