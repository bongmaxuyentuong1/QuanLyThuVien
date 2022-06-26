using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Entity
{
    public class GIANGVIEN
    {
        [Key][StringLength(10)]
        public string MADOCGIA { get; set; }

        [Required][StringLength(50)]
        public string HOCVI { get; set; }

        [Required][StringLength(10)]
        public string MAKHOA { get; set; }

        [ForeignKey("MADOCGIA")]
        public virtual DOCGIA DOCGIA { get; set; }

        [ForeignKey("MAKHOA")]
        public virtual KHOA KHOA { get; set; }
    }
}
