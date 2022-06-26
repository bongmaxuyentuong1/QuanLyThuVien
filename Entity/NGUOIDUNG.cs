using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Entity
{
    public class NGUOIDUNG
    {
        public NGUOIDUNG()
        {
            BANGIAO = new HashSet<BANGIAO>();
            BANGIAO1 = new HashSet<BANGIAO>();
            NHAPSACH = new HashSet<NHAPSACH>();
            PHIEUMUON = new HashSet<PHIEUMUON>();
        }

        [Key][StringLength(10)]
        public string MANGUOIDUNG { get; set; }

        [Required][StringLength(10)]
        public string MANHIEMVU { get; set; }

        [Required][StringLength(50)]
        [Index(IsUnique = true)]
        public string TAIKHOAN { get; set; }

        [Required][StringLength(50)]
        public string MATKHAU { get; set; }

        [Required][StringLength(50)]
        public string HOTEN { get; set; }

        public DateTime NAMSINH { get; set; }

        public bool GIOITINH { get; set; }

        [Required][StringLength(50)]
        public string DIENTHOAI { get; set; }

        [Required][StringLength(50)]
        public string EMAIL { get; set; }

        [ForeignKey("MANHIEMVU")]
        public virtual NHIEMVU NHIEMVU { get; set; }

        public virtual ICollection<BANGIAO> BANGIAO { get; set; }

        public virtual ICollection<BANGIAO> BANGIAO1 { get; set; }

        public virtual ICollection<NHAPSACH> NHAPSACH { get; set; }

        public virtual ICollection<PHIEUMUON> PHIEUMUON { get; set; }
    }
}
