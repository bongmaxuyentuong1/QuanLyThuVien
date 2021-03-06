using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Entity
{
    public class NXB
    {
        public NXB()
        {
            SACH = new HashSet<SACH>();
        }

        [Key][StringLength(10)]
        public string MANXB { get; set; }

        [Required][StringLength(50)]
        public string TENNXB { get; set; }

        public virtual ICollection<SACH> SACH { get; set; }
    }
}
