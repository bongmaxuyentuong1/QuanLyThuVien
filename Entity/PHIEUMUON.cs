using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Entity
{
    public class PHIEUMUON
    {
        public PHIEUMUON()
        {
            CHITIETPHIEUMUON = new HashSet<CHITIETPHIEUMUON>();
        }

        [Key][StringLength(10)]
        public string MAPHIEU { get; set; }

        [Required][StringLength(10)]
        public string MADOCGIA { get; set; }

        public DateTime NGAYMUON { get; set; }

        public DateTime NGAYTRA { get; set; }

        [StringLength(10)]
        public string MANGUOIDUNG { get; set; }

        [ForeignKey("MADOCGIA")]
        public virtual DOCGIA DOCGIA { get; set; }

        [ForeignKey("MANGUOIDUNG")]
        public virtual NGUOIDUNG NGUOIDUNG { get; set; }

        public virtual ICollection<CHITIETPHIEUMUON> CHITIETPHIEUMUON { get; set; }

    }
}
