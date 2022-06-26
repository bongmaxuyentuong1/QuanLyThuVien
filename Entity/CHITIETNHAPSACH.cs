using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Entity
{
    public class CHITIETNHAPSACH
    {
        [Key][Column(Order = 0)][StringLength(10)]
        public string MANHAPSACH { get; set; }

        [Key][Column(Order = 1)][StringLength(10)]
        public string MASACH { get; set; }

        public int SOLUONG { get; set; }

        [ForeignKey("MANHAPSACH")]
        public virtual NHAPSACH NHAPSACH { get; set; }

        [ForeignKey("MASACH")]
        public virtual SACH SACH { get; set; }
    }
}
