using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Entity
{
    public class LOPSH
    {
        public LOPSH()
        {
            SINHVIEN = new HashSet<SINHVIEN>();
        }

        [Key][StringLength(10)]
        public string MALOPSH { get; set; }

        [Required][StringLength(50)]
        public string TENLOPSH { get; set; }

        [Required][StringLength(10)]
        public string MAKHOA { get; set; }

        [ForeignKey("MAKHOA")]
        public virtual KHOA KHOA { get; set; }

        public virtual ICollection<SINHVIEN> SINHVIEN { get; set; }
    }
}
