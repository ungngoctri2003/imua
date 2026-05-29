namespace Nhom8_IMUA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        [Key]
        public int MaSP { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(100)]
        public string TenSP { get; set; }

        [Column(TypeName = "text")]
        [Required(ErrorMessage = "Ảnh sản phẩm không được để trống")]
        public string AnhDaiDien { get; set; }

        [Column(TypeName = "money")]
        [Required(ErrorMessage = "Giá sản phẩm không được để trống")]
        public decimal Gia { get; set; }

        [Required(ErrorMessage = "Khuyến mãi không được để trống")]
        public int KhuyenMai { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        [StringLength(4000)]
        public string MoTa { get; set; }

        [Required(ErrorMessage = "Xuất xứ không được để trống")]
        [StringLength(100)]
        public string XuatXu { get; set; }

        [Column(TypeName = "text")]
        [Required(ErrorMessage = "Trọng lượng không được để trống")]
        public string TrongLuong { get; set; }

        public int MaLoai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        public virtual LoaiSP LoaiSP { get; set; }
    }
}
