using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DTO
{
    public class PM_CHITIET
    {
        public string MAPHIEUMUON { get; set; }
        public string MANGUOIDUNG { get; set; }
        public string MADOCGIA { get; set; }    
        public DateTime NGAYMUON { get; set; }
        public DateTime NGAYTRA { get; set; }
        public string MASACH { get; set; }
        public int SOLUONG { get; set; }
    }
}
