using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyThuVien.BLL;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.VIEW
{
    public partial class PM_Tim : Form
    {
        public PM_Tim()
        {
            InitializeComponent();
        }
        public void setDGVColumnsHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Mã phiếu mượn";
            dataGridView1.Columns[1].HeaderText = "Mã người dùng";
            dataGridView1.Columns[2].HeaderText = "Mã độc giả";
            dataGridView1.Columns[3].HeaderText = "Ngày mượn";
            dataGridView1.Columns[4].HeaderText = "Ngày trả";
            dataGridView1.Columns[5].HeaderText = "Mã sách";
            dataGridView1.Columns[6].HeaderText = "Số lượng";
        }
        private void btn_Tim_Click(object sender, EventArgs e)
        {
            string maphieu = txtMaPhieuMuon.Text;
            string madocgia = txtMaDocGia.Text;
            string manguoidung = txtMaNguoiDung.Text;
            string masach = txtMaSach.Text;
            List<PM_CHITIET> list = BLL_PHIEUMUON.Instance.timChiTietPhieumuon(maphieu, madocgia, manguoidung, masach);
            dataGridView1.DataSource = list;
            setDGVColumnsHeader();
        }
    }
}
