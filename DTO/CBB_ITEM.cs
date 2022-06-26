using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThuVien.DTO
{
    internal class CBB_ITEM
    {
        public int VALUE { get; set; }
        public string TEXT { get; set; }
        public override string ToString()
        {
            return TEXT;
        }
    }
}
