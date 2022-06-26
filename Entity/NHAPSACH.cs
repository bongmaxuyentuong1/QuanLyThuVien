using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Entity
{
    public class NHAPSACH
    {
        public NHAPSACH()
        {
            CHITIETNHAPSACH = new HashSet<CHITIETNHAPSACH>();
        }

        [Key][StringLength(10)]
        public string MANHAPSACH { get; set; }

        public DateTime NGAYNHAP { get; set; }

        [Required][StringLength(10)]
        public string MANGUOIDUNG { get; set; }

        [ForeignKey("MANGUOIDUNG")]
        public virtual NGUOIDUNG NGUOIDUNG { get; set; }

        public virtual ICollection<CHITIETNHAPSACH> CHITIETNHAPSACH { get; set; }
    }
}
