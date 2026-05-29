namespace Nhom8_IMUA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguoiDung()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        [Key]
        public int MaND { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(50)]
        public string MatKhau { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100)]
        public string HoTen { get; set; }

        [Column(TypeName = "text")]
        public string AnhDaiDien { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [StringLength(50)]
        public string SoDT { get; set; }

        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [StringLength(50)]
        public string Email { get; set; }

        public bool TrangThai { get; set; }

        [Required(ErrorMessage = "Mã loại tài khoản không được để trống")]
        [StringLength(50)]
        public string GroupID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
