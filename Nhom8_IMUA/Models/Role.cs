namespace Nhom8_IMUA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Role")]
    public partial class Role
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Role()
        {
            Credentials = new HashSet<Credential>();
        }

        [Key]
        [Required(ErrorMessage = "Mã vai trò không được để trống")]
        [DisplayName("Mã vai trò")]
        [StringLength(50)]
        public string RoleID { get; set; }

        [Required(ErrorMessage = "Tên vai trò không được để trống")]
        [DisplayName("Tên vai trò")]
        [StringLength(200)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Credential> Credentials { get; set; }
    }
}
