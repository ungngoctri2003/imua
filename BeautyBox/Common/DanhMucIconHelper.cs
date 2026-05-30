namespace BeautyBox.Common
{
    public static class DanhMucIconHelper
    {
        public static string GetIconClass(int maDM)
        {
            switch (maDM)
            {
                case 1: return "fa-palette";
                case 2: return "fa-medkit";
                case 3: return "fa-spa";
                case 4: return "fa-leaf";
                case 5: return "fa-cut";
                case 6: return "fa-star";
                case 7: return "fa-heartbeat";
                case 8: return "fa-th-large";
                default: return "fa-tag";
            }
        }

        public static string GetIconClass(string tenDM)
        {
            if (string.IsNullOrWhiteSpace(tenDM))
            {
                return "fa-tag";
            }

            var name = tenDM.ToLowerInvariant();

            if (name.Contains("trang điểm") || name.Contains("trang diem"))
                return "fa-palette";
            if (name.Contains("điều trị") || name.Contains("dieu tri"))
                return "fa-medkit";
            if (name.Contains("da mặt") || name.Contains("da mat"))
                return "fa-spa";
            if (name.Contains("toàn thân") || name.Contains("toan than"))
                return "fa-leaf";
            if (name.Contains("tóc") || name.Contains("toc"))
                return "fa-cut";
            if (name.Contains("thương hiệu") || name.Contains("thuong hieu"))
                return "fa-star";
            if (name.Contains("sức khỏe") || name.Contains("suc khoe") || name.Contains("dinh dưỡng") || name.Contains("dinh duong"))
                return "fa-heartbeat";

            return "fa-tag";
        }
    }
}
