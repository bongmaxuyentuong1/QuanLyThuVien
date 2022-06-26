using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Entity
{
    public class SACH
    {
        public SACH()
        {
            CHITIETBANGIAO = new HashSet<CHITIETBANGIAO>();
            CHITIETNHAPSACH = new HashSet<CHITIETNHAPSACH>();
            CHITIETPHIEUMUON = new HashSet<CHITIETPHIEUMUON>();
        }

        [Key][StringLength(10)]
        public string MASACH { get; set; }

        [Required][StringLength(50)]
        public string NHANDE { get; set; }

        [Required][StringLength(10)]
        public string MATHELOAI { get; set; }

        [Required][StringLength(10)]
        public string MANXB { get; set; }

        public DateTime NAMXB { get; set; }

        [Required][StringLength(10)]
        public string MANGONNGU { get; set; }

        [Required][StringLength(50)]
        public string TACGIA { get; set; }

        public int TONGSL { get; set; }

        public int SLDANGMUON { get; set; }

        public int SLHIENTAI { get; set; }

        public int SLTONKHO { get; set; }

        [ForeignKey("MANXB")]
        public virtual NXB NXB { get; set; }

        [ForeignKey("MANGONNGU")]
        public virtual NGONNGU NGONNGU { get; set; }

        [ForeignKey("MATHELOAI")]
        public virtual THELOAI THELOAI { get; set; }

        public virtual ICollection<CHITIETBANGIAO> CHITIETBANGIAO { get; set; }

        public virtual ICollection<CHITIETNHAPSACH> CHITIETNHAPSACH { get; set; }

        public virtual ICollection<CHITIETPHIEUMUON> CHITIETPHIEUMUON { get; set; }
    }
}
