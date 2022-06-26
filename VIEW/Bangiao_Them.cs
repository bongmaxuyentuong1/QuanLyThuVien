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
    public partial class Bangiao_Them : Form
    {
        private string manguoidung;
        public static void showMessage(string message)
        {
            MessageBox.Show(message);
        }
        public Bangiao_Them(string manguoidung)
        {
            this.manguoidung = manguoidung;
            InitializeComponent();
            GUI();
        }
        public void setColumnsHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Mã sách";
            dataGridView1.Columns[1].HeaderText = "Số lượng";
        }
        public void GUI()
        {
            lbMaBanGiao.Text = BLL_BANGIAO.Instance.getMaBanGiaoMoi();
            lbMaNguoiBanGiao.Text = this.manguoidung;
            lbNgayBanGiao.Text = DateTime.Now.ToString();
            //dataGridView1.DataSource = typeof(List<BANGIAO_VIEW_SOLUONG>);
            //dataGridView1.DataSource = new List<BANGIAO_VIEW_SOLUONG>();
            //setColumnsHeader();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string masach = txtMaSach.Text;
            bool res = Int32.TryParse(txtSoLuong.Text, out int soluong);
            List<BANGIAO_VIEW_SOLUONG> list = (List<BANGIAO_VIEW_SOLUONG>)dataGridView1.DataSource;
            if (res)
            {
                dataGridView1.DataSource = typeof(List<BANGIAO_VIEW_SOLUONG>);
                dataGridView1.DataSource = BLL_BANGIAO.Instance.themDongDGVSoLuong(list, masach, soluong);
                setColumnsHeader();
            }
            else
            {
                //MessageBox.Show("Số lượng không hợp lệ");
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Số lượng không hợp lệ!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string masach = dataGridView1.SelectedRows[0].Cells["MASACH"].Value.ToString();
                List<BANGIAO_VIEW_SOLUONG> list = (List<BANGIAO_VIEW_SOLUONG>)dataGridView1.DataSource;
                dataGridView1.DataSource = typeof(List<BANGIAO_VIEW_SOLUONG>);
                dataGridView1.DataSource = BLL_BANGIAO.Instance.xoaDongDGV(list, masach);
                setColumnsHeader();
            }
            else
            {
                //MessageBox.Show("Vui lòng chỉ chọn 1 dòng");
                CN_Thongbao f = new CN_Thongbao();
                f.setNotice("Vui lòng chỉ chọn 1 dòng!");

            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string mabangiao = lbMaBanGiao.Text;
            string manguoibangiao = lbMaNguoiBanGiao.Text;
            DateTime ngaybangiao = Convert.ToDateTime(lbNgayBanGiao.Text);
            List<BANGIAO_VIEW_SOLUONG> list = (List<BANGIAO_VIEW_SOLUONG>)dataGridView1.DataSource;
            BLL_BANGIAO.Instance.taoMoiBanGiao(mabangiao, manguoibangiao, ngaybangiao, list);
            //MessageBox.Show("Tạo mới bàn giao thành công");
            dataGridView1.DataSource = null;
            CN_Thongbao f = new CN_Thongbao();
            f.setNotice("Tạo mới bàn giao thành công!");
            GUI();
        }
    }
}
