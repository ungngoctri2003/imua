namespace Nhom8_IMUA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Credential")]
    public partial class Credential
    {
        [Key]
        public long CredentialID { get; set; }

        [Required(ErrorMessage = "Mã loại tài khoản không được để trống")]
        [DisplayName("Mã loại tài khoản")]
        [StringLength(50)]
        public string GroupID { get; set; }

        [Required(ErrorMessage = "Mã vai trò không được để trống")]
        [DisplayName("Mã vai trò")]
        [StringLength(50)]
        public string RoleID { get; set; }

        public virtual Role Role { get; set; }

        public virtual UserGroup UserGroup { get; set; }
    }
}
