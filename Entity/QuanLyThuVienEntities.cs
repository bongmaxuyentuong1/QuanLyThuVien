using System;
using System.Data.Entity;
using System.Linq;
using QuanLyThuVien.Entity;

namespace QuanLyThuVien
{
    public class QuanLyThuVienEntities : DbContext
    {
        public QuanLyThuVienEntities()
            : base("name=QuanLyThuVienEntities")
        {
            Database.SetInitializer<QuanLyThuVienEntities>(new CreateDB());
//            Database.SetInitializer<QuanLyThuVienEntities>(new CreateDBWhenChange());
        }
        public virtual DbSet<BANGIAO> BANGIAOs { get; set; }
        public virtual DbSet<CHITIETBANGIAO> CHITIETBANGIAOs { get; set; }
        public virtual DbSet<CHITIETNHAPSACH> CHITIETNHAPSACHes { get; set; }
        public virtual DbSet<CHITIETPHIEUMUON> CHITIETPHIEUMUONs { get; set; }
        public virtual DbSet<DOCGIA> DOCGIAs { get; set; }
        public virtual DbSet<GIANGVIEN> GIANGVIENs { get; set; }
        public virtual DbSet<KHOA> KHOAs { get; set; }
        public virtual DbSet<LOPSH> LOPSHes { get; set; }
        public virtual DbSet<NGONNGU> NGONNGUs { get; set; }
        public virtual DbSet<NGUOIDUNG> NGUOIDUNGs { get; set; }
        public virtual DbSet<NHAPSACH> NHAPSACHes { get; set; }
        public virtual DbSet<NHIEMVU> NHIEMVUs { get; set; }
        public virtual DbSet<NXB> NXBs { get; set; }
        public virtual DbSet<PHIEUMUON> PHIEUMUONs { get; set; }
        public virtual DbSet<SACH> SACHes { get; set; }
        public virtual DbSet<SINHVIEN> SINHVIENs { get; set; }
        public virtual DbSet<THELOAI> THELOAIs { get; set; }
    }

}