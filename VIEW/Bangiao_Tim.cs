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
    public partial class Bangiao_Tim : Form
    {
        private string manguoidung;
        public Bangiao_Tim(string manguoidung)
        {
            this.manguoidung = manguoidung;
            InitializeComponent();
            GUI();
        }
        public void GUI()
        {
            dataGridView1.DataSource = typeof(List<BANGIAO_VIEW_CHITIET>);
            dataGridView1.DataSource = new List<BANGIAO_VIEW_CHITIET>();
            setColumnsHeader();
        }
        public void setColumnsHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Mã bàn giao";
            dataGridView1.Columns[1].HeaderText = "Mã người bàn giao";
            dataGridView1.Columns[2].HeaderText = "Ngày bàn giao";
            dataGridView1.Columns[3].HeaderText = "Mã người xác nhận";
            dataGridView1.Columns[4].HeaderText = "Ngày xác nhận";
            dataGridView1.Columns[5].HeaderText = "Mã sách";
            dataGridView1.Columns[6].HeaderText = "Số lượng";
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string mabangiao = txtMaBanGiao.Text;
            string manguoibangiao = txtMaNguoiBanGiao.Text;
            string manguoixacnhan = txtMaNguoiXacNhan.Text;
            string masach = txtMaSach.Text;
            dataGridView1.DataSource = typeof(List<BANGIAO_VIEW_CHITIET>);
            dataGridView1.DataSource = BLL_BANGIAO.Instance.timBanGiaoChiTiet(mabangiao, manguoibangiao, manguoixacnhan, masach);
            setColumnsHeader();

        }
    }
}
