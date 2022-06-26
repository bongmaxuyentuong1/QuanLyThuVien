using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Entity
{
    public class BANGIAO
    {
        public BANGIAO()
        {
            CHITIETBANGIAO = new HashSet<CHITIETBANGIAO>();
        }

        [Key][StringLength(10)]
        public string MABANGIAO { get; set; }

        public DateTime? NGAYBANGIAO { get; set; }

        public DateTime? NGAYXACNHAN { get; set; }

        [StringLength(10)]
        public string MANGUOIBANGIAO { get; set; }

        [StringLength(10)]
        public string MANGUOIXACNHAN { get; set; }

        [ForeignKey("MANGUOIBANGIAO")]
        public virtual NGUOIDUNG NGUOIDUNG { get; set; }

        [ForeignKey("MANGUOIXACNHAN")]
        public virtual NGUOIDUNG NGUOIDUNG1 { get; set; }

        public virtual ICollection<CHITIETBANGIAO> CHITIETBANGIAO { get; set; }
    }
}
