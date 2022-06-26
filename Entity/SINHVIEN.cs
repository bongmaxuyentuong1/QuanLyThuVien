using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Entity
{
    public class SINHVIEN
    {
        [Key][StringLength(10)]
        public string MADOCGIA { get; set; }

        [StringLength(10)]
        [Index(IsUnique = true)]
        public string MASINHVIEN { get; set; }

        [Required][StringLength(10)]
        public string MALOPSH { get; set; }

        [ForeignKey("MADOCGIA")]
        public virtual DOCGIA DOCGIA { get; set; }

        [ForeignKey("MALOPSH")]
        public virtual LOPSH LOPSH { get; set; }
    }
}
