using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Entity
{
    public class CHITIETPHIEUMUON
    {
        [Key][Column(Order = 0)][StringLength(10)]
        public string MAPHIEU { get; set; }

        [Key][Column(Order = 1)][StringLength(10)]
        public string MASACH { get; set; }

        public int SOLUONG { get; set; } 

        [ForeignKey("MAPHIEU")]
        public virtual PHIEUMUON PHIEUMUON { get; set; }

        [ForeignKey("MASACH")]
        public virtual SACH SACH { get; set; }
    }
}
