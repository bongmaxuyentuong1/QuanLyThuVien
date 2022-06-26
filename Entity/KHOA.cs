using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Entity
{
    public class KHOA
    {
        public KHOA()
        {
            GIANGVIEN = new HashSet<GIANGVIEN>();
            LOPSH = new HashSet<LOPSH>();
        }

        [Key][StringLength(10)]
        public string MAKHOA { get; set; }

        [Required][StringLength(50)]
        public string TENKHOA { get; set; }

        public virtual ICollection<GIANGVIEN> GIANGVIEN { get; set; }

        public virtual ICollection<LOPSH> LOPSH { get; set; }
    }
}
