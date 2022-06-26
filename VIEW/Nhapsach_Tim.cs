using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyThuVien.Entity;
using QuanLyThuVien.BLL;
using QuanLyThuVien.DTO;
namespace QuanLyThuVien.VIEW
{
    public partial class Nhapsach_Tim : Form
    {
        public Nhapsach_Tim()
        {
            InitializeComponent();
        }
        public void setDGVColumnsHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Mã nhập sách";
            dataGridView1.Columns[1].HeaderText = "Mã người dùng";
            dataGridView1.Columns[2].HeaderText = "Ngày nhập";
            dataGridView1.Columns[3].HeaderText = "Mã sách";
            dataGridView1.Columns[4].HeaderText = "Số lượng";
        }
        private void btn_Tim_Click(object sender, EventArgs e)
        {
            string manhapsach = txtManhapsach.Text;
            string manguoidung = txtManguoidung.Text;
            string masach = txtMasach.Text;
            List<NHAPSACH_CHITIET> list = BLL_NHAPSACH.Instance.timChiTietNhapSach(manhapsach, manguoidung, masach);
            dataGridView1.DataSource = list;
            setDGVColumnsHeader();
        }
    }
}
