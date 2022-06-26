using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Entity
{
    public class THELOAI
    {
        public THELOAI()
        {
            SACH = new HashSet<SACH>();
        }

        [Key][StringLength(10)]
        public string MATHELOAI { get; set; }

        [Required][StringLength(50)]
        public string TENTHELOAI { get; set; }

        public virtual ICollection<SACH> SACH { get; set; }
    }
}
