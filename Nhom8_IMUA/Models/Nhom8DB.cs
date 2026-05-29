using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Nhom8_IMUA.Models
{
    public partial class Nhom8DB : DbContext
    {
        public Nhom8DB()
            : base("name=Nhom8DB")
        {
        }

        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<LoaiSP> LoaiSPs { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<TinTuc> TinTucs { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<SanPhamBanChay> SanPhamBanChays { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DanhMuc>()
                .Property(e => e.AnhDM)
                .IsUnicode(false);

            modelBuilder.Entity<DanhMuc>()
                .Property(e => e.BieuTuong)
                .IsUnicode(false);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.PhiVanChuyen)
                .HasPrecision(19, 4);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.ThanhTien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.AnhDaiDien)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.SoDT)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.HoaDons)
                .WithOptional(e => e.NguoiDung)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Credentials)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.AnhDaiDien)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.Gia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.TrongLuong)
                .IsUnicode(false);

            modelBuilder.Entity<TinTuc>()
                .Property(e => e.AnhTinTuc)
                .IsUnicode(false);

            modelBuilder.Entity<UserGroup>()
                .HasMany(e => e.Credentials)
                .WithRequired(e => e.UserGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPhamBanChay>()
                .Property(e => e.Gia)
                .HasPrecision(19, 4);
        }
    }
}
