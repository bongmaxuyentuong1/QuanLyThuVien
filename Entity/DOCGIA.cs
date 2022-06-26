using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Entity
{
    public class DOCGIA
    {
        public DOCGIA()
        {
            PHIEUMUON = new HashSet<PHIEUMUON>();
        }

        [Key][StringLength(10)]
        public string MADOCGIA { get; set; }

        [Required][StringLength(50)]
        public string HOTEN { get; set; }

        [Required][StringLength(50)]
        public string DIACHI { get; set; }

        public int SOSACHMUON { get; set; }

        public virtual ICollection<PHIEUMUON> PHIEUMUON { get; set; }

        //[ForeignKey("MADOCGIA")]
        //public virtual GIANGVIEN GIANGVIEN { get; set; }

        //[ForeignKey("MADOCGIA")]
        //public virtual SINHVIEN SINHVIEN { get; set; }
    }
}
