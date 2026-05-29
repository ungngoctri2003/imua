namespace Nhom8_IMUA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserGroup")]
    public partial class UserGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserGroup()
        {
            Credentials = new HashSet<Credential>();
            NguoiDungs = new HashSet<NguoiDung>();
        }

        [Key]
        [Required(ErrorMessage = "Mã loại tài khoản không được để trống")]
        [DisplayName("Mã loại tài khoản")]
        [StringLength(50)]
        public string GroupID { get; set; }

        [Required(ErrorMessage = "Tên loại tài khoản không được để trống")]
        [DisplayName("Tên loại tài khoản")]
        [StringLength(200)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Credential> Credentials { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
    }
}
