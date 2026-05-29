using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Nhom8_IMUA.Models
{
    public class RegisterModel
    {
        [Key]
        public int MaND { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [StringLength(50)]
        [DisplayName("Tên đăng nhập")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
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
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }

        public bool TrangThai { get; set; }
    }
}