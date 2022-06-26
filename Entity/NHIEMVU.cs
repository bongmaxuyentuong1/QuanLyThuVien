using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Entity
{
    public class NHIEMVU
    {
        public NHIEMVU()
        {
            NGUOIDUNGs = new HashSet<NGUOIDUNG>();
        }

        [Key][StringLength(10)]
        public string MANHIEMVU { get; set; }

        [Required][StringLength(50)]
        public string TENNHIEMVU { get; set; }

        public virtual ICollection<NGUOIDUNG> NGUOIDUNGs { get; set; }
    }
}
