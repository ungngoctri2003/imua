namespace Nhom8_IMUA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPhamBanChay")]
    public partial class SanPhamBanChay
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaSP { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string TenSP { get; set; }

        [StringLength(100)]
        public string AnhDaiDien { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "money")]
        public decimal Gia { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int KhuyenMai { get; set; }

        public int? Tong { get; set; }
    }
}
